using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DAL
{
    class SongContext : ISongContext
    {
        private SQLContext SQLqueries = new SQLContext();
        private List<Song> songs = new List<Song>();

        public SongContext()
        {
            songs.Add(new Song(new Artist("Tobu"), "Roots", 1283464));
            songs.Add(new Song(new Artist("Summer was fun"), "Run To You", 321263));
            songs.Add(new Song(new Artist("Nigel Good"), "Space Plus One", 426021));
        }

        public List<Song> GetAllSongs()
        {
            return songs;
        }



    }
}
