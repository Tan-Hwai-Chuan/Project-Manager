using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Models
{
    public class Project : BaseEntity
    {

        [Key]
        public int ProjectId { get; set; }
        public IList<UserProject> UserProjects { get; set; }
        public IList<Mini> Minis { get; set; }

    }
}
