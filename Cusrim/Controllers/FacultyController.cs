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
    public class FacultyController : Controller
    {
        private readonly UserLogic _context = new UserLogic();
        private readonly FacultyLogic _facultyContext = new FacultyLogic();
        private readonly StudentLogic _studentContext = new StudentLogic();
        private readonly ReportLogic _reportContext = new ReportLogic();



        // GET: Faculty
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
            //var viewModel = new FacultyRegistration();
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AuthenticateUser( user)
        //{
        //    ////validate model state
        //    //var curUser = _context.GetByUsername(user.Username);
        //    //if (_context.Authenticate(user))
        //    //{
        //    //    Session["id"] = curUser.Id;
        //    //    Session["Password"] = curUser.Password;
        //    //    Session["Username"] = curUser.Username;
        //    //    Session["Role"] = curUser.Role;
        //    //    Session["FinancialDate"] = _bankConfigContext.GetConfig().FinancialDate;
        //    //    if ((string)Session["Role"] == $"Admin")
        //    //    {
        //    //        var bankConfig = _bankConfigContext.GetConfig();
        //    //        if (bankConfig == null)
        //    //            Session["setup"] = "start";
        //    //        else
        //    //            Session["isBusinessOpen"] = bankConfig.IsBusinessOpen;


        //    //        return RedirectToAction("AdminDashboard", "Users");
        //    //    }
        //    //    else if ((string)Session["Role"] == $"Teller" && curUser.PasswordStatus == false)
        //    //    {
        //    //        return RedirectToAction("ChangePassword", "Users");
        //    //    }
        //    //    else if ((string)Session["Role"] == $"Teller" && curUser.PasswordStatus == true)
        //    //    {
        //    //        //user has changed password
        //    //        return RedirectToAction("TellerDashboard", "Users");
        //    //    }
        //    //    else
        //    //    {
        //    //        //user has  no role
        //    //        return RedirectToAction("TellerDashboard", "Account");
        //    //    }
        //    //}

        //    ViewBag.msg = "Incorrect Username or Password";
        //    return View("LoginForm");

        //}


        //public ActionResult Register(StudentRegistration studentRegistration)
        //{


        //    var customerInDb = _context.Get(customer.Id);
        //    var emailIsUnique = _context.EmailIsUnique(customer.Email);
        //    var phoneNumberIsUnique = _context.PhoneNumberIsUnique(customer.PhoneNumber);
        //    if (customer.Id != 0 && customerInDb.Email == customer.Email) //check if Email is Unchanged for update and set is unique to true
        //        emailIsUnique = true;

        //    if (customer.Id != 0 && customerInDb.PhoneNumber == customer.PhoneNumber) //Check if phone number is unchanged for update and set is unique to true
        //        phoneNumberIsUnique = true;

        //    if (phoneNumberIsUnique && emailIsUnique)
        //    {
        //        if (customer.Id != 0)   //update customer
        //        {
        //            customerInDb.FullName = customer.FullName;
        //            customerInDb.Address = customer.Address;
        //            customerInDb.Email = customer.Email;
        //            customerInDb.PhoneNumber = customer.PhoneNumber;
        //            customerInDb.Gender = customer.Gender;
        //            //customer.
        //            _context.Update(customer);
        //            TempData["Success"] = "Update Successful";
        //            return RedirectToAction("Index");
        //        }

        //        if (customer.Id == 0) //add customer
        //        {
        //            customer.CustomerId = _context.GenerateCustomerId();
        //            customer.Date = DateTime.Now;
        //            _context.Save(customer);
        //            TempData["Success"] = "Customer Successfully Added";

        //            return RedirectToAction("Index");
        //        }

        //    }
        //    if (!emailIsUnique)
        //        ModelState.AddModelError("EmailExist", "This Email Exists");

        //    if (!phoneNumberIsUnique)
        //        ModelState.AddModelError("PhoneNumberExist", "This Phone Number Exists");


        //    return View();
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(FacultyRegistration facultyRegistration)
        {


            var staffIsUnique = _facultyContext.StaffNoIsUnique(facultyRegistration.Faculty.StaffNo);
            if (staffIsUnique)
            {
                var user = new User
                {
                    Username = facultyRegistration.Faculty.Email,
                    password = facultyRegistration.User.password,
                    UserRole = "faculty",
                    CreatedAt = DateTime.Now
                };
                if (_context.Save(user))
                {
                    var faculty = new Faculty
                    {
                        StaffNo = facultyRegistration.Faculty.StaffNo,
                        Email = facultyRegistration.Faculty.Email,

                        UserId = _context.GetLast()
                    };
                    _facultyContext.Save(faculty);


                }
                TempData["Success"] = "Registration Successful";

                var id = _context.GetLast();
                var curUser = _context.Get(id);
                Session["id"] = curUser.Id;
                Session["Password"] = curUser.password;
                Session["Username"] = curUser.Username;
                Session["Role"] = curUser.UserRole;
                return RedirectToAction("Info");


            }
            if (!staffIsUnique)
                ModelState.AddModelError("StaffExist", "This Staff No has been registered");
            return View();
        }
        public ActionResult Save(Faculty faculty)
        {
            var userId = Session["id"];

            var facultyInDb = _facultyContext.GetByUserId(Convert.ToInt64(userId));
            facultyInDb.PhoneNumber = faculty.PhoneNumber;
            facultyInDb.Name = faculty.Name;
            facultyInDb.Department = faculty.Department;
            facultyInDb.profile_status = true;
            _facultyContext.Update(faculty);

            return RedirectToAction("Dashboard");
        }

        public ActionResult Info()
        {

            return View();
        }
        public ActionResult Dashboard()
        {
            var userId = Session["id"];
            var facultyStatus = false;
            var facultyInDb = _facultyContext.GetByUserId(Convert.ToInt64(userId));
            if (facultyInDb?.StudentCount >0)
            {
                facultyStatus = true;
            }
            var reports = _reportContext.GetReports(facultyInDb.Id);
            var viewModel = new FacultyDashboard
            {
                HasStudent = facultyStatus,
                Students = _studentContext.GetAll(),
                Supeprvisees = _studentContext.GetSupervisees(facultyInDb.Id),
                Faculty = facultyInDb,
                Reports = reports.Take(4)

            };

            return View(viewModel);
        }
        public ActionResult ViewStudents()
        {
            var students = _studentContext.GetAll();
            return View(students);
        }
        public ActionResult ViewAttached()
        {
            var userId = Convert.ToInt64(Session["id"]);
            var facultyId = _facultyContext.GetByUserId(userId).Id;

            var students = _studentContext.GetSupervisees(facultyId);
            return View(students);
        }
        public ActionResult Attach( int id)
        {
           
            var userId =Convert.ToInt64(Session["id"]);
            var studentInDb = _studentContext.Get(Convert.ToInt64(id));
            studentInDb.FacultyId = _facultyContext.GetByUserId(Convert.ToInt64(userId)).Id;
            var student = new Student
            {
                FacultyId = _facultyContext.GetByUserId(Convert.ToInt64(userId)).Id
            };
            var facultyInDb = _facultyContext.GetByUserId(userId);
            var faculty = new Faculty
            {
                StudentCount = facultyInDb.StudentCount++
            };
            facultyInDb.StudentCount++;
            _facultyContext.Update(faculty);
            _studentContext.Update(student);
            TempData["message"] = "Student Attached Successfully";

            return RedirectToAction("ViewStudents");
        }
    }
}