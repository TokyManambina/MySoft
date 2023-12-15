using Newtonsoft.Json;

namespace SoftSignAPI.Helpers
{
    public class Tools
    {
        public static string MySecret = "Soft123well!SoftSign";

        public static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string RandomPassword(string mail)
        {
            return mail.GetHashCode().ToString("X");
        }
    }
}
