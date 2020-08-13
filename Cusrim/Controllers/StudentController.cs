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

        private readonly FacultyLogic _facultyContext = new FacultyLogic();
        private readonly ReportLogic _reportContext = new ReportLogic();
        private readonly CompanyLogic _companyContext = new CompanyLogic();


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
                var status = _studentContext.GetByUserId(curUser.Id).profile_status;
                if ((string)Session["Role"] == $"student" && status )
                {
                    return RedirectToAction("Dashboard", "Student");
                }
                else if ((string)Session["Role"] == $"student" && !status )
                {
                    return RedirectToAction("Info", "Student");
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
        public ActionResult Report(Report report, HttpPostedFileBase file)
        {
            var userId= Convert.ToInt64(Session["id"]);
            var student = _studentContext.GetByUserId(userId);
            if (file != null)
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Images/")
                                                      + file.FileName);
                report.ImageUrl = file.FileName;
            }
            report.CreatedAt = DateTime.Now;
            report.StudentId = student.Id;
            report.FacultyId = student.FacultyId;
            _reportContext.Save(report);
            //db.Image.Add(img);
            //db.SaveChanges();
            return RedirectToAction("Dashboard");
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
                        UserId = _context.GetLast()
                    };
                    _studentContext.Save(student);

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
            if (!matricIsUnique)
                ModelState.AddModelError("EmailExist", "This Matric Number has been registered");
            return View();
        }
        public ActionResult Info()
        {
            return View();  
        }

        public ActionResult Save(Student student)
        {
            var userId = Session["id"];

            var studentInDb = _studentContext.GetByUserId( Convert.ToInt64(userId));
            studentInDb.Grade = student.Grade;
            studentInDb.Level = student.Level;
            studentInDb.Name = student.Name;
            studentInDb.Department = student.Department;
            studentInDb.profile_status = true;
            student.profile_status = true;
            _studentContext.Update(student);

            return RedirectToAction("Dashboard");
        }
        public ActionResult ViewCompany()
        {
            var company = _companyContext.GetAll();
            return View(company);
        }
        public ActionResult Dashboard()
        {
                var userId = Session["id"];
                var facultyStatus = false;
                var studentInDb = _studentContext.GetByUserId(Convert.ToInt64(userId));
                var faculty = new Faculty();
               if(studentInDb?.FacultyId != null)
                {
                    facultyStatus = true;
                    faculty = _facultyContext.Get(Convert.ToInt64(studentInDb.FacultyId));
                }
            var reports = _reportContext.GetByStudentId(studentInDb.Id);
            var viewModel = new StudentDashboard
            {
                HasStaff = facultyStatus,
                Student = studentInDb,
                Faculty = faculty,
                Report = new Report(),
                Reports = reports,

                Companies = _companyContext.GetAll()
                   
             
                };

                return View(viewModel);
        }
        public ActionResult ViewReport()
        {
            return View();
        }
        public ActionResult ReportDetails(int id)
        {
            var report = _reportContext.Get(id);
            return View(report);
        }
    }
}