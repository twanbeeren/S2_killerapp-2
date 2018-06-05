using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Models;

namespace Logic
{
    public class Account_Logic
    {
        Repository repo = new Repository();

        public bool Log_in(string name, string password)
        {
             return repo.Login(name, password);
        }

        public bool Register(Account account)
        {
            if (account.Password.Length < 6)
            {
                return false;
            }
            repo.Register(account);
            return true;
        }

        public List<Account> GetAllFriendsDatabase(Account account)
        {
            return repo.GetAllFriendsDatabase(account);
        }
        
    }
}
