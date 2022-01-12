using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Models
{
    public class BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public States State { get; set; } = States.Incomplete;
    }

    public enum States
    {
        Completed,
        Incomplete
    }
}
