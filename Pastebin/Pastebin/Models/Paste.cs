using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebin.Models
{
    public class Paste : TableEntity
    {
        public Paste(string UserId, string PasteId) : base()
        {
            this.UserId = PartitionKey = UserId;
            this.PasteId = RowKey = PasteId;
        }

        public Paste()
        {
            // blank
        }

        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }
        public string UserId { get; set; }
        public string PasteId { get; set; }
        public DateTime Created { get; set; }
    }
}