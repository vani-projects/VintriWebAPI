using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace VintriWebAPI.Models
{
    public class Database
    {
        private string path = @"data\database.json";
        private List<UserRating> userRatings;

        public string getPath()
        {
            return path;
        }
        public List<UserRating> GetUserRatings()
        {

            if (System.IO.File.Exists(path))
            {
                using (var fr = System.IO.File.OpenText(path))
                {
                    string jsonRead = "";
                    string line;
                    while ((line = fr.ReadLine()) != null)
                    {
                        jsonRead = jsonRead + line;
                    }

                    userRatings = System.Text.Json.JsonSerializer.Deserialize<List<UserRating>>(jsonRead);

                    fr.Close();
                }
            }
            else
            {
                userRatings = new List<UserRating>();
            }
            return userRatings;

        }

        public void addUserRating(UserRating userRating)
        {
            if (System.IO.File.Exists(path))
            {
                using (var fr = System.IO.File.OpenText(path))
                {
                    string jsonRead = "";
                    string line;
                    while ((line = fr.ReadLine()) != null)
                    {
                        jsonRead = jsonRead + line;
                    }

                    userRatings = System.Text.Json.JsonSerializer.Deserialize<List<UserRating>>(jsonRead);
                    userRatings.Add(userRating);
                    fr.Close();
                }
            }
            else
            {
                this.userRatings = new List<UserRating>();

                userRatings.Add(userRating);
            }
            WriteToFile();
            //WriteToFile();

        }

        private void WriteToFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            string jsonWrite = System.Text.Json.JsonSerializer.Serialize(userRatings, options);

            using (StreamWriter sw = System.IO.File.CreateText(path))
            {
                using (StringReader sr = new StringReader(jsonWrite))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(line);
                    }
                    sr.Close();
                }
                sw.Close();
            }
        }
    }
}
