using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZombieParty.Models;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private BaseDonnees _baseDonnees { get; set; }

        public ZombieController(BaseDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            this.ViewBag.MaListe = _baseDonnees.Zombies.ToList();

            return View();
        }

        // GET
        public IActionResult Create()
        {
            ViewBag.ZombieTypes = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName", null);
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult Create(Zombie zombie)
        {
            if (ModelState.IsValid)
            {
                _baseDonnees.Zombies.Add(zombie);
                return this.RedirectToAction("Index");
            }

            // set le zombie type du nouveau zombie
            ZombieType zombieTypeSelectionee = _baseDonnees.ZombieTypes.Where(zt => zt.Id == zombie.ZombieTypeId).SingleOrDefault();
            zombie.ZombieType = zombieTypeSelectionee;

            ViewBag.ZombieTypes = new SelectList(_baseDonnees.ZombieTypes.ToList(), "Id", "TypeName", zombieTypeSelectionee);

            return View(zombie);
        }
    }
}