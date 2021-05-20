using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public DateTime CommentDatetime { get; set; }
        public TaskAgenda Task { get; set; }
    }
}
