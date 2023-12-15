using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;

namespace SoftSignWeb.Models
{
    public class Token
    {
        public string token {get; set;}
        public string refresh {get;set;}
        public DateTime Created {get;set;}
        public DateTime Expires {get;set;}
    }
}
