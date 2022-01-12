using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repositories
{
    public interface IUserProjectRepository
    {
        UserProject GetUserProject(string userId, int projectId);
        List<UserProject> GetProjectsOwnedByUserId(string userId);
        List<UserProject> GetUsersByProjectId(int projectId);
        void AddUserProject(UserProject userProject);
        void UpdateUserProject(UserProject userProject);
        void RemoveUserProject(string userId, int projectId);
        Task<bool> SaveChangesAsync();
    }
}
