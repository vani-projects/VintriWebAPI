using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RestSharp;
using VintriWebAPI.Models;

public class BeerService
{
    public bool BeerExists(int beerId)
    {
        var client = new RestClient($"https://api.punkapi.com/v2/beers/{beerId}");
        var request = new RestRequest(Method.GET);
        IRestResponse response = client.ExecuteAsync(request).Result;
        if (response.IsSuccessful)
            return true;
        else
            return false;
    }

    public Beer GetBeerByName(string beername)
    {
        var client = new RestClient($"https://api.punkapi.com/v2/beers/?beer_name={beername}");
        var request = new RestRequest(Method.GET);
        IRestResponse response = client.ExecuteAsync(request).Result;
        if (response.IsSuccessful)
        {
            List<Beer> beers = (List<Beer>)JsonConvert.DeserializeObject(response.Content, typeof(List<Beer>));

            if (beers.Count > 0)
            {

                return beers.First<Beer>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}