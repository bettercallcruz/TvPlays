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

                db.Payments.Add(payment);
                db.SaveChanges();
                //Voltar para a View Index do 'Manage'  
                //return RedirectToAction("Index");
            }
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", payments.UtilizadoresFK);
            return View(payments);
        }

        //Nao faz sentido alguem conseguir editar um pagamento, ate mesmo o 'Admin'

        //// GET: Payments/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Payments payments = db.Payments.Find(id);
        //    if (payments == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", payments.UtilizadoresFK);
        //    return View(payments);
        //}

        //// POST: Payments/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Value,PaymentDay,UtilizadoresFK")] Payments payments)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(payments).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", payments.UtilizadoresFK);
        //    return View(payments);
        //}

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payments payments = db.Payments.Find(id);
            db.Payments.Remove(payments);
            db.SaveChanges();
            return RedirectToAction("Index");
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
