using LotteryApplication.DBContext;
using LotteryApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LotteryApplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
            _userManager = context.GetService<UserManager<ApplicationUser>>();
        }
        // GET: Dashboard
        public IActionResult Index()
        {
            return View();
        }

        // GET: Dashboard/Details/Participants
        public async Task<IActionResult> Participants()
        {
            var participants = await (from user in _context.applicationUsers
                                join ur in _context.UserRoles on user.Id equals ur.UserId
                                join role in _context.Roles on ur.RoleId equals role.Id
                                where role.Name == "Participant"
                                select user).ToListAsync();
            return View(participants);
        }
        // GET: Dashboard/Details/Participants/DetailsParticipation/5
        public async Task<IActionResult> DetailsParticipant(String? id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.applicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Dashboard/CreateParticipant/
        public IActionResult CreateParticipant()
        {
            return View();
        }

        // POST: Dashboard/CreateParticipant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParticipant([Bind("Id,FirstName,LastName,Email,IsAdmin")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                applicationUser.UserName = applicationUser.Email;
                var result = await _userManager.CreateAsync(applicationUser,"Password1."+applicationUser.FirstName+applicationUser.LastName);
                if (result.Succeeded)
                {
                    if(applicationUser.IsAdmin)
                        await _userManager.AddToRoleAsync(applicationUser, "Admin");
                    else
                        await _userManager.AddToRoleAsync(applicationUser, "Participant");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUser);
        }

        // GET: Dashboard/EditParticipant/5
        public async Task<IActionResult> EditParticipant(string id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }
            var participant = await _userManager.FindByIdAsync(id);
            if (participant == null)
                return NotFound();
            return View(participant);
        }

        // POST: Dashboard/EditParticipant/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditParticipant(string id, [Bind("Id,FirstName,LastName,Email,IsAdmin")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(id);
                if (currentUser == null)
                {
                    return NotFound();
                }

                currentUser.FirstName = applicationUser.FirstName;
                currentUser.LastName = applicationUser.LastName;
                currentUser.Email = applicationUser.Email;
                currentUser.IsAdmin = applicationUser.IsAdmin;

                var result = await _userManager.UpdateAsync(currentUser);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(currentUser);
                    foreach (var role in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(currentUser, role);
                    }
                    if (applicationUser.IsAdmin)
                        await _userManager.AddToRoleAsync(currentUser, "Admin");
                    else
                        await _userManager.AddToRoleAsync(currentUser, "Participant");

                    RedirectToAction(nameof(Index));
                }


            }
            return View(applicationUser);
        }

        // GET: Dashboard/DeleteParticipant/5
        public async Task<IActionResult> DeleteParticipant(String id)
        {
            if (id == null || _context.applicationUsers == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.applicationUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Dashboard/DeleteParticipant/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteParticipant(String id, IFormCollection collection)
        {
            if (_context.applicationUsers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.applicationUsers'  is null.");
            }
            var applicationUser = await _context.applicationUsers.FindAsync(id);
            if (applicationUser != null)
            {
                _context.applicationUsers.Remove(applicationUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
        // GET: Dashboard/Participations
        public async Task<IActionResult> Participations()
        {
            return View(await _context.participations.ToListAsync());
        }

        // GET: Dashboard/Participations/Details/5
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

        // GET: Dashboard/Participations/Create
        public IActionResult CreateParticipation()
        {
            return View();
        }

        // POST: Dashboard/Participations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateParticipation([Bind("Id,HaveWon,DateOfParticipation")] Participation participation)
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

        // GET: Dashboard/Participations/Edit/5
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

        // POST: Dashboard/Participations/Edit/5
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

        // GET: Dashboard/Participations/Delete/5
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

        // POST: Dashboard/Participations/Delete/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LotteryResult()
        {
            var participants = await (from user in _context.applicationUsers
                                      join ur in _context.UserRoles on user.Id equals ur.UserId
                                      join role in _context.Roles on ur.RoleId equals role.Id
                                      join participation in _context.participations on user equals participation.Participant 
                                      where role.Name == "Participant"
                                      select user).ToListAsync();

            var winners = participants.Take(3).ToList();
            foreach(var winner in winners)
            {
                if (winner.Participation != null)
                    winner.Participation.HaveWon = true;
            }
                
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
