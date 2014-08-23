using PasswordGenerator.WorkflowActivites;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Web.Http;

namespace PasswordGenerator.Controllers
{
    public class RssController : ApiController
    {
        // GET: api/Rss
        public IEnumerable<string> Get()
        {
            List<Uri> feedUrls = new List<Uri>()
            {
                new Uri("http://www.microsoft.com/ru-ru/news/rss/rssfeed.aspx"),
                new Uri("http://blogs.msdn.com/b/rudevnews/rss.aspx")
            };

            var feedResult = new List<SyndicationFeed>();

            WorkflowInvoker.Invoke(new RssFeed(), new Dictionary<string, object>() 
            { 
                {"FeedUrls", feedUrls},
                {"FeedResult", feedResult}
            });

            var result = new List<string>();
            foreach (var item in feedResult)
            {
                result.AddRange(item.Items.Select(x => x.Title.Text));
            }
            return result;
        }
    }
}
