using Microsoft.EntityFrameworkCore;
using Reduvius.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private ApplicationDbContext _ctx;
        
        public ProjectRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public void AddProject(Project project)
        {
            _ctx.Projects.Add(project);
        }

        public Project GetProject(int id)
        {
            return _ctx.Projects.Include(p => p.Minis)
                                .ThenInclude(m => m.Bugs)
                                .FirstOrDefault(p => p.ProjectId == id);
        }

        public List<Project> GetAllProjects()
        {
            return _ctx.Projects.ToList();
        }

        public List<Project> GetAllProjectsByUserId(string userId)
        {
            return _ctx.UserProjects
                       .Where(up => up.UserId == userId)
                       .Include(up => up.Project.Minis)
                       .ThenInclude(p => p.Bugs)
                       .Select(up => up.Project)
                       .ToList();
        }

        public void UpdateProject(Project project)
        {
            _ctx.Projects.Update(project);
        }

        public void DeleteProject(int id)
        {
            _ctx.Projects.Remove(GetProject(id));
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
