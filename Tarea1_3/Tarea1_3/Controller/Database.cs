using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tarea1_3.Models;
using SQLite;


namespace Tarea1_3.Controller
{
    public class Database
    {
        readonly SQLiteAsyncConnection db;

        public Database(String pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);
            db.CreateTableAsync<Models.Personas>().Wait();
        }

        public Task<List<Personas>> getListPersonas()
        {
            return db.Table<Personas>().ToListAsync();
        }

        public Task<Personas> getPersonById(int ids)
        {
            return db.Table<Personas>()
                .Where(i => i.id == ids)
                .FirstOrDefaultAsync();
        }

        public Task<int> savePersona(Personas user)
        {
            if(user.id !=0)
            {
                return db.UpdateAsync(user);
            } 
            else
            {
                return db.InsertAsync(user);
            }

        }

        public Task<int> deletePerson(Personas user)
        {
            return db.DeleteAsync(user);
        }
    }
}
