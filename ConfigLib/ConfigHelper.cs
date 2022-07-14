using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLib
{
    public  class ConfigHelper
    {
        public static string SetConnectionString(string ServerName,string DatabaseName)
        {
            EntityConnectionStringBuilder conn = new EntityConnectionStringBuilder();
            conn.Metadata = @"res://*/ConfigModel.csdl|res://*/ConfigModel.ssdl|res://*/ConfigModel.msl";
            conn.Provider = "System.Data.SqlClient";
            conn.ProviderConnectionString = @"data source="+ ServerName + ";initial catalog="+ DatabaseName + ";integrated security=True;multipleactiveresultsets=True;application name=EntityFramework;";
            return conn.ConnectionString;
        }
    }
}
