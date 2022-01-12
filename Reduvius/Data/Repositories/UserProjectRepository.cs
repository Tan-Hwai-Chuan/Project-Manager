using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private ApplicationDbContext _ctx;

        public UserProjectRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddUserProject(UserProject userProject)
        {
            _ctx.UserProjects.Add(userProject);
        }

        public UserProject GetUserProject(string userId, int projectId)
        {
            return _ctx.UserProjects
                       .Where(up => up.UserId == userId && up.ProjectId == projectId)
                       .FirstOrDefault();
        }

        public List<UserProject> GetProjectsOwnedByUserId(string userId)
        {
            return _ctx.UserProjects
                       .Where(up => up.UserId == userId)
                       .ToList();
        }

        public List<UserProject> GetUsersByProjectId(int projectId)
        {
            return _ctx.UserProjects
                       .Where(up => up.ProjectId == projectId)
                       .ToList();
        }

        public void UpdateUserProject(UserProject userProject)
        {
            _ctx.UserProjects.Update(userProject);
        }

        public void RemoveUserProject(string userId, int projectId)
        {
            _ctx.UserProjects
                .RemoveRange(
                    _ctx.UserProjects
                        .Where(up => up.UserId == userId && up.ProjectId == projectId)
                );

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
