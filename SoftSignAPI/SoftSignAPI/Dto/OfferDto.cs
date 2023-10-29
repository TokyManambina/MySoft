namespace SoftSignAPI.Dto
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; } = "0";
        public bool IsActive { get; set; }
    }
}
