using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VintriWebAPI.Models
{
    public class UserRating
    {
        //[System.Text.Json.Serialization.JsonPropertyName("id")]
        //public int Id { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("beerId")]
        public int BeerId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("userName")]

        public string UserName { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("rating")]
        [Range(1,5,ErrorMessage = "Enter numbers between 1 and 5")]
        public int Rating { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("comments")]
        public string Comments { get; set; }

        


       
        

       

       
    }

    
}
