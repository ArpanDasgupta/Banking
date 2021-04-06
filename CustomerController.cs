using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingLib;
using Banking.Models;
namespace Banking.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var rm = (RegisterModel)Session["User"];
           
            ViewBag.User = rm.Account_Name;
            return View(rm);
        }
    }
}