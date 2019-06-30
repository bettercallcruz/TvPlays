using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TvPlays.Models;

namespace TvPlays.Controllers
{
    public class ClipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clips
        public ActionResult Index()
        {
            var clips = db.Clips.Include(c => c.User);
            return View(clips.ToList());
        }

        // GET: Clips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clips clips = db.Clips.Find(id);
            if (clips == null)
            {
                return HttpNotFound();
            }
            return View(clips);
        }

        //GET: video de cada Clip
        public FileResult VideoClip(string pathClip)
        {
            var path = Server.MapPath("~/Assets/images");
            return File(path + "\\" + pathClip, "video/mp4");
        }

        // GET: Clips/Create
        public ActionResult Create()
        {
            ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name");
            return View();
        }

        // POST: Clips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TimeClip,TitleClip,DateClip,PathClip,UserFK")] Clips clips)
        {
            if (ModelState.IsValid)
            {
                db.Clips.Add(clips);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name", clips.UserFK);
            return View(clips);
        }

        // GET: Clips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clips clips = db.Clips.Find(id);
            if (clips == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name", clips.UserFK);
            return View(clips);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TimeClip,TitleClip,DateClip,PathClip,UserFK")] Clips clips)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clips).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name", clips.UserFK);
            return View(clips);
        }

        // GET: Clips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clips clips = db.Clips.Find(id);
            if (clips == null)
            {
                return HttpNotFound();
            }
            return View(clips);
        }

        // POST: Clips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clips clips = db.Clips.Find(id);
            db.Clips.Remove(clips);
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
