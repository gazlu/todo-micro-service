using System.Configuration;

namespace Tasks
{
    public static class Globals
    {
        public static string TaskConnectionString = ConfigurationManager.ConnectionStrings["tasks"].ConnectionString;
        public static string StoreConnectionString = ConfigurationManager.ConnectionStrings["store"].ConnectionString;
        public static string StoreTableName = "StoreMessages";

        public static string SenderEmail = ConfigurationManager.AppSettings["sendGridUserName"];
        public static string SenderPassword = ConfigurationManager.AppSettings["sendGridPassword"];
        public static string SenderSMTPHost = "I.dont.know";
        public static int SenderSMTPPort = 587;
    }
}