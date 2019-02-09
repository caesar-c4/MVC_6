using MVC_6.Model_View;
using MVC_6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_6.Controllers
{
    public class ExamController : Controller
    {
        //
        // GET: /Exam/
        newdataEntities db = new newdataEntities();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Loaddata()
        {
            List<Student> lst = db.Students.OrderByDescending(d => d.ID).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult savedata(StudentModel stu)
        {
            var student1 = new Student
            {
                Name = stu.Name,
                Email = stu.Email
            };

            if (ModelState.IsValid)
            {
                db.Students.Add(student1);
                db.SaveChanges();
                return Json(new { success = true, data = stu });
            }
            return Json(new { success = false});

                           
        }

        public JsonResult finddata(int id)
        {
            List<Student> lst = db.Students.Where(d => d.ID == id).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public JsonResult updatedata(StudentModel stu)
        {
            var student1 = new Student
            {
                ID=stu.ID,
                Name = stu.Name,
                Email = stu.Email
            };

            if (ModelState.IsValid)
            {
                db.Entry(student1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, data = stu });
            }
            return Json(new { success = false });
        }
        public JsonResult deletedata(int id)
        {
            Student lst = db.Students.Where(d => d.ID == id).SingleOrDefault() as Student;
            db.Students.Remove(lst);
            db.SaveChanges();
            return Json(lst, JsonRequestBehavior.AllowGet);

        }
        public ActionResult login()
        {
            return View("login");
        }

        public ActionResult check()
        {
            if ((Request.Form["name"] == "sabuj") && (Request.Form["pass"] == "123"))
            {
                FormsAuthentication.SetAuthCookie((Request.Form["name"]), true);
                return View("Index");
            }
            return View("login");
        }

        public ActionResult logout()
        {


            FormsAuthentication.SignOut();
              
            
            return View("login");
        }

        
	}
}