using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace SoftSignAPI.Model
{
    [Index(nameof(Code), IsUnique = true)]
    public class Offer
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Hour { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Capacity { get; set; }
        public CapacityUnit CapacityUnit { get; set; }
        public string? Description { get; set; }
        public string Price { get; set; } = "0";
        public bool IsActive { get; set; }

        public virtual List<Subscription> Subscriptions { get; set;}
    }

    public enum CapacityUnit
    {
        Mo, Go, To
    }
}
