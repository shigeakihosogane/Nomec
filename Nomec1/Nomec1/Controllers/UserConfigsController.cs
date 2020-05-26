using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nomec1.Data;
using Nomec1.Models;

namespace Nomec1.Controllers
{
    public class UserConfigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Claim _claim;


        public UserConfigsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        }

        
        //---------------------------------------------------------------------------------------------------------------------------------------カスタム開始       メニュー追加
        public async Task<IActionResult> Menu()
        {
            ViewBag.Message = "Your contact page. ";
            if (User.Identity.IsAuthenticated)
            {
                string id = _claim.Value;
                ViewBag.Message = "Your id = " + id;

                var UC = await _context.UserConfig.FirstOrDefaultAsync(m => m.UserId == id);

                if (UC == null)
                {
                    //UserIdがなかった場合の処理（初回）
                    _context.Add(new UserConfig
                    {
                        UserId = id,
                        UserCategoryId = 0
                    });

                    await _context.SaveChangesAsync();

                }

                return View(UC);

            }
            else
            {
                ViewBag.Message = "No login information";
                //return NotFound();
                //return RedirectToAction(nameof(Index));
            }

            return View();

        }
        //--------------------------------------------------------------------------------------------------------------------------------カスタムここまで










        // GET: UserConfigs
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserConfig.ToListAsync());
        }

        // GET: UserConfigs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConfig = await _context.UserConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userConfig == null)
            {
                return NotFound();
            }

            return View(userConfig);
        }

        // GET: UserConfigs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserCategoryId,NickName")] UserConfig userConfig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userConfig);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userConfig);
        }

        // GET: UserConfigs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConfig = await _context.UserConfig.FindAsync(id);
            if (userConfig == null)
            {
                return NotFound();
            }
            return View(userConfig);
        }

        // POST: UserConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserCategoryId,NickName")] UserConfig userConfig)
        {
            if (id != userConfig.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userConfig);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserConfigExists(userConfig.Id))
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
            return View(userConfig);
        }

        // GET: UserConfigs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConfig = await _context.UserConfig
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userConfig == null)
            {
                return NotFound();
            }

            return View(userConfig);
        }

        // POST: UserConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userConfig = await _context.UserConfig.FindAsync(id);
            _context.UserConfig.Remove(userConfig);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserConfigExists(int id)
        {
            return _context.UserConfig.Any(e => e.Id == id);
        }
    }
}
