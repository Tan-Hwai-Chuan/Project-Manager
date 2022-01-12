using Microsoft.AspNetCore.Mvc;
using Reduvius.Data.Repositories;
using Reduvius.Data.Repository;
using Reduvius.Models;
using Reduvius.ViewModels;
using System.Threading.Tasks;

namespace Reduvius.Controllers
{
    public class MiniController : Controller
    {
        private readonly IMiniRepository _mRepo;

        public MiniController(
            IMiniRepository mRepo)
        {
            _mRepo = mRepo;

        }

        public IActionResult Index(int projectId)
        {
            return View(_mRepo.GetMinisByProjectId(projectId));
        }

        public IActionResult Mini(int miniId)
        {
            return PartialView(_mRepo.GetMini(miniId));
        }

        [HttpGet]
        public IActionResult Edit(int? miniId)
        {
            if (miniId == null)
            {
                return View(new MiniViewModel());
            }
            else
            {
                var mini = _mRepo.GetMini((int)miniId);
                return View(new MiniViewModel
                {
                    ProjectId = mini.ProjectId,
                    MiniId = mini.MiniId,
                    Title = mini.Title,
                    Description = mini.Description,
                    CreatedDate = mini.CreatedDate,
                    UpdatedDate = mini.UpdatedDate,
                    State = mini.State
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MiniViewModel vm)
        {

            var mini = new Mini
            {
                ProjectId = vm.ProjectId,
                MiniId = vm.MiniId,
                Title = vm.Title,
                Description = vm.Description,
                CreatedDate = vm.CreatedDate,
                UpdatedDate = vm.UpdatedDate,
                State = (Models.States)vm.State
            };

            if(mini.MiniId > 0)
            {
                _mRepo.UpdateMini(mini);
            } 
            else
            {
                _mRepo.AddMini(mini);
            }

            if (await _mRepo.SaveChangesAsync())
            {
                return RedirectToAction("Project", "Project", new { projectId = vm.ProjectId });
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int miniId)
        {
            var mini = _mRepo.GetMini(miniId);

            _mRepo.DeleteMini(miniId);
            await _mRepo.SaveChangesAsync();

            return RedirectToAction("Project", "Project", new { projectId = mini.ProjectId });
        }
    }
}
