using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using atomicflair.Models;

namespace atomicflair.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        /// <summary>
        /// Action extracts the user page from atomic and returns the 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>      
        public ActionResult Id(int userId)
        {
            // 5095 = robzy
            // 9541 = kikz			
            if (userId == 0)
            {
                // try and get it from the model                
                return View(new AtomicUserViewModel { Name = "Unknown user" });
            }

            var service = new AtomicUserService();
            var user = service.GetUserInformation(userId);

            if (user == null)
            {
                return View(new AtomicUserViewModel { Name = "Unknown user" });
            }

            var userModel = AtomicUserViewModel.Create(user);
            userModel.SpecialRankColor = user.IsHeroic ? "DodgerBlue" : user.IsMod ? "Red" : user.IsGod ? "Purple" : "White";

            return View(userModel);
        }

    }
}
