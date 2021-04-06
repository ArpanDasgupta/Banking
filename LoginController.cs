using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Banking.Models;
using BankingLib;

namespace Banking.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Verify()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Verify(Login lg)
        {
            LoginDAL lgdal = new LoginDAL();
            if (!lgdal.VerifyData(lg))
            {
                Response.Write("Login Failure");

            }
            else
            {
                var lglist = lgdal.GetLoginDetails();
                var dt = lglist.Where(x => x.Account_No == lg.Account_No).Select(x => x.LastLogin);
                
                if(dt.First() == null)
                {
                    lg.LastLogin = DateTime.Now;
                    lgdal.UpdateLoginDetails(lg);
                    return RedirectToAction("ChangePassword");
                }
                else
                {
                    RegisterDAL rd = new RegisterDAL();
                    var rmlist= rd.PeekDetails();
                    foreach(var item in rmlist)
                    {
                        if(item.Account_No==lg.Account_No)
                            Session["User"] = item;
                            break;
                    }
                    
                    return RedirectToAction("Index", "Customer");
                   
                }
            }
            return View();

        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

         [HttpPost]
        public ActionResult ChangePassword(Login lg)
        {
            LoginDAL lgdal = new LoginDAL();
            lgdal.changepassword(lg);
            return RedirectToAction("Register", "Register");
        }

       
    }
}