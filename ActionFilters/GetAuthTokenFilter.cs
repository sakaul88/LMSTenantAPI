using DeviceManager.Api.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Api.ActionFilters
{
    public class GettingAuthTokenFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Retrieve Token and Cache Id from request and store for sub-sequent request
            //var cacheId = context.HttpContext.Request.Path.Value.ToLower().Replace("/api/bronto/", string.Empty);

            if (context.HttpContext.Request.Headers["Authorization"].Count > 0)
            {
                string token = context.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                //AuthentedUser.Token = new RequestToken(token);// { accessToken = token };
                LoggedInUser.AuthentedUser = new AuthentedUser { AccessToken = token };
            }
            base.OnActionExecuting(context);
        }
    }
}
