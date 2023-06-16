using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E10072.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id {  get; set; }

        public byte[] Imagen { get; set; }

        public double latitud { get; set; }

        public double longitud { get; set; }

        public string descripcion { get; set; }
        
    }
}
