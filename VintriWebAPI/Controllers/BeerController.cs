using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using VintriWebAPI.Filters;
using VintriWebAPI.Models;
//using static VentriWebAPI.Models.Domain;

namespace VintriWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private BeerService beerService;
        private Database db;

        public BeerController(BeerService beerService, Database db){
            this.beerService = beerService;
            this.db = db;
        }

        //api/beer/{buzz}/userrating
        [HttpGet]
        public List<Beer> GetRatingsForBeer(string beername)
        {
            var beer = beerService.GetBeerByName(beername);
            if(beer == null){
                return new List<Beer>();
                
            }
            // LINQ
            var userRatings = db.GetUserRatings().Where(x => x.BeerId == beer.id);
            beer.userRatings = userRatings.ToList();

            //serialize 
            
            return new List<Beer>{beer};

        }

        

        [HttpPost("{id}")]
        [ValidateUsername]
        public ActionResult AddUserRating(int id,UserRating userRating)
        {

            if(beerService.BeerExists(id))
            {
                userRating.BeerId = id;
                db.addUserRating(userRating);
                return Ok();
            }
            else
            {
                return NotFound();
            }           

        }
    }
}
