using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PasswordGenerator.Controllers
{
    public class FactorialsController : ApiController
    {
        // GET: api/Factorials/5
        public string Get(int id)
        {
            var factorialWF = new CFactorial();
            factorialWF.Num = id;
            var output = WorkflowInvoker.Invoke(factorialWF);
            return output["Factorial"].ToString();
        }

    }
}
