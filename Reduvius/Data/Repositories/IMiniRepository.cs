using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public interface IMiniRepository
    {
        Mini GetMini(int miniId);
        List<Mini> GetAllMini();
        List<Mini> GetMinisByProjectId(int projectId);
        void AddMini(Mini mini);
        void UpdateMini(Mini mini);
        void DeleteMini(int miniId);
        Task<bool> SaveChangesAsync();
    }
}
