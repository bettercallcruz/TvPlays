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
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        //GET: video de cada Clip
        public ActionResult Image(string imagePath)
        {
            var dir = Server.MapPath("/App_Data/CategoriesImg");
            var path = Path.Combine(dir, imagePath); //validate the path for security or use other means to generate the path.
            return base.File(path, "image/jpeg");
        }

        public ActionResult ImageCategory(string imagePath)
        {
            var file = File(imagePath, "image/jpeg");
            return file;
        }


        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Name")] Categories categories, HttpPostedFileBase fileupload)
        {
            string fileName = Path.GetFileName(fileupload.FileName);
            string cont = fileupload.ContentType;
            string path = Server.MapPath("~/App_Data/Videos/" + fileName);
            fileupload.SaveAs(path);


            if (ModelState.IsValid)
            {
                var categoria = new Categories
                {
                    Name = categories.Name,
                    PathToCategory = path
                };


                db.Categories.Add(categoria);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(categories);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles="Admin")]
        public ActionResult Edit([Bind(Include = "ID,Name,PathToCategory")] Categories categories, HttpPostedFileBase fileupload)
        {

            Categories cat = db.Categories.Find(categories.ID);
            
            string path;

            if (fileupload != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                path = Server.MapPath("~/App_Data/Videos/" + fileName);
                fileupload.SaveAs(path);
            }
            else
            {
                path = cat.PathToCategory;
            }


            if (cat != null)
            {
                if (ModelState.IsValid)
                {

                    cat.ID = categories.ID;
                    cat.Name = categories.Name;
                    cat.PathToCategory = path;

                }
                db.SaveChanges();
                return View(categories);
            }
            return View(categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = db.Categories.Find(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = db.Categories.Find(id);

            //Se for Admin pode apagar qualquer Clip antes da verificacao do User == null && comment == null 
            //porque o admin nao e um Utilizador normal da aplicação
            if (User.IsInRole("Admin"))
            {
                db.Categories.Remove(categories);
                db.SaveChanges();
            }

            //Verifica se tanto o user como o clip existem e retorna 404 se nao encontrar algum dos dois
            if (categories == null)
            {
                return HttpNotFound();
            }

            db.Categories.Remove(categories);
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
