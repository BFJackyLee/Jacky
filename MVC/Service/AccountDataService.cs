using MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVC.Service
{
    public class AccountDataService : IDisposable
    {
        private HankDBEntities _db = new HankDBEntities();

        public async Task<List<Account>> GetAccountTable()
        {
            try
            {
                return await _db.Account.ToListAsync();
            }
            catch
            {
                //log
                return new List<Account>();
            }
        }

        public async Task<bool> CreateAccount(Account account)
        {
            try
            {
                _db.Account.AddOrUpdate(account);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                //log
                return false;
            }
        }

        public async Task<bool> DeleteAccount(int sn)
        {
            try
            {
                var account = await _db.Account.FindAsync(sn);
                _db.Account.Remove(account);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                //log
                return false;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}