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
                    RegisterModel rm = new RegisterModel();
                    var rmlist= rd.PeekDetails();
                    foreach(var item in rmlist)
                    {
                        if (item.Account_No == lg.Account_No)
                        {
                            rm.Account_Name = item.Account_Name;
                            rm.Account_No = item.Account_No;
                            rm.Address = item.Address;
                            rm.Bank_Branch = item.Bank_Branch;
                            rm.Email = item.Email;
                            rm.IFSC = item.IFSC;
                            rm.Mobile_No = item.Mobile_No;
                            rm.Password = item.Password;
                            Session["User"] = rm;
                            break;
                        }
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