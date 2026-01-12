using Microsoft.AspNetCore.Mvc;
using Simulation16_MPA201.Contexts;
using Simulation16_MPA201.Models;
using System.Threading.Tasks;

namespace Simulation16_MPA201.Areas.Admin.Controllers;
[Area("Admin")]

public class BranchController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var branches = _context.Branches.ToList();
        return View(branches);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Branch branch)
    {
        if (!ModelState.IsValid)
        {
            return View(branch);
        }

        Branch newBranch = new()
        {
            Name = branch.Name
        };

        _context.Branches.Add(newBranch);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var branch = _context.Branches.Find(id);

        if (branch is null)
        {
            return NotFound();
        }

        return View(branch);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Branch branch)
    {
        if (!ModelState.IsValid)
        {
            return View(branch);
        }

        var existBranch = _context.Branches.Find(branch.Id);

        if (existBranch is null)
        {
            return NotFound();
        }

        existBranch.Name = branch.Name;

        _context.Branches.Update(existBranch);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var branch = _context.Branches.Find(id);

        if (branch is null)
        {
            return NotFound();
        }

        _context.Branches.Remove(branch);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
