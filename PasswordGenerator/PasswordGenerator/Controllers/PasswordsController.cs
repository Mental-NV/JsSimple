using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PasswordGenerator.Controllers
{
    public class PasswordsController : ApiController
    {
        public IEnumerable<string> GetAll()
        {
            return new List<string>() { "AA1", "AA2", "AA3" };
        }
    }
}
