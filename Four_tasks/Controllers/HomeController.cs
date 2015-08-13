using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Four_tasks.Models;
using Four_tasks.Repository;
using System.Threading.Tasks;

namespace Four_tasks.Controllers
{
    public class HomeController : Controller
    {
        const int contactsOnPage = 3;
        static List<IdComment> idComments = new List<IdComment>(0);

        public async Task<ActionResult> Index(int page = 1)
        {            
            ViewBag.Page = page;
            ViewBag.contactsOnPage = contactsOnPage;
            var contacts = await (new ContactJsonFile()).readAsync();
            ViewBag.PageCount = contacts.Count / contactsOnPage + ((contacts.Count % contactsOnPage > 0) ? 1 : 0);
            if (page > ViewBag.PageCount) page = ViewBag.PageCount;            
            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.contacts = contacts.Skip((page - 1) * contactsOnPage).Take(contactsOnPage);
                return Json(new { contacts = ViewBag.contacts, contactsOnPage = contactsOnPage,
                                  page = page, pageCount = ViewBag.PageCount }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.comments = idComments;
                ViewBag.contacts = contacts;
                return View();
            }
        }

        public ActionResult Add_contact()
        {
            ViewBag.comments = idComments;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add_contact(Contact contact)
        {
            await (new ContactJsonFile()).writeAsync(contact);
            return RedirectToAction("Index");
        }

        #region JqueryAjax
        [HttpPost]
        public ActionResult JqueryAjax(IdComment idComment)
        {
            idComments.Add(idComment);
            if (Request.IsAjaxRequest())
            {
                return Json(idComments);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
