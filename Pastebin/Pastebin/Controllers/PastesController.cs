using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pastebin.Models;

namespace Pastebin.Controllers
{
    public class PastesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pastes
        public ActionResult Index()
        {
            return View(db.Pastes.ToList());
        }

        // GET: Pastes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paste paste = db.Pastes.Find(id);
            if (paste == null)
            {
                return HttpNotFound();
            }
            return View(paste);
        }

        // GET: Pastes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pastes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PasteId,Title,Content,UserId,Created")] Paste paste)
        public ActionResult Create([Bind(Include = "Title,Content")] Paste paste)
        {
            paste.PasteId = Guid.NewGuid().ToString();
            paste.UserId = GetUserId();
            paste.Created = DateTime.Now;
            if (string.IsNullOrEmpty(paste.Title))
            {
                paste.Title = "Untitled";
            }
            
            if (ModelState.IsValid)
            {
                db.Pastes.Add(paste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paste);
        }

        private string GetUserId()
        {
            return HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : null;
        }

        // GET: Pastes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paste paste = db.Pastes.Find(id);
            if (paste == null)
            {
                return HttpNotFound();
            }
            return View(paste);
        }

        // POST: Pastes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PasteId,Title,Content,UserId,Created")] Paste paste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paste);
        }

        // GET: Pastes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paste paste = db.Pastes.Find(id);
            if (paste == null)
            {
                return HttpNotFound();
            }
            return View(paste);
        }

        // POST: Pastes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Paste paste = db.Pastes.Find(id);
            db.Pastes.Remove(paste);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
