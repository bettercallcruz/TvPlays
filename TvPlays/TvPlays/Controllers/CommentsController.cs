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
        [Authorize(Roles = "Premium,Normal")]
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
        public ActionResult Create([Bind(Include = "ContComment")] Comments comments, int id)
        {
            if (ModelState.IsValid)
            {
                //Buscar o User associado e o clip
                Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
                Clips clip = db.Clips.Find(id);

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
                return View();
            }
            //Se o modelo nao tiver valiado apresentar erro
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Premium,Normal")]
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
        [Authorize(Roles = "Premium,Normal")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, ContComment")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
                Comments comment = db.Comments.Find(comments.ID);

                //Verificar se o utilizador Autenticado e mesmo o dono do Comment, se for realiza-se a remoção
                if (user.ID.Equals(comment.UtilizadoresFK))
                {
                    comment.ContComment = comments.ContComment;
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", "Clips", new { id = comment.ClipsFK });

                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }

            }
            ViewBag.ClipsFK = new SelectList(db.Clips, "ID", "TitleClip", comments.ClipsFK);
            ViewBag.UtilizadoresFK = new SelectList(db.Utilizadores, "ID", "Name", comments.UtilizadoresFK);
            return RedirectToAction("Details", "Clips", new { id = comments.ClipsFK });
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin,Premium,Normal")]
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

        [Authorize(Roles = "Admin,Premium,Normal")]
        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Ir buscar o User e o Comment a base de dados
            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
            Comments comments = db.Comments.Find(id);

            //Se for Admin pode apagar qualquer Comment antes da verificacao do User == null && comment == null 
            //porque o admin nao e um Utilizador mas sim um User da Identity
            if (User.IsInRole("Admin"))
            {
                db.Comments.Remove(comments);
                db.SaveChanges();
                return RedirectToAction("Details", "Clips", new { id = comments.ClipsFK });
            }

            //Verificar se eles existem
            if (user == null || comments == null)
            {
                return HttpNotFound();
            }

            //Verificar se o utilizador Autenticado e mesmo o dono do Comment, se for realiza-se a remoção
            if (user.ID.Equals(comments.UtilizadoresFK))
            {
                db.Comments.Remove(comments);
                db.SaveChanges();
                return RedirectToAction("Details", "Clips", new { id = comments.ClipsFK});
            } else
            {
                return new HttpStatusCodeResult(400);
            }

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
