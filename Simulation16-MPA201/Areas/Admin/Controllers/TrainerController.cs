using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation16_MPA201.Contexts;
using Simulation16_MPA201.Helpers;
using Simulation16_MPA201.Models;
using Simulation16_MPA201.ViewModels.TrainerViewModels;

namespace Simulation16_MPA201.Areas.Admin.Controllers;
[Area("Admin")]

public class TrainerController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
{
    public IActionResult Index()
    {
        var trainers = _context.Trainers.Include(x => x.Branch).Select(trainer => new TrainerGetVM()
        {
            Id = trainer.Id,
            Name = trainer.Name,
            Description = trainer.Description,
            BranchName = trainer.Branch.Name,
            ImagePath = trainer.ImagePath
        }).ToList();

        return View(trainers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        SendBranchesWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrainerCreateVM vm)
    {
        SendBranchesWithViewBag();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var existBranch = _context.Branches.Any(x => x.Id == vm.BranchId);

        if (!existBranch)
        {
            ModelState.AddModelError("BranchId", "Branch not found");
            return View(vm);
        }

        if (!vm.ImagePath.CheckFileSize(2))
        {
            ModelState.AddModelError("ImagePath", "File size must be less than 2MB");
            return View(vm);
        }

        if (!vm.ImagePath.CheckFileType("image"))
        {
            ModelState.AddModelError("ImagePath", "File type must be IMAGE !");
            return View(vm);
        }

        var folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");

        string imageName = vm.ImagePath.SaveFile(folderPath);

        Trainer trainer = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            BranchId = vm.BranchId,
            ImagePath = imageName
        };

        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        SendBranchesWithViewBag();
        var existTrainer = _context.Trainers.Find(id);

        if (existTrainer is null)
        {
            return NotFound();
        }

        TrainerUpdateVM vm = new()
        {
            Id = existTrainer.Id,
            Name = existTrainer.Name,
            Description = existTrainer.Description,
            BranchId = existTrainer.BranchId,
        };

        return View(vm);
    }

    [HttpPost]
    public IActionResult Update(TrainerUpdateVM vm)
    {
        SendBranchesWithViewBag();

        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var existTrainer = _context.Trainers.Find(vm.Id);

        if (existTrainer is null)
        {
            return NotFound();
        }

        var existBranch = _context.Branches.Any(x => x.Id == vm.BranchId);

        if (!existBranch)
        {
            ModelState.AddModelError("BranchId", "Branch not found");
            return View(vm);
        }

        if (!vm.ImagePath?.CheckFileSize(2) ?? false)
        {
            ModelState.AddModelError("ImagePath", "File size must be less than 2MB");
            return View(vm);
        }

        if (!vm.ImagePath?.CheckFileType("image") ?? false)
        {
            ModelState.AddModelError("ImagePath", "File type must be IMAGE !");
            return View(vm);
        }


        existTrainer.Name = vm.Name;
        existTrainer.Description = vm.Description;
        existTrainer.BranchId = vm.BranchId;


        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");

        if (vm.ImagePath is { })
        {
            string newImageName = vm.ImagePath.SaveFile(folderPath);

            if (System.IO.File.Exists(Path.Combine(folderPath, existTrainer.ImagePath)))
            {
                System.IO.File.Delete(Path.Combine(folderPath, existTrainer.ImagePath));
            }

            existTrainer.ImagePath = newImageName;
        }

        _context.Trainers.Update(existTrainer);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var existTrainer = _context.Trainers.Find(id);

        if (existTrainer is null)
        {
            return NotFound();
        }

        _context.Trainers.Remove(existTrainer);
        _context.SaveChanges();

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        if (System.IO.File.Exists(Path.Combine(folderPath, existTrainer.ImagePath)))
        {
            System.IO.File.Delete(Path.Combine(folderPath, existTrainer.ImagePath));
        }

        return RedirectToAction("Index");
    }

    private void SendBranchesWithViewBag()
    {
        var branches = _context.Branches.ToList();
        ViewBag.Branches = branches;
    }
}
