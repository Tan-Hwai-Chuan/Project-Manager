using Reduvius.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Reduvius.ViewModels
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public States State { get; set; } = States.Incomplete;
    }

}
