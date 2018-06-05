using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DAL
{
    interface ISQLContext
    {
        void Register(Account account);
        bool Login(string name, string password);
        List<Song> GetAllSongsDatabase(Song song);
        List<Account> GetAllFriendsDatabase(Account account);
        List<Playlist> GetAllPlaylistsDatabase(Account account);
        Playlist GetPlaylist(Playlist playlist);
        void UpdateSong_Total_Streams(Song song);



    }
}
