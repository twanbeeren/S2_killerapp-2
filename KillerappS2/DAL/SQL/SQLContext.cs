using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using Models;

namespace DAL
{
    class SQLContext : ISQLContext
       
    {
        private const string connecstring = "Server=studmysql01.fhict.local;Uid=dbi385731;Database=dbi385731;Pwd=musicdatabase;SSLMode=none;";
        public void Register(Account account)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_Register", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Name", account.Name);
                        cmd.Parameters.AddWithValue("in_Password", account.Password);
                        cmd.Parameters.AddWithValue("in_Email", account.Email);
                        cmd.Parameters.AddWithValue("in_Language", "Nederlands");
                        cmd.Parameters.AddWithValue("in_Joined", DateTime.Now);
                        cmd.Parameters.AddWithValue("in_Blocked", "False");
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            //  context.Register();
        }

        public bool Login(string name, string password)
        {
            int UserExist = 0;

            using (MySqlConnection conn = new MySqlConnection(connecstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_Login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("in_Username", name);
                    cmd.Parameters.AddWithValue("in_Password", password);
                    cmd.ExecuteNonQuery();
                    UserExist = Convert.ToInt32(cmd.ExecuteScalar());
                }
                conn.Close();
            }
            
            if (UserExist == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void GetAccount_id(Account account)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetAccountId", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Name", account.Name);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    account.Account_Id = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GetSong_id(Song song)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetSongIdBySearch", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Title", "%" + song.Title + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    song.Song_id = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void GetPlaylist_id(Playlist playlist)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetPlaylistIdBySearch", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Name", playlist.Name + "%");
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    playlist.Playlist_id = reader.GetInt32(0);
                                }
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<Playlist>GetAllPlaylistsDatabase(Account account)
        {
            List<Playlist> AllPlaylists = new List<Playlist>();

            GetAccount_id(account);
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetPlaylistsByAccount_Id", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Account_id", account.Account_Id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Playlist playlist;
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    playlist = new Playlist(reader.GetString(0));
                                    AllPlaylists.Add(playlist);
                                }
                            }

                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return AllPlaylists;

        }

        public List<Account> GetAllFriendsDatabase(Account account)
        {
            List<Account> AllFriends = new List<Account>();

            GetAccount_id(account);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetFriendsByAccount_Id", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Account_id", account.Account_Id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Account friend;
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    friend = new Account(reader.GetString(0));
                                    AllFriends.Add(friend);
                                }
                            }
                            
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return AllFriends;

        }

        public List<Song> GetAllSongsDatabase(Song song)
        {
            List<Artist> ArtistsSong = new List<Artist>();
            List<Song> AllSongs = new List<Song>();

            GetSong_id(song);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetArtistNameBySong_Id", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Song_id", song.Song_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Song newsong = null;
                            while (reader.Read())
                            {
                                if (newsong == null)
                                {
                                    newsong = new Song(new Artist(reader.GetString(0)), reader.GetString(1), reader.GetInt32(2));
                                }
                                newsong.AddArtist(new Artist(reader.GetString(0)));
                                AllSongs.Add(newsong);
                            }
                            
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return AllSongs;

        }

        public Playlist GetPlaylist(Playlist playlist)
        {
            Playlist playlistsongs = new Playlist();

            GetPlaylist_id(playlist);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_GetList_SongsByPlaylist_Id", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Playlist_id", playlist.Playlist_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    
                                }
                            }
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return playlist;

        }

        public void UpdateSong_Total_Streams(Song song)
        {
            GetSong_id(song);

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connecstring))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("s2_killerapp_UpdateTotal_Streams", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("in_Song_id", song.Song_id);
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
    }
}
