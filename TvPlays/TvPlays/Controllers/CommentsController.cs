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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Clip).Include(c => c.User);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ClipsFK = new SelectList(db.Clips, "ID", "TitleClip");
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // E passado o id no Post por parametro para fazer a relacao
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContComment")] Comments comments, int? id)
        {
            if (ModelState.IsValid)
            {
                //Buscar o User asociado e o clip
                var user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
                var clip = db.Clips.Find(id);

                //retorna uma view de erro se nao encontrar o user ou o clip
                if(user == null || clip == null)
                {
                    return HttpNotFound();
                }

                //Criar o comentario
                var comment = new Comments
                {
                    ContComment = comments.ContComment,
                    DateComment = DateTime.UtcNow,
                    ClipsFK = clip.ID,
                    UtilizadoresFK = user.ID
                };

                //Adiciona a base de dados e Guarda
                db.Comments.Add(comment);
                db.SaveChanges();

                //Tentar dar redirect para os clips '/Clips/Details/X'

            }
            //Se o modelo nao tiver valiado apresentar erro
            return View(comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClipsFK = new SelectList(db.Clips, "ID", "TitleClip", comments.ClipsFK);
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", comments.UtilizadoresFK);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ContComment,DateComment,ClipsFK,UtilizadoresFK")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClipsFK = new SelectList(db.Clips, "ID", "TitleClip", comments.ClipsFK);
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", comments.UtilizadoresFK);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Ir buscar o User e o Comment a base de dados
            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
            Comments comments = db.Comments.Find(id);

            //Verificar se eles existem
            if(user == null || comments == null)
            {
                return HttpNotFound();
            }

            //Verificar se o utilizador Autenticado e mesmo o dono do Comment, se realiza-se a remoção
            if (user.ID.Equals(comments.UtilizadoresFK))
            {
                db.Comments.Remove(comments);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            else
            {
                //Erro porque o comment nao e dele
            }
            return View(comments);
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
