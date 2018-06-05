using System;
using System.Collections.Generic;
using System.Text;
using Models;
using DAL;

namespace Logic
{
    public class Playlist_Logic
    {
        Repository repo = new Repository();

        public List<Playlist> GetAllPlaylists()
        {
            return repo.GetAllPlaylists();
        }

        public List<Playlist> GetAllPlaylistsDatabase(Account account)
        {
            return repo.GetAllPlaylistsDatabase(account);
        }
    }
}
