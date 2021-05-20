using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class TaskAgenda
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Title { get; set; }

        [Required, MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime DateTimeAdded { get; set; }

        [Required]
        public DateTime DateTimeDeadline { get; set; }

        [Required]
        //low, medium, high
        public string Importance { get; set; }

        [Required]
        //open, in progress, closed
        public string State { get; set; }
        public DateTime DateTimeClosedAt { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
