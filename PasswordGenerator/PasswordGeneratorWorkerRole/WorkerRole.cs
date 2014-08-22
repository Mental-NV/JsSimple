using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using PasswordGenerator;
using System.Data.Entity;

namespace PasswordGeneratorWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private const int _maxLogSetCount = (int)(_normalLogSetCount * 1.5);
        private const int _normalLogSetCount = 1000000;

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("PasswordGeneratorWorkerRole entry point called");

            while (true)
            {
                Thread.Sleep(100000);
                Trace.TraceInformation("Working");

                try
                {
                    using (var logContext = new LogContext())
                    {
                        var count = logContext.LogSet.Count();

                        Trace.TraceInformation(string.Format("There are {0} items", count));

                        if (count > _maxLogSetCount)
                        {
                            var firstToDelete = logContext.LogSet.OrderByDescending(x => x.When).Skip(_normalLogSetCount).FirstOrDefault();
                            if (firstToDelete != null)
                            {
                                var toDelete = logContext.LogSet.Where(x => x.When <= firstToDelete.When);
                                logContext.LogSet.RemoveRange(toDelete);
                                var removedCount = logContext.SaveChanges();
                                Trace.TraceInformation(string.Format("{0} items were deleted", removedCount));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error occurred when logs were cleared: {0}", ex);
                }
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            Trace.TraceInformation("PasswordGeneratorWorkerRole is before starting");

            return base.OnStart();
        }
    }
}
