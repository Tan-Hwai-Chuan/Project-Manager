using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repository
{
    public interface IProjectRepository
    {
        Project GetProject(int id);
        List<Project> GetAllProjects();
        List<Project> GetAllProjectsByUserId(string userId);
        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(int id);
        Task<bool> SaveChangesAsync();
    }
}
