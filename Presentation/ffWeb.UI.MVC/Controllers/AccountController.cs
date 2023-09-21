
using DotNetOpenAuth.AspNet;
using fanikiwaGL.Entities;
using fCommon.Utility;
using ffWeb.UI.MVC.Filters;
using ffWeb.UI.MVC.Models;
using fPeerLending.Business;
using fPeerLending.Entities;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ffWeb.UI.MVC.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    [HandleError]
    public class AccountController : Controller
    {

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && model.UserName != null)
            {
                RegistrationComponent rc = new RegistrationComponent();
                string email = model.UserName.Trim();
                Member member = rc.GetMemberByEmail(email);
                if (member != null)
                {
                    if (member.Status == "C")
                    {
                        // If we got this far, the user is deregistered 
                        ModelState.AddModelError("", "Sorry looks like you are already DeRegistered. Register first.");
                        return View(model); 
                    }
                }
            }
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form  
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [AllowAnonymous]
        public bool LoginViaPhone(string UserName, string Password)
        {
            bool isautheticated = false;
            if (ModelState.IsValid && WebSecurity.Login(UserName, Password, persistCookie: true))
            {
                isautheticated = true;
            }
            return isautheticated;
        }

        //
        // POST: /Account/LogOff 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register 
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model.InformBy)
                {
                    case "SMS":
                        if (model.Telephone == null)
                        {
                            ModelState.AddModelError("", "Phone cannot be null if you plan to be informed via SMS.");

                            return View(model);
                        }
                        break;
                }

                if (model.TermsAccepted == false)
                {
                    ModelState.AddModelError("", "You must accept Terms and Conditions.");

                    return View(model);
                }



                /*
     * Register to Fanikiwa via web
     * 1. If (the member is registered via sms)
     * 2.   Update the registration to include web
     * 3. Else Register for web
     */
                 
                RegistrationComponent rc = new RegistrationComponent();

                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);

                //register in Fanikiwa
                Member member = new Member();
                if (model.UserName != null)
                {
                    member.Email = model.UserName.ToLower();
                }
                member.Surname = Utils.ConvertFirstLetterToUpper(model.SurName);
                member.Status = "N";
                member.DateActivated = DateTime.Today;
                member.DateJoined = DateTime.Today;
                member.InformBy = model.InformBy;
                member.Telephone = Utils.ConvertFirstLetterToUpper(model.Telephone);
                member.Pwd = model.Password;

                Member _registeredMember = rc.Register(member);
                string msg = String.Format("Registration successful. \nMember Id: [ {0} ], \nCurrent Account Id: [ {1} ], \nLoan Account Id: [ {2} ], \nInvestment Account Id: [ {3} ] ", _registeredMember.MemberId, _registeredMember.CurrentAccountId, _registeredMember.LoanAccountId, _registeredMember.InvestmentAccountId);

                ffWeb.UI.MVC.Helpers.Messager.Inform(_registeredMember, msg);

                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("Index", "Home");
        }

        //
        // POST: /Account/Disassociate 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });

        }

        //
        // GET: /Account/Manage 
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();

        }

        //
        // POST: /Account/Manage 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        WebSecurity.Logout();

                        return RedirectToAction("Login", new { returnUrl = "" });

                        //return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);

        }

        //
        // POST: /Account/ExternalLogin 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));

        }

        //
        // GET: /Account/ExternalLoginCallback 
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }

        }

        //
        // POST: /Account/ExternalLoginConfirmation 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        { 
            RegistrationComponent rg = new RegistrationComponent();

            switch (model.InformBy)
            {
                case "SMS":
                    if (model.Telephone == null)
                    {
                        // If we got this far, user has selected sms as mode of inform by and telephone is null     
                        ModelState.AddModelError("", "Phone cannot be null if you plan to be informed via SMS!");
                        return View(model);
                    }
                    break;
            }

            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        //register in Fanikiwa
                        Member member = new Member();
                        member.Email = model.UserName;
                        member.Surname = Utils.ConvertFirstLetterToUpper(model.SurName);
                        member.Status = "N";
                        member.DateActivated = DateTime.Today;
                        member.DateJoined = DateTime.Today;
                        member.InformBy = model.InformBy;
                        member.Telephone = Utils.ConvertFirstLetterToUpper(model.Telephone);

                        Member _registeredMember = rg.Register(member);
                         
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);

        }

        //
        // GET: /Account/ExternalLoginFailure 
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();

        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);

        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);

        }

        [Authorize]
        public ActionResult EditProfile()
        {
            string email = User.Identity.Name;
            RegistrationComponent rc = new RegistrationComponent();
            Member model = rc.GetMemberByEmail(email);
            if (model.DateOfBirth == null)
            {
                model.DateOfBirth = DateTime.Now;
            }
            return View( model);

        }

        [HttpPost]
        public ActionResult EditProfile([Bind] Member model)
        {
            if (model.DateOfBirth != null)
            {
                DateTime parsable_date;
                if (!DateTime.TryParse(model.DateOfBirth.ToString(), out parsable_date))
                {
                    // If we got this far, user has entered an invalid date
                    ModelState.AddModelError("", "Date of Birth is Incorrect format.Try { MM/dd/YYYY }!");
                    return View("EditProfile", model);
                }
            }
            switch (model.InformBy)
            {
                case "SMS":
                    if (model.Telephone == null)
                    {
                        // If we got this far, user has selected sms as mode of inform by and telephone is null     
                        ModelState.AddModelError("", "Phone cannot be null if you plan to be informed via SMS!");
                        return View("EditProfile", model);
                    }
                    break;
            } 
                //update the member
                RegistrationComponent rc = new RegistrationComponent();

                Member member = rc.GetMemberByEmail(model.Email);
                member.Surname = Utils.ConvertFirstLetterToUpper(model.Surname);
                member.OtherNames = Utils.ConvertFirstLetterToUpper(model.OtherNames);
                member.DateOfBirth = model.DateOfBirth;
                member.Gender = model.Gender;
                member.Telephone = Utils.ConvertFirstLetterToUpper(model.Telephone);
                member.RefferedBy = model.RefferedBy;
                member.InformBy = model.InformBy;

                rc.UpdateMember(member);

                return View("_ManageProfileSucess"); 
        }
        //[Authorize]
        public ActionResult UploadMemberImage_Edit(int id)
        {
            Session["MemberId"] = null;
            Session["MemberId"] = id;

            return RedirectToAction("UploadMemberImage");

        }
        //[Authorize]
        public ActionResult UploadMemberImage()
        {
            return View("UploadImagesView");

        }
        [HttpPost]
        public ActionResult UploadMemberImage(HttpPostedFileBase HttpPostedLogo)
        {
            RegistrationComponent rc = new RegistrationComponent();

            string fileNameWithPath = null;
            string fileName = null;
            string physicalPath = Server.MapPath("~/Images/");

            //Ensure that the user has selected a file
            if (HttpPostedLogo != null && HttpPostedLogo.ContentLength > 0)
            {
                fileName = System.IO.Path.GetFileName(HttpPostedLogo.FileName);

                fileNameWithPath = System.IO.Path.Combine(physicalPath, fileName);

                // save image in folder
                HttpPostedLogo.SaveAs(fileNameWithPath);
            }

            string _SessionMemberId = Session["MemberId"].ToString();
            int _MemberId = int.Parse(_SessionMemberId);

            Member member = rc.GetMemberByID(_MemberId);
            member.Photo = fileName;
            rc.UploadMemberImage(member);

            return RedirectToAction("EditProfile");

        }
        [Authorize]
        public ActionResult MyAccounts()
        {
            string email = User.Identity.Name;
            RegistrationComponent rc = new RegistrationComponent();
            Member member = rc.GetMemberByEmail(email);

            AccountsComponent ac = new AccountsComponent();
            List<Account> accounts = ac.GetMemberAccounts(member.MemberId);
            return View(accounts);
        } 
        [Authorize]
        public ActionResult Statement(int AccountID)
        { 
            TransactionsComponent tc=new TransactionsComponent();
             
            DateTime _startdate = DateTime.Now.Date.AddMonths(-3);
            DateTime _enddate = DateTime.Now.Date;

            List<TransactionModel> model = tc.GetAccountViewStatement(AccountID, _startdate, _enddate);
            return View(model); 
        }
        [Authorize]
        public ActionResult MiniStatemement(int AccountID)
        {
            TransactionsComponent tc = new TransactionsComponent();

            DateTime _startdate = DateTime.Now.Date.AddMonths(-3);
            DateTime _enddate = DateTime.Now.Date;

            List<TransactionModel> model = tc.GetAccountViewStatement(AccountID, _startdate, _enddate);
            return View(model);
        }
        [Authorize]
        public ActionResult GetLoanAccountTransactions(AccountsDetsModel accountsmodel)
        {
            RegistrationComponent rc = new RegistrationComponent();
            DepositComponent dc = new DepositComponent();

            int _accountid = accountsmodel.LoanAccid;
            DateTime _startdate = DateTime.Now.Date.AddMonths(-3);
            DateTime _enddate = DateTime.Now.Date;


            return RedirectToAction("GetAccountTransactions", new { account = _accountid, startdate = _startdate, enddate = _enddate });

        }
        [Authorize]
        public ActionResult GetInvestmentAccountTransactions(AccountsDetsModel accountsmodel)
        {
            RegistrationComponent rc = new RegistrationComponent();
            DepositComponent dc = new DepositComponent();

            int _accountid = accountsmodel.InvestmentAccid;
            DateTime _startdate = DateTime.Now.Date.AddMonths(-3);
            DateTime _enddate = DateTime.Now.Date;


            return RedirectToAction("GetAccountTransactions", new { account = _accountid, startdate = _startdate, enddate = _enddate });

        }

        [Authorize]
        public ActionResult Scores()
        {
            RegistrationComponent rc = new RegistrationComponent();
            ListOffersComponent lc = new ListOffersComponent();
            StatisticsModel model = new StatisticsModel();

            string email = User.Identity.Name;
            Member member = rc.GetMemberByEmail(email);

            model.BorrowOffersToMe = lc.ListBorrowOffers(member).Count();
            model.BorrowOffersByMe = lc.ListMyBorrowOffers(member).Count();
            model.LendOffersToMe = lc.ListLendOffers(member).Count();
            model.LendOffersByMe = lc.ListMyOffers(member).Count();

            return View("_ManageProfilePartial", model);

        }

        [Authorize]
        public ActionResult DeRegister()
        {

            RegistrationComponent rc = new RegistrationComponent();
            RegisterModel rm = new RegisterModel();
            string email = User.Identity.Name;
            Member member = rc.GetMemberByEmail(email);
            rc.DeRegister(member);
            WebSecurity.Logout();

            return RedirectToAction("Register");

        }
        [HttpPost]
        public ActionResult DeRegister([Bind] RegisterModel model)
        {
            RegistrationComponent rc = new RegistrationComponent();
            RegisterModel rm = new RegisterModel();
            string email = User.Identity.Name;
            Member member = rc.GetMemberByEmail(email);
            rc.DeRegister(member);
            WebSecurity.Logout();

            ffWeb.UI.MVC.Helpers.Messager.Inform(member, "DeRegistration successful. \n All Open Offers have been closed and Accounts have been Closed.");

            return RedirectToAction("Register");
        }
         

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
        
    }
}