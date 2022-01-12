using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public interface IBugRepository
    {
        Bug GetBug(int bugId);
        List<Bug> GetAllBug();
        List<Bug> GetBugsByMiniId(int miniId);
        void AddBug(Bug bug);
        void UpdateBug(Bug bug);
        void DeleteBug(int bugId);
        Task<bool> SaveChangesAsync();
    }
}
