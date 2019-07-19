using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        private readonly ApplicationDbContext db = new ApplicationDbContext();
<<<<<<< Updated upstream
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
=======
>>>>>>> Stashed changes

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
            var clips = db.Clips.Include(c => c.ListComments).FirstOrDefault(c => c.ID == id);

            if (clips == null)
            {
                return HttpNotFound();
            }
            return View(clips);
        }

        //GET: video de cada Clip
        public ActionResult VideoClip(string pathClip)
        {
            var path = Server.MapPath("~/Assets/images");
            var file = File(path + "\\" + pathClip, "video/mp4");
            return file;
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,TimeClip,TitleClip,DateClip,PathClip,UserFK")] Clips clips)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Clips.Add(clips);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name", clips.UserFK);
        //    return View(clips);
        //}

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase fileupload, ClipsDTO clips)
        {
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(User.Identity.Name));
            var user2 = db.Utilizadores.FirstOrDefault(u => u.Email.Equals(user.Email));

            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
<<<<<<< Updated upstream
                int fileSize = fileupload.ContentLength;
                int Size = fileSize / 1000;
                fileupload.SaveAs(Server.MapPath("~/VideoFileUpload/" + fileName));



                var clip = new Clips
                {
                    
=======
                string path = Server.MapPath("~/Assets/images/" + fileName);
                fileupload.SaveAs(path);

                var clip = new Clips { 
                    TitleClip = clips.TitleClip,
                    PathClip = path,
                    DateClip = DateTime.Now,
                    UserFK = user2.ID
>>>>>>> Stashed changes
                };

                db.Clips.Add(clip);
                db.SaveChanges();
                return View();
            }
            return View();
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
