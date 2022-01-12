using Microsoft.AspNetCore.Mvc;
using Reduvius.Data.Repositories;
using Reduvius.Models;
using Reduvius.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reduvius.Controllers
{
    public class BugController : Controller
    {
        private readonly IBugRepository _bRepo;
        private readonly IMiniRepository _mRepo;

        public BugController(
            IBugRepository bRepo,
            IMiniRepository mRepo)
        {
            _bRepo = bRepo;
            _mRepo = mRepo;
        }

        public IActionResult Index(int miniId)
        {
            return View(_bRepo.GetBugsByMiniId(miniId));
        }

        public IActionResult Bug(int bugId)
        {
            return View(_bRepo.GetBug(bugId));
        }

        [HttpGet]
        public IActionResult Edit(int? bugId)
        {
            if (bugId == null)
            {
                return PartialView(new BugViewModel());
            }
            else
            {
                var bug = _bRepo.GetBug((int)bugId);
                return PartialView(new BugViewModel
                {
                    MiniId = bug.MiniId,
                    BugId = bug.BugId,
                    Title = bug.Title,
                    Description = bug.Description,
                    CreatedDate = bug.CreatedDate,
                    UpdatedDate = bug.UpdatedDate,
                    State = bug.State
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BugViewModel vm)
        {
            var bug = new Bug
            {
                MiniId = vm.MiniId,
                BugId = vm.BugId,
                Title = vm.Title,
                Description = vm.Description,
                CreatedDate = vm.CreatedDate,
                UpdatedDate = vm.UpdatedDate,
                State = (Models.States)vm.State
            };

            var mini = _mRepo.GetMini(vm.MiniId);

            if(bug.BugId > 0)
            {
                _bRepo.UpdateBug(bug);
            } 
            else
            {
                _bRepo.AddBug(bug);
            }

            if (await _bRepo.SaveChangesAsync())
            {
                return RedirectToAction("Project","Project", new { projectId = mini.ProjectId });
            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int bugId)
        {
            _bRepo.DeleteBug(bugId);
            await _bRepo.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
