using Cusrim.Core.Models;
using Cusrim.Core.ViewModels;
using Cusrim.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Cusrim.Controllers
{
    public class AdminController : Controller
    {
        private readonly FacultyLogic _facultyContext = new FacultyLogic();
        private readonly StudentLogic _studentContext = new StudentLogic();
        private readonly ReportLogic _reportContext = new ReportLogic();
        private readonly UserLogic _context = new UserLogic();
        private readonly CompanyLogic _companyContext = new CompanyLogic();


        // GET: Admin
        public ActionResult Index()
        {
            var viewModel = new AdminDashboard
            {
                Faculties = _facultyContext.GetAll(),
                Students = _studentContext.GetAll(),
                Report = _reportContext.GetAllReports(),
                Company = _companyContext.GetAll()

            };
            return View(viewModel);
        }
        public ActionResult Save(Company company)
        {
            company.CreatedAt = DateTime.Now;
            _companyContext.Save(company);

            return RedirectToAction("Index");
        }
        public ActionResult SaveStudent(Student student)
        {
            
            var matricIsUnique = _studentContext.MatricIsUnique(student.MatricNo);
            if (matricIsUnique)
            {
                var user = new User
                {
                    Username = student.MatricNo,
                    password = "password",
                    UserRole = "student",
                    CreatedAt = DateTime.Now
                };
                if (_context.Save(user))
                {
                    student.UserId = _context.GetLast();
                    _studentContext.Save(student);
                }
                TempData["success"] = "Registration Successful";

            
                return RedirectToAction("Index");
            }
            if (!matricIsUnique)
                TempData["message"] = "This Matric Number has been registered";

            return RedirectToAction("Index");
        }
        public ActionResult SaveFaculty(Faculty faculty)
        {

            var staffIsUnique = _facultyContext.StaffNoIsUnique(faculty.StaffNo);
            if (staffIsUnique)
            {
                var user = new User
                {
                    Username = faculty.StaffNo,
                    password = "password",
                    UserRole = "faculty",
                    CreatedAt = DateTime.Now
                };
                if (_context.Save(user))
                {

                    faculty.UserId = _context.GetLast();
                    _facultyContext.Save(faculty);

                }
            TempData["success"] = "Registration Successful";

            return RedirectToAction("Index");

        }
            if (!staffIsUnique)
                TempData["message"] = "This Matric Number has been registered";
            return RedirectToAction("Index");
        }

        public ActionResult CompanyDetails(int id)
        {
            var company = _companyContext.Get(id);
            return View(company);
        }
        public ActionResult facultyDetails(int id)
        {
            var faculty = _facultyContext.Get(id);
            var viewModel = new FacultyDetails
            {
                Faculty = faculty,
                Students =_studentContext.GetSupervisees(faculty.Id)
            };
            
            return View(viewModel);
        }
        public ActionResult studentDetails(int id)
        {
            var student = _studentContext.Get(id);
            return View(student);
        }
        public ActionResult ViewFaculty()
        {
            var faculty = _facultyContext.GetAll();
            return View(faculty);
        }
        public ActionResult ViewStudent()
        {
            var student = _studentContext.GetAllStudents();
            return View(student);
        }
        public ActionResult ViewCompany() {
            var company = _companyContext.GetAll();
            return View(company);
        }
        public ActionResult Attach(int id)
        {
            var faculty = _facultyContext.Get(id);
            var students = _studentContext.GetAllUnassigned();
            var selectedViewModel = new AttachViewModel
            {
                Faculty = faculty,
                Students = students

            };
            return View("AttachForm", selectedViewModel);


          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AttachStudent(Student student)
        {
            var facultyId = Convert.ToInt64(Request.Form["facultyId"]);
            var studentInDb = _studentContext.Get(Convert.ToInt64(student.Id));
            studentInDb.FacultyId = facultyId;
            
            var facultyInDb = _facultyContext.Get(facultyId);

            facultyInDb.StudentCount++;
            _facultyContext.Update(facultyInDb);
            _studentContext.Update(student);
            TempData["message"] = "Student Attached Successfully";

            return RedirectToAction("ViewFaculty");

        }
       public ActionResult Unassign(int id)
        {
            var student = new Student();
            var studentInDb = _studentContext.Get(id);
            var facultyInDb = _facultyContext.Get(Convert.ToInt64(studentInDb.FacultyId));
            if (facultyInDb.StudentCount > 0)
            {
                facultyInDb.StudentCount--;
                _facultyContext.Update(facultyInDb);
            }
            studentInDb.FacultyId = null;
            _studentContext.Update(student);
            TempData["message"] = "Student Unassigned Successfully";
            return RedirectToAction("ViewFaculty");


        }

    }
}