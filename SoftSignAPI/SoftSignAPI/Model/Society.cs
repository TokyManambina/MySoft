﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftSignAPI.Model
{
    public class Society
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
    }
}
