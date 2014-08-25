using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.ServiceModel.Syndication;
using System.Xml;

namespace PasswordGenerator.Activities
{

    public sealed class GetFeed : CodeActivity<SyndicationFeed>
    {
        // Define an activity input argument of type string
        public InArgument<Uri> FeedUrl { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override SyndicationFeed Execute(CodeActivityContext context)
        {
            return SyndicationFeed.Load(XmlReader.Create(FeedUrl.Get(context).ToString()));
        }
    }
}
