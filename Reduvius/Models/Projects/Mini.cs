using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Models
{
    public class Mini : BaseEntity
    {


        [Key]
        public int MiniId { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public IList<Bug> Bugs { get; set; }
    }
}
