using SoftSignAPI.Model;
using System.Reflection.PortableExecutable;

namespace SoftSignAPI.Dto
{
    public class FieldDto
    {
        public string Page { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
		public double Width { get; set; }
		public double Height { get; set; }
		public FieldType? Type { get; set; }
        public string? Detail { get; set; }
        public string? Text { get; set; }
    }
}
