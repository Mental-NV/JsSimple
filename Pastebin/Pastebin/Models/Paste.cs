using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pastebin.Models
{
    public class Paste
    {
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        public string Content { get; set; }
        public string UserId { get; set; }
        public string PasteId { get; set; }
        public DateTime Created { get; set; }
    }
}