using  System.Configuration;


namespace TestLibrary.Global
{
    public static class GlobalValues
    {
        public static string baseURL = ConfigurationManager.AppSettings["BaseURL"];

    }
}
