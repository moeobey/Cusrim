using Cusrim.Core.Models;
using Cusrim.Core.ViewModels;
using Cusrim.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Cusrim.Controllers
{
    public class UserController:Controller
    {
        private readonly UserLogic _context = new UserLogic();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthenticateUser(User user)
        {

            var curUser = _context.GetByUserID(user.Username);
            if (_context.Authenticate(user))
            {
                Session["id"] = curUser.Id;
                Session["Password"] = curUser.password;
                Session["Username"] = curUser.Username;
                Session["Role"] = curUser.UserRole;
                if ((string)Session["Role"] == $"student")
                {
                    return RedirectToAction("Dashboard", "Student");
                }
                else if((string)Session["Role"] == $"faculty")
                {

                    return RedirectToAction("Dashboard", "Faculty");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            TempData["message"] = "Incorrect Username or Password";
            
            return RedirectToAction("Index", "Home");

        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
