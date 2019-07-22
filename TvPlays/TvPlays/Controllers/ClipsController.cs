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
                foreach (string item in categoriasEscolhidas)
                {
                    //procurar o objeto de B
                    Categories category = db.Categories.Find(Convert.ToInt32(item));
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



                    return RedirectToAction("Index");
                }

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
            ViewBag.ListCategories = db.Categories.OrderBy(b => b.ID).ToList();
            return View(clips);
        }

        // POST: Clips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int? id, HttpPostedFileBase fileupload, ClipsDTO clips, string[] categoriasEscolhidas)
        {
            Utilizadores user = db.Utilizadores.SingleOrDefault(u => u.Email.Equals(User.Identity.Name));
            Clips clip = db.Clips.Find(id);


            if (user.ID.Equals(clip.UserFK))
            {

                // ler da BD o objeto que se pretende editar
                var aa = db.Clips.Include(b => b.ListCategories).Where(b => b.ID == id).SingleOrDefault();
                string path;

                if (fileupload != null) {

                    string fileName = Path.GetFileName(fileupload.FileName);
                    string cont = fileupload.ContentType;
                    path = Server.MapPath("~/App_Data/Videos/" + fileName);
                    fileupload.SaveAs(path);
                }else
                {
                    path = clip.PathClip;
                }


                // avaliar se os dados são 'bons'
                if (ModelState.IsValid)
                {
                    aa.TitleClip = clips.TitleClip;
                    aa.PathClip = path;
                    aa.DateClip = DateTime.Now;
                }
                else
                {
                    // gerar a lista de objetos de B que podem ser associados a A
                    ViewBag.ListCategories = db.Categories.OrderBy(b => b.ID).ToList();
                    // devolver o controlo à View
                    return View(clips);
                }

                // tentar fazer o UPDATE
                if (TryUpdateModel(aa, "", new string[] { nameof(aa.TitleClip), nameof(aa.PathClip), nameof(aa.DateClip), nameof(aa.ListCategories) }))
                {

                    // obter a lista de elementos de B
                    var categorias = db.Categories.ToList();

                    if (categoriasEscolhidas != null)
                    {
                        // se existirem opções escolhidas, vamos associá-las
                        foreach (var bb in categorias)
                        {
                            if (categoriasEscolhidas.Contains(bb.ID.ToString()))
                            {
                                // se uma opção escolhida ainda não está associada, cria-se a associação
                                if (!aa.ListCategories.Contains(bb))
                                {
                                    aa.ListCategories.Add(bb);
                                }
                            }
                            else
                            {
                                // caso exista associação para uma opção que não foi escolhida, 
                                // remove-se essa associação
                                aa.ListCategories.Remove(bb);
                            }
                        }
                    }
                    else
                    {
                        // não existem opções escolhidas!
                        // vamos eliminar todas as associações
                        foreach (var bb in categorias)
                        {
                            if (aa.ListCategories.Contains(bb))
                            {
                                aa.ListCategories.Remove(bb);
                            }
                        }
                    }

                    // guardar as alterações
                    db.SaveChanges();

                    // devolver controlo à View
                    return RedirectToAction("Index");
                }
           

            // se cheguei aqui, é pq alguma coisa correu mal
            ModelState.AddModelError("", "Alguma coisa correu mal...");
            }
            // gerar a lista de objetos de B que podem ser associados a A
            ViewBag.ListaObjetosDeB = db.Categories.OrderBy(b => b.ID).ToList();
            //ViewBag.UserFK = new SelectList(db.Utilizadores, "ID", "Name", clips.UserFK);
            // visualizar View...
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
