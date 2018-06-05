using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Models;

namespace Logic
{
    public class Song_Logic
    {
        Repository repo = new Repository();

        public List<Song> GetAllSongs()
        {
            return repo.GetAllSongs();
        }

        public List<Song> GetAllSongsDatabase(Song song)
        {
            return repo.GetAllSongsDatabase(song);
        }

        public void UpdateTotal_Streams(Song song)
        {
            repo.UpdateTotal_Streams(song);
        }
    }
}
