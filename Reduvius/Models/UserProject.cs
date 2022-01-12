using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Models
{
    public class UserProject
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Key, Column(Order = 1)]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
