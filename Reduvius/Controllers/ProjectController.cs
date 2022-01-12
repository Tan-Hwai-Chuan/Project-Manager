using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reduvius.Data.Repositories;
using Reduvius.Data.Repository;
using Reduvius.Models;
using Reduvius.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Controllers
{
    public class ProjectController : Controller
    {
        private readonly UserManager<ApplicationUser> _usrManager;
        private readonly IProjectRepository _pRepo;
        private readonly IUserProjectRepository _upRepo;

        public ProjectController(
            UserManager<ApplicationUser> usrManager,
            IProjectRepository pRepo,
            IUserProjectRepository upRepo
            )
        {
            _usrManager = usrManager;
            _pRepo = pRepo;
            _upRepo = upRepo;
        }

        public IActionResult Index()
        {
            string userId = _usrManager.GetUserId(HttpContext.User);
            return View(_pRepo.GetAllProjectsByUserId(userId));

        }

        public IActionResult Project(int projectId)
        {
            return View(_pRepo.GetProject(projectId));
        }

        [HttpGet]
        public IActionResult Edit(int? projectId)
        {
            if(projectId == null)
            {
                return View(new ProjectViewModel());
            }
            else
            {
                var project = _pRepo.GetProject((int)projectId);
                return View(new ProjectViewModel
                {
                    ProjectId = project.ProjectId,
                    Title = project.Title,
                    CreatedDate = project.CreatedDate,
                    UpdatedDate = project.UpdatedDate,
                    State = project.State
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectViewModel vm)
        {
            string userId = _usrManager.GetUserId(HttpContext.User);

            var project = new Project
            {
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                CreatedDate = vm.CreatedDate,
                UpdatedDate = vm.UpdatedDate,
                State = (Models.States)vm.State
            };

            

            if (project.ProjectId > 0)
            {
                _pRepo.UpdateProject(project);
                if (await _pRepo.SaveChangesAsync())
                    return RedirectToAction("Project", new { projectId = project.ProjectId });
                else
                    return View(vm);
            }
            else
            {
                _pRepo.AddProject(project);
                if (await _pRepo.SaveChangesAsync() )
                {
                    var userProject = new UserProject
                    {
                        UserId = userId,
                        ProjectId = project.ProjectId
                    };
                    _upRepo.AddUserProject(userProject);
                    if (await _upRepo.SaveChangesAsync())
                        return Redirect("Index");
                    else
                        return View(vm);
                }
                else
                {
                    return View(vm);
                }
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int projectId)
        {
            string userId = _usrManager.GetUserId(HttpContext.User);

            _pRepo.DeleteProject(projectId);
            _upRepo.RemoveUserProject(userId, projectId);
            await _pRepo.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
