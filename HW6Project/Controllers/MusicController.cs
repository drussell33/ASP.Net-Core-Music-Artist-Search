using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HW6Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW6Project.Controllers
{
    public class MusicController : Controller
    {
        private MusicDbContext db;

        public MusicController(MusicDbContext context)
        {
            this.db = context;
            Debug.WriteLine(db.Artists.FirstOrDefault());
        }

        public IActionResult ArtistSearch(string searchQuery)
        {

            if (!String.IsNullOrEmpty(searchQuery) && searchQuery.Length > 1)
            {
                var artists = db.Artists.Where(s => s.Name.Contains(searchQuery));

                return View(artists.ToList());
            }

            return View();
        }

        public IActionResult AlbumGenerator(int artistId)
        {
            var albums = db.Albums.Where(s => s.ArtistId.Equals(artistId)).Include(x => x.Tracks).Include(a => a.Artist);

            return View(albums);
        }
    }
}
