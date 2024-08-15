using SportsHub.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsHub.API.UserAuthentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   
            try
            {

                var _userAuthentication = context.HttpContext.RequestServices.GetRequiredService<UserAuthenticationService>();

                var _appSettings = context.HttpContext.RequestServices.GetRequiredService<AppSettings>();
                var id = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //var userToken = await _userAuthentication.GetAuthenticationDetails(id);

                //if (userToken != null)
                //{
                //    double minutes = (DateTime.Now.ToUniversalTime() - userToken.ExpiryDt.ToUniversalTime()).TotalMinutes;
                //    double munitesSettingsThreeshold = _appSettings.authenticationExpirationMinutes;
                //    if (minutes > 0 && minutes < munitesSettingsThreeshold)
                //    {
                //        var result = await _userAuthentication.RefreshLonestarToken(userToken.RefreshToken);
                //        if (result != null)
                //            await _userAuthentication.SaveAuthenticationDetails(id, result.AccessToken, result.RefreshToken, Convert.ToDateTime(result.Expiration).ToString());
                //        else
                //        {

                //            var contentResult = new ContentResult
                //            {
                //                Content = "Unauthorized",
                //                ContentType = "application/json",
                //                StatusCode = 401

                //            };
                //            context.Result = contentResult;

                //            return;

                //        }

                //    }
                //    else if (minutes >= munitesSettingsThreeshold)
                //    {
                //        var contentResult = new ContentResult
                //        {
                //            Content = "Unauthorized",
                //            ContentType = "application/json",
                //            StatusCode = 401
                //        };
                //        context.Result = contentResult;
                //        return;
                //    }
                //}

            }
            catch (Exception ex) { var r = ex.Message + ex.Source + ex.StackTrace; }
            await next();
        }

    }
}
