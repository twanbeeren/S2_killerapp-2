using System;
using Xunit;
using DAL;
using Models;
using Logic;

namespace UnitTesting
{
    public class UnitTests
    {
        private Account newaccount;
        private Song newsong;
        private Playlist newplaylist;
        private Rapport newrapport;
        private TestContext testContext = new TestContext();

        
        [Fact]
        private void CheckRegister()
        {
            bool register = true;
            newaccount = testContext.NewAccount();
            if(newaccount.Name == null || newaccount.Password == null || newaccount.Email == null)
            {
                register = false;
            }

            Assert.True(register);
        }

        [Fact]
        private void CheckRegisterEmail()
        {
            bool register = true;
            newaccount = testContext.NewAccount();
            testContext.AddAccountsToList();
            foreach(Account mcaccount in testContext.Accountslist)
            {
                if(mcaccount.Email == newaccount.Email)
                {
                    register = false;
                    throw new EmailAlreadyExcistsException("We have seen this email before");
                }
            }

            Assert.True(register);
        }
        [Fact]
        private void CheckRegisterGroftaalgebruik()
        {
            bool register = true;
            newaccount = testContext.NewAccount();
            if (testContext.Taalgebruiklijst().Contains(newaccount.Name))
            {
                register = false;
                throw new NameIsFoulLanguageException("Your contains strong language, please pick another username");
            }

            Assert.True(register);
        }

        [Fact]
        private void CheckRegisterPasswordLength()
        {
            Account_Logic accountLogic = new Account_Logic();
            Assert.False(accountLogic.Register(new Account("Bert", "12345", "bert@ernie.com")));
            Assert.True(accountLogic.Register(new Account("Bert", "123456", "bert@ernie.com")));
        }


        [Fact]
        public void CheckLog_in()
        {
            Account_Logic accountLogic = new Account_Logic();

            // Arrange
            Account account = new Account("Bert", "123456", "bert@ernie.com");
            Assert.False(accountLogic.Log_in(account.Name, account.Password));

            // Act
            Assert.True(accountLogic.Register(account));

            // Assert
            Assert.True(accountLogic.Log_in(account.Name, account.Password));
        }

        [Fact]
        public void CheckSearchSong()
        {
            bool search = false;
            newsong = testContext.NewSong();
            testContext.AddSongsToList();
            if (testContext.Songslist.Contains(newsong))
            {
                search = true;
            }

            Assert.False(search);
        }

        [Fact]
        public void CheckPlaylistAdd()
        {
            bool addplaylist = true;
            newplaylist = testContext.NewPlaylist();
            testContext.AddPlaylistsToList();
            foreach(Playlist playl in testContext.Playlists)
            {
                if(newplaylist.Name == playl.Name)
                {
                    addplaylist = false;
                }
            }

            Assert.True(addplaylist);
        }

        [Fact]
        public void CheckSongAddToPlaylist()
        {
            bool addsongs = true;
            newplaylist = testContext.NewPlaylist();
            newsong = testContext.NewSong();
            foreach(Song song in newplaylist.Songs)
            {
                if(newsong.Song_id == song.Song_id)
                {
                    addsongs = false;
                }
            }

            Assert.True(addsongs);
        }

        [Fact]
        public void CheckRapportAccount()
        {
            bool rapportaccount = true;
            newrapport = testContext.NewRapport();
            
            foreach(Rapport rapport in testContext.Rapports)
            {
                if(newrapport == rapport)
                {
                    rapportaccount = false;
                }
            }

            Assert.True(rapportaccount);
        }


    }
}
