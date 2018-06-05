using System;
using System.Collections.Generic;
using System.Text;
using Models;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class Repository
    {
        private readonly ISongContext songcontext = new SongContext();
        private readonly IPlaylistContext playlistcontext = new PlaylistContext();
        private readonly ISQLContext sqlcontext = new SQLContext();

        public List<Song> GetAllSongs()
        {
            return songcontext.GetAllSongs();
        }

        public List<Playlist> GetAllPlaylists()
        {
            return playlistcontext.GetAllPlaylists();
        }

        public void Register(Account account)
        {
            sqlcontext.Register(account);
        }

        public bool Login(string name, string password)
        {
            if(sqlcontext.Login(name, password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Song> GetAllSongsDatabase(Song song)
        {
            return sqlcontext.GetAllSongsDatabase(song);
        }

        public List<Account> GetAllFriendsDatabase(Account account)
        {
            return sqlcontext.GetAllFriendsDatabase(account);
        }

        public List<Playlist> GetAllPlaylistsDatabase (Account account)
        {
            return sqlcontext.GetAllPlaylistsDatabase(account);
        }

        public void UpdateTotal_Streams(Song song)
        {
            sqlcontext.UpdateSong_Total_Streams(song);
        }

    }
}
