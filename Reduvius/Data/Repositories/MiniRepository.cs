using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public class MiniRepository : IMiniRepository
    {
        private ApplicationDbContext _ctx;

        public MiniRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void AddMini(Mini mini)
        {
            _ctx.Minis.Add(mini);
        }

        public Mini GetMini(int miniId)
        {
            return _ctx.Minis.FirstOrDefault(m => m.MiniId == miniId);
        }

        public List<Mini> GetAllMini()
        {
            return _ctx.Minis.ToList();
        }

        public List<Mini> GetMinisByProjectId(int projectId)
        {
            return _ctx.Minis
                       .Where(m => m.ProjectId == projectId)
                       .ToList();
        }

        public void UpdateMini(Mini mini)
        {
            _ctx.Minis.Update(mini);
        }

        public void DeleteMini(int miniId)
        {
            _ctx.Minis.Remove(GetMini(miniId));
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
