namespace SimpleDotNetWebApiApp.Application.Helpers
{
    public static class Utils
    {
        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }
    }
}
