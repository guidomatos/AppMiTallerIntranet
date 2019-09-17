using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for DataBaseHelper
/// </summary>
/// 
namespace AppMiTaller.Intranet.DA
{
    public static class DataBaseHelper
    {
        public static string GetDbProvider()
        {
            return ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ProviderName;
        }
        public static string GetDbConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["AppMiTallerCN"].ConnectionString;
        }
    }

}

