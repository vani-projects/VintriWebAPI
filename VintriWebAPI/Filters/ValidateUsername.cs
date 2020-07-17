using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;
using VintriWebAPI.Models;

namespace VintriWebAPI.Filters
{
    public class ValidateUsername:ActionFilterAttribute
    {

        //public string username { get; set; }


  

        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
            base.OnActionExecuting(context);
            
            
            //get values from userRating.
            UserRating values = (UserRating) context.ActionArguments.Where(pair => pair.Key.Contains("userRating"))
                  .Select(pair => pair.Value).FirstOrDefault();

            var user = values.UserName.ToString();
            //string user = values.
            
            
            //string username = context.ModelState["username"].ToString();

            Regex email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!email.Match(user).Success)
            {
                throw new Exception("The username in the request body is not a valid email");
            }
            /*
            using (var reader = new StreamReader(context.ModelState[))
            {
                string final = "";
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    final = final + line;
                }

                // Do something
                Regex email = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!email.Match("vani@gmail.com").Success)
                {
                    throw new Exception("The username in the request body is not a valid email");
                }
            }*/



        }
    }
}
