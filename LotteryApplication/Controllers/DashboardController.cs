using LotteryApplication.DBContext;
using LotteryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LotteryApplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: DashboardController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DashboardController/Details/Participants/5
        public ActionResult DetailsParticipants(int id)
        {
            return View();
        }

        // GET: DashboardController/Create
        public ActionResult CreateParticipant()
        {
            return View();
        }

        // POST: DashboardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateParticipant(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Edit/5
        public ActionResult EditPariticpant(int id)
        {
            return View();
        }

        // POST: DashboardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditParticipant(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashboardController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashboardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Participations
        public async Task<IActionResult> IndexParticipations()
        {
            return View(await _context.participations.ToListAsync());
        }

        // GET: Participations/Details/5
        public async Task<IActionResult> DetailsParticipation(Guid? id)
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

        // GET: Participations/Create
        public IActionResult CreateParticipition()
        {
            return View();
        }

        // POST: Participations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParticipition([Bind("Id,HaveWon,DateOfParticipation")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                participation.Id = Guid.NewGuid();
                _context.Add(participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participation);
        }

        // GET: Participations/Edit/5
        public async Task<IActionResult> EditParticipation(Guid? id)
        {
            if (id == null || _context.participations == null)
            {
                return NotFound();
            }

            var participation = await _context.participations.FindAsync(id);
            if (participation == null)
            {
                return NotFound();
            }
            return View(participation);
        }

        // POST: Participations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParticipation(Guid id, [Bind("Id,HaveWon,DateOfParticipation")] Participation participation)
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

        // GET: Participations/Delete/5
        public async Task<IActionResult> DeleteParticipation(Guid? id)
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

        // POST: Participations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedParticipation(Guid id)
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
