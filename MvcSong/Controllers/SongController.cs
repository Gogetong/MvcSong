using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSong.Models;

namespace MvcSong.Controllers
{
    public class SongController : Controller
    {
        private SongDBContext db = new SongDBContext();

        //
        // GET: /Song/

         public ActionResult Index(string SongGenre, string searchString)
            {
            var GenreLst = new List<string>();

           var GenreQry = from d in db.Songs
                   orderby d.Genre
                   select d.Genre;

             GenreLst.AddRange(GenreQry.Distinct());
             ViewBag.SongGenre = new SelectList(GenreLst);

           var movies = from m in db.Songs
                 select m;

           if (!String.IsNullOrEmpty(searchString))
            {
             movies = movies.Where(s => s.Title.Contains(searchString));
            }

           if (!string.IsNullOrEmpty(SongGenre))
               {
        movies = movies.Where(x => x.Genre == SongGenre);
             }

         return View(movies);
}

        //
        // GET: /Song/Details/5

        public ActionResult Details(int id = 0)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        //
        // GET: /Song/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Song/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Song song)
        {
            if (ModelState.IsValid)
            {
                db.Songs.Add(song);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(song);
        }

        //
        // GET: /Song/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        //
        // POST: /Song/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Song song)
        {
            if (ModelState.IsValid)
            {
                db.Entry(song).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(song);
        }

        //
        // GET: /Song/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Song song = db.Songs.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        //
        // POST: /Song/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Songs.Find(id);
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}