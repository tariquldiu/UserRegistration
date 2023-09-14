using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserRegistration.Models;

namespace UserRegistration.Controllers
{
    
    public class UsersController : Controller
    {
        private UserDbContext context = new UserDbContext();
        // GET: Users
        public ActionResult Index()
        {
            //Enable commented code is to test a error.
            //string msg = null;
            //ViewBag.Message = msg.Length;

            return View(context.Users.ToList());
        }

       

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FullName,Email,MobileNumber,Address,UserName,Password")] User user)
        {
            try
            {
                string validationMsg = CheckValidation(user);
                if(validationMsg != string.Empty)
                {
                    TempData["msg"] = validationMsg;
                    return View();
                }
                User uObj = context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                if(uObj == null )
                {
                    string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "SHA1");
                    if (ModelState.IsValid)
                    {
                        user.Password = pass;
                        context.Users.Add(user);
                        context.SaveChanges();
                        TempData["msg"] = "User Registration Completed.";
                        RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["msg"] = "The user " + user.UserName + " already exist. Please try another.";
                    return View();
                }
               
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
                return View();
            }
            return RedirectToAction("Create");
        }

        private string CheckValidation(User user)
        {
            string msg = string.Empty;
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");
            //check empty
            if (user.FullName == null)
            {
                msg = "Please input the full name.";
            }
            if (user.Email == null)
            {
                msg = "Please input the email.";
            }
            if (user.MobileNumber == null)
            {
                msg = "Please input the mobile number.";
            }
            if (user.Address == null)
            {
                msg = "Please input the address.";
            }
            if (user.UserName == null)
            {
                msg = "Please input the user name.";
            }
            if (user.Password == null)
            {
                msg = "Please input the password.";
            }
            //check length
            if (user.UserName.Length < 6)
            {
                msg = "The username is at least 6 characters long.";
            }
            if (user.Password.Length < 8)
            {
                msg = "The username is at least 6 characters long.";
            }
            //check special character
            if (!regexItem.IsMatch(user.FullName))
            {
                msg = "Special character should not be entered for full name.";
            }
            if (!regexItem.IsMatch(user.Address))
            {
                msg = "Special character should not be entered for address.";
            }
            if (!regexItem.IsMatch(user.UserName))
            {
                msg = "Special character should not be entered for user name.";
            }
            return msg;
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //Log the error!!
            filterContext.ExceptionHandled = true;

            //return specific view
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Error/InternalError.cshtml"
            };


        }
    }
}
