namespace Downloader.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Configuration;
    using Downloader.Models;

    public class DownloadsController : Controller
    {
        private CloudStorageAccount StorageAccount
        {
            get
            {
                if (_storageAccount == null)
                {
                    _storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                }
                return _storageAccount;
            }
        }
        private CloudStorageAccount _storageAccount;

        private CloudTable DownloadsTable
        {
            get
            {
                if (_downloadsTable == null)
                {
                    var tableClient = StorageAccount.CreateCloudTableClient();
                    _downloadsTable = tableClient.GetTableReference("downloads");
                    _downloadsTable.CreateIfNotExists();
                    FileTableWithSampleDataIfEmpty(DownloadsTable);
                }
                return _downloadsTable;
            }
        }
        private CloudTable _downloadsTable;

        private string GetUserId()
        {
            return HttpContext != null && HttpContext.User != null && HttpContext.User.Identity != null ? HttpContext.User.Identity.Name : null;
        }

        private void FileTableWithSampleDataIfEmpty(CloudTable DownloadsTable)
        {
            if (!DownloadsTable.ExecuteQuery(new TableQuery<DownloadTaskInfo>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, GetUserId()))).Any())
            {
                List<DownloadTaskInfo> sampleDataList = new List<DownloadTaskInfo>() { 
                    new DownloadTaskInfo() { Url = "http://cs609530.vk.me/u259703126/docs/1ceeed1490ff/file_5.gif" },
                    new DownloadTaskInfo() { Url = "http://cs539411.vk.me/u2000013787/docs/8b78bfda3fee/file.gif" },
                    new DownloadTaskInfo() { Url = "http://cs6078.vk.me/u16000159/docs/cee33cdea87f/file.gif" } 
                };

                var batch = new TableBatchOperation();

                foreach (var d in sampleDataList)
                {
                    d.Id = Guid.NewGuid().ToString();
                    d.UserId = GetUserId();
                    d.File = d.Url;
                    d.IsCanceled = false;
                    d.IsFinished = true;
                    d.Progress = 100;

                    batch.InsertOrReplace(d);
                }

                DownloadsTable.ExecuteBatch(batch);
            }
        }

        // GET: Downloads
        public ActionResult Index()
        {
            var query = DownloadsTable.ExecuteQuery(new TableQuery<DownloadTaskInfo>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, GetUserId())));
            return View(query.ToList());
        }

        // GET: Downloads/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Downloads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Downloads/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Downloads/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Downloads/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Downloads/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Downloads/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
