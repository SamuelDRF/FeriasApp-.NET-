using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FeriasApp.Data;
using FeriasApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace FeriasApp.Controllers
{
    public class InfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Infoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Info.ToListAsync());
        }

        // GET: Infoes / ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Infoes / ShowSearchResults
        public async Task<IActionResult> ShowSearchResults( string SearchDate)
        {
            return View("Index", await _context.Info.Where(i => i.Name.Contains(SearchDate)).ToListAsync());
        }

        // POST: Infoes / ShowSearchUserResults
        public async Task<bool> ShowSearchUserResults(string SearchUser)
        {
            bool foundResult = await _context.Info.AnyAsync(i => i.Name.Contains(SearchUser));
            return foundResult;
        }


        //filter
        [HttpGet]
        public async Task<ActionResult> Index(string Empsearch)
        {
            ViewData["Getemployeedetails"] = Empsearch;
            var empquery = from x in _context.Info select x;
            if (!String.IsNullOrEmpty(Empsearch))
            {
                if (Empsearch != "*")
                {
                    empquery = empquery.Where(x => x.Confirmed.Contains(Empsearch));
                }
            }
            return View(await empquery.AsNoTracking().ToListAsync());
        }

        // GET: Infoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }


        // GET: Infoes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Infoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind("Id,Name,Requested,Confirmed,Days,Zone")] Info info)
        {
            //var User = ShowSearchUserResults(info.Name);
            //if (await User)
            //{
                //true (found data created by the user, therefore cannot create data)
                Console.WriteLine("true");
            //}
            //else
            //{
                //false (did not find any data created by the user, therefore you can create data)
                Console.WriteLine("false");
                if (ModelState.IsValid)
                {
                    _context.Add(info);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            //}
            return View(info);
        }

        // GET: Infoes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info.FindAsync(id);
            if (info == null)
            {
                return NotFound();
            }
            return View(info);
        }

        //Encontra uma pessoa pelo name e devolve a sua info
        public async Task<bool> ShowSearchUserResultsName(string SearchUser)
        {
            bool foundResult = await _context.Info.AnyAsync(i => i.Name.Contains(SearchUser));
            return foundResult;
        }

        //Encontra uma pessoa pelo name e devolve a sua info
        public async Task<object> ShowSearchUserResultsToEdit(string SearchUser)
        {
            object foundResult = await _context.Info.Where(i => i.Name == SearchUser).ToListAsync();
            return foundResult;
        }

        // POST: Infoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Requested,Confirmed,Days,Zone")] Info info)
        {
            if (id != info.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }
        public async Task<IActionResult> EditIndex(int id, [Bind("Id,Name,Requested,Confirmed,Days,Zone")] Info info)
        {
            if (id != info.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(info);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoExists(info.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(info);
        }

        // GET: Infoes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Info == null)
            {
                return NotFound();
            }

            var info = await _context.Info
                .FirstOrDefaultAsync(m => m.Id == id);
            if (info == null)
            {
                return NotFound();
            }

            return View(info);
        }

        // POST: Infoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Info == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Info'  is null.");
            }
            var info = await _context.Info.FindAsync(id);
            if (info != null)
            {
                _context.Info.Remove(info);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoExists(int id)
        {
          return _context.Info.Any(e => e.Id == id);
        }
    }
}
