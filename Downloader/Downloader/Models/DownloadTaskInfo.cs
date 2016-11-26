using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Downloader.Models
{
    public class DownloadTaskInfo : TableEntity
    {
        public DownloadTaskInfo()
            : base()
        {
            // blank
        }

        public DownloadTaskInfo(string userId, string url)
            : base(userId, url)
        {
        }

        public string UserId
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }

        public string Id
        {
            get { return RowKey; }
            set { RowKey = value; }
        }

        public string Url { get; set; }

        public int Progress { get; set; }

        public bool IsFinished { get; set; }

        public bool IsCanceled { get; set; }

        public string Error { get; set; }
        [DataType(DataType.Url)]
        public string File { get; set; }
    }
}