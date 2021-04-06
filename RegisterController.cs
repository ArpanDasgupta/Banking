using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banking.Models;
using BankingLib;

namespace Banking2.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        ///public ActionResult Index()
        //{
        //    return View();
        //}


        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Registration model)
        {
            Registration details = new Registration();

            RegisterDAL dal = new RegisterDAL();

            if (ModelState.IsValid)
            {
                dal.GetDetails(model);

                return RedirectToAction("Verify","Login");
            }
            return View();
        }
    }
}