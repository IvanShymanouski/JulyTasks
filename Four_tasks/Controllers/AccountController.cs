using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Four_tasks.Models;
using Four_tasks.Authentification;
using Four_tasks.Providers;
using ORM;

namespace Four_tasks.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            ActionResult result = (HttpContext.Request.IsAjaxRequest()) ?
                                    (ActionResult)Json("Error", JsonRequestBehavior.AllowGet) :
                                    RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                User user = CustomMembershipProvider.ValidateUserAndReturn(model.Email, model.Password);
                if (null != user)
                {
                    var setCockie = DependencyResolver.Current.GetService<ICustomAuthenticationService>();
                    setCockie.SignIn(new Identity(user), model.RememberMe);
                    result = (HttpContext.Request.IsAjaxRequest()) ?
                             (ActionResult)Json("Hello, " + model.Email, JsonRequestBehavior.AllowGet) :
                                    RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return result;            
        }

        [HttpPost]        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            ActionResult result = (HttpContext.Request.IsAjaxRequest()) ?
                                    (ActionResult)Json("Error", JsonRequestBehavior.AllowGet) :
                                    RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                User user = CustomMembershipProvider.CreateUser(model.Email, model.Password);
                if (null != user)
                {                    
                    var setCockie = DependencyResolver.Current.GetService<ICustomAuthenticationService>();
                    setCockie.SignIn(new Identity(user), false);
                    result = (HttpContext.Request.IsAjaxRequest()) ?
                             (ActionResult)Json("Hello, " + model.Email, JsonRequestBehavior.AllowGet) :
                                    RedirectToAction("Index", "Home");         
                }
                else
                {
                    ModelState.AddModelError("", "Registration error, this login or email already exist");
                }
            }

            return result;
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
    }   
}
