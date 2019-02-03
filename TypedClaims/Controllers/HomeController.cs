using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TypedClaims.Models;

namespace TypedClaims.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> AddUpdateDateClaim([FromServices]UserManager<IdentityUser> userManager)
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            //tried it and we cannot convert directly into our own type on this line so first we need to retrieve the stored claim then switch it to our own implementation
            Claim updatedClaim = (await userManager.GetClaimsAsync(currentUser)).FirstOrDefault(claim => claim.Type == nameof(UserCurrentDateTimeClaim)); 
            if (updatedClaim is null)
            {
                await userManager.AddClaimAsync(currentUser, new UserCurrentDateTimeClaim(DateTime.Now));
            }
            else
            {
                UserCurrentDateTimeClaim exampleOfConvertingToCustomType = updatedClaim; // here we have the stored claim converted back to our own type
                await userManager.ReplaceClaimAsync(currentUser, exampleOfConvertingToCustomType,
                    new UserCurrentDateTimeClaim(DateTime.Now));
            }
            return RedirectToAction("Index");
        }
    }
}
