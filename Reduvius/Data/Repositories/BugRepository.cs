using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public class BugRepository : IBugRepository
    {
        private ApplicationDbContext _ctx;

        public BugRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddBug(Bug bug)
        {
            _ctx.Bugs.Add(bug);
        }

        public Bug GetBug(int bugId)
        {
            return _ctx.Bugs.FirstOrDefault(b => b.BugId == bugId);
        }

        public List<Bug> GetAllBug()
        {
            return _ctx.Bugs.ToList();
        }

        public List<Bug> GetBugsByMiniId(int miniId)
        {
            return _ctx.Bugs
                       .Where(b => b.MiniId == miniId)
                       .ToList();
        }

        public void UpdateBug(Bug bug)
        {
            _ctx.Bugs.Update(bug);
        }

        public void DeleteBug(int bugId)
        {
            _ctx.Bugs.Remove(GetBug(bugId));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
