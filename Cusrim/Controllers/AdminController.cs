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
    public class AdminController : Controller
    {
        private readonly FacultyLogic _facultyContext = new FacultyLogic();
        private readonly StudentLogic _studentContext = new StudentLogic();
        private readonly ReportLogic _reportContext = new ReportLogic();
        private readonly CompanyLogic _companyContext = new CompanyLogic();


        // GET: Admin
        public ActionResult Index()
        {
            var viewModel = new AdminDashboard
            {
                Faculty = _facultyContext.GetAll(),
                Student = _studentContext.GetAll(),
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
        
        public ActionResult CompanyDetails(int id)
        {
            var company = _companyContext.Get(id);
            return View(company);
        }
        public ActionResult facultyDetails(int id)
        {
            var faculty = _facultyContext.Get(id);
            return View(faculty);
        }
        public ActionResult studentDetails(int id)
        {
            var student = _studentContext.Get(id);
            return View(student);
        }
        
    }
}