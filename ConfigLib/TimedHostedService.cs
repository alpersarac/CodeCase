
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;

namespace ConfigLib
{

    public class TimedHostedService : ApiController
    {
        static AllYouPlayEntities allYouPlayEntities;
        List<Configuration> AppConfig;
        string ConnectionString = "";
        [HttpPost]
        public IHttpActionResult ConfigurationReader(string applicationName,string connectionString,int refreshTimerIntervalInMs)
        {
            //ConfigurationReader(applicationName,connectionString,refreshTimerIntervalInMs);
            Debug.WriteLine("Threads have started.");
            ConnectionString = connectionString;
            HostingEnvironment.QueueBackgroundWorkItem(ct => FireAndForgetMethodAsync(applicationName, connectionString, refreshTimerIntervalInMs));

            return Ok("OK");
        }
        public static dynamic GetValue(string Key)
        {
            try
            {
                
                var IncomingValue = (from p in allYouPlayEntities.Configurations where p.Name == Key&& p.IsActive == true select p).FirstOrDefault();
                if (IncomingValue.Type.ToLower()=="int")
                {
                    return (Convert.ToInt32(IncomingValue.Value));
                }
                else if (IncomingValue.Type.ToLower() == "boolean")
                {
                    return IncomingValue.Value.Equals("1") ? true : false;
                }
                else if (IncomingValue.Type.ToLower() == "string")
                {
                    return IncomingValue.Value;
                }
                return "";
            }
            catch (Exception)
            {

                return "";
            }    
            
           

        }
        private async Task FireAndForgetMethodAsync(string applicationName, string connectionString, int refreshTimerIntervalInMs)
        {

            while (true)
            {
                try
                {
                    

                    allYouPlayEntities = new AllYouPlayEntities(connectionString);
                    AppConfig = (from p in allYouPlayEntities.Configurations where p.ApplicationName == applicationName&& p.IsActive==true select p).ToList();
                    Debug.WriteLine("Thread is running.. "+ " ### Application Name:" + applicationName + " ### Count: " +AppConfig.Count);
                    await Task.Delay(refreshTimerIntervalInMs);
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("Exception ! ##:"+ ex.Message);
                }
               
            }
            

             // Pretend we are doing something for 5s

            
        }

        private void FireAndForgetMethod()
        {
            Debug.WriteLine("Started running a background work item...");

            Thread.Sleep(5000); // Pretend we are doing something for 5s

            Debug.WriteLine("Finished running that.");
        }

        private void FaultyFireAndForgetMethod()
        {
            throw new Exception("I'm failing just to make a point.");
        }
    }
}
