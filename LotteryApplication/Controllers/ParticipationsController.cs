using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LotteryApplication.DBContext;
using LotteryApplication.Models;

namespace LotteryApplication.Controllers
{
    public class ParticipationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParticipationsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
    }
}
