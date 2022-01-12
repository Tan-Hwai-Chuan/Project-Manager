using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reduvius.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserId")]
        public IList<UserProject> UserProjects { get; set; }

    }
}
