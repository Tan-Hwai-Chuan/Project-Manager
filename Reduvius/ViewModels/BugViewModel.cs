using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Reduvius.Models;

namespace Reduvius.ViewModels
{
    public class BugViewModel
    {
        public int MiniId { get; set; }
        public int BugId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public States State { get; set; } = States.Incomplete;
    }
}
