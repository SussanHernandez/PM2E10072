using PM2E10072.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E10072.Controllers
{
    public class DBProc
    {
        readonly SQLiteAsyncConnection _connection;

        public DBProc() { }

        public DBProc(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);
            _connection.CreateTableAsync<Sitios>().Wait();
        }

        public Task<int> crearSitio(Sitios sitio)
        {
            if(sitio.id == 0)
            {
                return _connection.InsertAsync(sitio);
            }

            return _connection.UpdateAsync(sitio);
        }

        public Task<List<Sitios>> obtenerSitios()
        {
            return _connection.Table<Sitios>().ToListAsync();
        }

        public Task<Sitios> obtenerPorId(int id)
        {
            return _connection.Table<Sitios>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> borrarSitio(Sitios sitio)
        {
            return _connection.DeleteAsync(sitio);
        }
    }
}
