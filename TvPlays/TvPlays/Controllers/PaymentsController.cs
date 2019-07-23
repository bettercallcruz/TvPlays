using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TvPlays.Models;

namespace TvPlays.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.User);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payments payments = db.Payments.Find(id);
            if (payments == null)
            {
                return HttpNotFound();
            }
            return View(payments);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Value")] Payments payments)
        {
            if (ModelState.IsValid)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));

                if (user == null)
                {
                    return HttpNotFound();
                }

                Payments payment = new Payments
                {
                    Value = payments.Value,
                    PaymentDay = DateTime.Now,
                    UtilizadoresFK = user.ID
                };

                var user2 = db.Users.SingleOrDefault(u => u.UserName.Equals(User.Identity.Name));

                db.Payments.Add(payment);
                db.SaveChanges();

                userManager.RemoveFromRole(user2.Id , "Normal");
                userManager.AddToRole(user2.Id, "Premium");

                db.Entry(user).State = EntityState.Modified;

                //return RedirectToAction("LogOff", "Account"); 
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", payments.UtilizadoresFK);
            return View(payments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
