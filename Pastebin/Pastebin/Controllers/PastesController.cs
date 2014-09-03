namespace Pastebin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Pastebin.Models;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Configuration;

    public class PastesController : Controller
    {
        private CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

        private CloudTable PastesTable
        {
            get
            {
                if (_pastesTable == null)
                {
                    var tableClient = storageAccount.CreateCloudTableClient();
                    _pastesTable = tableClient.GetTableReference("pastes");
                    _pastesTable.CreateIfNotExists();
                }
                return _pastesTable;
            }
        }
        private CloudTable _pastesTable;

        // GET: Pastes
        [AllowAnonymous]
        public ActionResult Index()
        {
            var pastes = PastesTable.ExecuteQuery(new TableQuery<Paste>()).ToList();
            return View(pastes);
        }

        // GET: Pastes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paste = PastesTable.CreateQuery<Paste>().Where(x => x.PasteId == id).ToList().FirstOrDefault();
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

            paste.PartitionKey = paste.UserId;
            paste.RowKey = paste.PasteId;

            var op = TableOperation.Insert(paste);
            PastesTable.Execute(op);

            return RedirectToAction("Index");
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

            var paste = PastesTable.CreateQuery<Paste>().Where(x => x.PasteId == id).ToList().FirstOrDefault();

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
            paste.RowKey = paste.PasteId;
            paste.PartitionKey = paste.UserId;
            PastesTable.Execute(TableOperation.InsertOrReplace(paste));
            return RedirectToAction("Index");
        }

        // GET: Pastes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var paste = PastesTable.CreateQuery<Paste>().Where(x => x.PasteId == id).ToList().FirstOrDefault();

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
            var paste = PastesTable.CreateQuery<Paste>().Where(x => x.PasteId == id).ToList().FirstOrDefault();
            paste.RowKey = paste.PasteId;
            paste.PartitionKey = paste.UserId;
            var op = TableOperation.Delete(paste);
            PastesTable.Execute(op);
            
            return RedirectToAction("Index");
        }
    }
}
