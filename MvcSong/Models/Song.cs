using System;
using System.Data.Entity;

namespace MvcSong.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
    public class SongDBContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
    }
}