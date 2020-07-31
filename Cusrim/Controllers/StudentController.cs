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
    public class StudentController : Controller
    {
        private readonly UserLogic _context = new UserLogic();
        private readonly StudentLogic _studentContext = new StudentLogic();


        // GET: Student
        public ActionResult Index()
        {
            if (Session["Id"] != null)
            {
                return RedirectToAction("Dashboard", "Student");
            }
            else
            {
                return View("Index", "Home");
            }
            //return View();
        }
        public ActionResult Register()
        {
            var viewModel = new StudentRegistration();
            return View(viewModel);
        }
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
                else
                {
                
                    return RedirectToAction("Dashboard", "Faculty");
                }
            }

            ViewBag.msg = "Incorrect Username or Password";
            return View("LoginForm");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(StudentRegistration studentRegistration)
        {


            var matricIsUnique = _studentContext.MatricIsUnique(studentRegistration.Student.MatricNo);
            if (matricIsUnique)
            {
                var user = new User
                {
                    Username = studentRegistration.Student.Email,
                    password = studentRegistration.User.password,
                    UserRole = "student",
                    CreatedAt = DateTime.Now
                };
                if (_context.Save(user))
                {
                    var student = new Student
                    {
                        MatricNo = studentRegistration.Student.MatricNo,
                        Email = studentRegistration.Student.Email,
                        UserId = _context.GetLast(),

                        

                    };
                    _studentContext.Save(student);

                }
                TempData["Success"] = "Registration Successful";
                    return RedirectToAction("Info");
            }
            if (!matricIsUnique)
                ModelState.AddModelError("EmailExist", "This Matric Number has been registered");
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}