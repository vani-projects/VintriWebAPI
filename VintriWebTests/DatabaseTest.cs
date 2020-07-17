using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VintriWebAPI.Models;
using Xunit;

namespace VintriWebTests
{
    public class DatabaseTest
    {
        [Fact]
        public void TestAddUserRatingsFirstTime()
        {
            Console.WriteLine("TestAddUserRatingsFirstTime");
            // Arrange
            // << Remove the file if already exists
            Database database = new Database();
            if (File.Exists(database.getPath()))
            {
                File.Delete(database.getPath());
            }
            // Remove the file if already exists >>
            UserRating userRating = new UserRating
            {
                BeerId = 5,
                UserName = "vani@gmail.com",
                Rating = 5,
                Comments = "Excellent"
            };
            string expected = "[{\"beerId\":5,\"userName\":\"vani@gmail.com\",\"rating\":5,\"comments\":\"Excellent\"}]";

            //Act
            database.addUserRating(userRating);

            //Assert
            Assert.True(File.Exists(database.getPath()));
            string jsonRead = "";
            using (StreamReader sr = System.IO.File.OpenText(database.getPath())){
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    jsonRead += line.Replace(" ", String.Empty);
                }
                sr.Close();
            }
            Assert.Equal(expected, jsonRead);
        }


        [Fact]
        public void TestAddUserRatingSecondTime()
        {
            Console.WriteLine("TestAddUserRatingSecondTime");
            // Arrange
            Database database = new Database();
            UserRating userRating = new UserRating
            {
                BeerId = 5,
                UserName = "vani@gmail.com",
                Rating = 5,
                Comments = "Excellent"
            };
            if (File.Exists(database.getPath()))
            {
                File.Delete(database.getPath());
            }
            string expected = "[{\"beerId\":5,\"userName\":\"vani@gmail.com\",\"rating\":5,\"comments\":\"Excellent\"},{\"beerId\":5,\"userName\":\"vani@gmail.com\",\"rating\":5,\"comments\":\"Excellent\"}]";

            //Act
            database.addUserRating(userRating);
            database.addUserRating(userRating);

            //Assert
            Assert.True(File.Exists(database.getPath()));

            string jsonRead = "";
            using (StreamReader sr = System.IO.File.OpenText(database.getPath())) {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    jsonRead = jsonRead + line.Replace(" ", String.Empty);
                }
                sr.Close();
            }
            Assert.Equal(expected, jsonRead);
        }

        [Fact]
        public void TestGetUserRatings()
        {
            Console.WriteLine("TestGetUserRatings");
            //Arrange
            Database database = new Database();
            string json = "[{\"beerId\":5,\"userName\":\"vani@gmail.com\",\"rating\":5,\"comments\":\"Excellent\"}]";
            if (File.Exists(database.getPath()))
            {
                File.Delete(database.getPath());
            }

            using (StreamWriter sw = System.IO.File.CreateText(database.getPath())){
                sw.WriteLine(json);
                sw.Close();
            }

            //Act
            List<UserRating> userRatings = database.GetUserRatings();
            UserRating rating = userRatings.First<UserRating>();

            //Assert
            Assert.Equal(5,rating.BeerId);
            Assert.Equal("vani@gmail.com",rating.UserName);
            Assert.Equal("Excellent",rating.Comments);
            Assert.Equal(5,rating.Rating);
        }
    }
}
