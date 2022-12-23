using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LotteryApplication.DBContext;
using LotteryApplication.Models;
using MessagePack.Formatters;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LotteryApplication.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationUser? _applicationUser;


        public ParticipantController(ApplicationDbContext context)
        {
            _context = context;
            _userManager = context.GetService<UserManager<ApplicationUser>>();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(User.Identity!=null)
            _applicationUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
        }
        // GET: Participant
        public IActionResult Index()
        {
            Participation? participation = null;
            if (_applicationUser != null)
                participation = _applicationUser.Participation;
              return View(participation);
        }

        // GET: Participant/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.participations == null)
            {
                return NotFound();
            }

            var participation = await _context.participations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create()
        {
            

            if (_applicationUser!=null && _applicationUser.Participation==null)
            {
                Participation participation = new Participation();
                participation.Id = Guid.NewGuid();
                participation.DateOfParticipation = DateTime.Now;
                participation.HaveWon = false;
                participation.Participant = _applicationUser;
                _applicationUser.Participation = participation;
                await _userManager.UpdateAsync(_applicationUser);
                _context.Add(participation);
                await _context.SaveChangesAsync();
                
                
            }

            return RedirectToAction(nameof(Index));



        }

        // GET: Participant/Edit/5
      


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,HaveWon,DateOfParticipation")] Participation participation)
        {
            if (id != participation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationExists(participation.Id))
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
            return View(participation);
        }

        // GET: Participant/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.participations == null)
            {
                return NotFound();
            }

            var participation = await _context.participations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // POST: Participant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.participations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.participations'  is null.");
            }
            var participation = await _context.participations.FindAsync(id);
            if (participation != null)
            {
                _context.participations.Remove(participation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipationExists(Guid id)
        {
          return _context.participations.Any(e => e.Id == id);
        }
    }
}
