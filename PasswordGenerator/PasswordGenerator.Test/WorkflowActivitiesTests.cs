namespace PasswordGenerator.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PasswordGenerator.Activities;
    using System.Activities;
    using System.ServiceModel.Syndication;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class WorkflowActivitiesTests
    {
        [TestMethod]
        public void FactorialTest()
        {
            var fact = new CFactorial();
            fact.Num = 5;
            var result = WorkflowInvoker.Invoke(fact);
            Assert.AreEqual(120, Convert.ToInt32(result["Factorial"]));
        }

        [TestMethod]
        public void RssFeedTest()
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

            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(result[0]));
        }
    }
}
