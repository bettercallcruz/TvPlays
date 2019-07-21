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

            // Incluir a lista de comentarios no Clip para ter acesso a eles
            var clips = db.Clips.Include(c => c.ListComments).FirstOrDefault(c => c.ID == id);

            //Se nao houver o clip dar View de erro  
            if (clips == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListCategories = db.Categories.OrderBy(b => b.ID).ToList();


            return View(clips);
        }

        //GET: video de cada Clip
        public ActionResult VideoClip(string pathClip)
        {
            var file = File(pathClip, "video/mp4");
            return file;
        }

        // GET: Clips/Create
        public ActionResult Create()
        {
            ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name");
            ViewBag.ListCategories = db.Categories.OrderBy(b => b.ID).ToList();
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
        public ActionResult Create(HttpPostedFileBase fileupload, ClipsDTO clips, string[] categoriasEscolhidas)
        {

            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));

            if (fileupload != null && user != null)
            {
                string fileName = Path.GetFileName(fileupload.FileName);
                string cont = fileupload.ContentType;
                string path = Server.MapPath("~/App_Data/Videos/" + fileName);
                fileupload.SaveAs(path);

                /// avalia se o array com a lista das escolhas de objetos de B associados ao objeto do tipo A 
                /// é nula, ou não.
                /// Só poderá avanção se NÃO for nula
                if (categoriasEscolhidas == null)
                {
                    ModelState.AddModelError("", "Necessita escolher pelo menos um valor de B para associar ao seu objeto de A.");
                    // gerar a lista de objetos de B que podem ser associados a A
                    ViewBag.ListCategories = db.Categories.OrderBy(b => b.ID).ToList();
                    // devolver controlo à View
                    return View(clips);
                }

                // criar uma lista com os objetos escolhidos de B
                List<Categories> listCategories = new List<Categories>();
                foreach (Categories item in listCategories)
                {
                    //procurar o objeto de B
                    Categories category = db.Categories.Find(item.ID);
                    // adicioná-lo à lista
                    listCategories.Add(category);
                }

                if (ModelState.IsValid)
                {
                    var clip = new Clips
                    {
                        TitleClip = clips.TitleClip,
                        PathClip = path,
                        DateClip = DateTime.Now,
                        UserFK = user.ID,
                        ListCategories = listCategories
                    };

                    db.Clips.Add(clip);
                    db.SaveChanges();
                    return View();
                }

                return View(clips);
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
        public ActionResult Edit([Bind(Include = "ID,TitleClip,DateClip,PathClip,UserFK")] Clips clips)
        {
            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
            Clips clip = db.Clips.Find(clips.ID);
            

            //Verificar se o utilizador Autenticado e se o mesmo é o dono do Clip, se for autoriza-se a alteração 
            if (user.ID.Equals(clip.UserFK))
            {
                clip.TitleClip = clips.TitleClip;
                db.Entry(clip).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            else
            {
                //Erro porque o comment nao e dele
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
            //Ir buscar o User e o Clip a base de dados
            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
            Clips clips = db.Clips.Find(id);

            //Se for Admin pode apagar qualquer Clip antes da verificacao do User == null && comment == null 
            //porque o admin nao e um Utilizador normal da aplicação
            if (User.IsInRole("Admin"))
            {
                db.Clips.Remove(clips);
                db.SaveChanges();
            }

            //Verifica se tanto o user como o clip existem e retorna 404 se nao encontrar algum dos dois
            if (user == null || clips == null)
            {
                return HttpNotFound();
            }

            //Verificar se o utilizador Autenticado e mesmo o dono do Clips, se for autoriza-se e concretiza-se a remoção
            if (user.ID.Equals(clips.UserFK))
            {
                db.Clips.Remove(clips);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            else
            {
                //Erro porque o comment nao e dele
            }
            return View(clips);
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
