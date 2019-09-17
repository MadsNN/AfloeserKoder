using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Afloeser.Models
{
    public class TemporaryUser
    {
        public int ID { get; set; }
        public string Kategori { get; set; }
        public string TempCode_username { get; set; }
        public DateTime StartDato { get; set; }
        public DateTime SlutDato { get; set; }
        public string Opretter { get; set; }


        public TemporaryUser()
        {
        }

        public TemporaryUser(int id, string kategori, string tempCode_username)
        {
            ID = id;
            Kategori = kategori;
            TempCode_username = tempCode_username;
        }

        public TemporaryUser(int id, string kategori, string tempCode_username, DateTime startDato, DateTime slutDato, string opretter)
        {
            ID = id;
            Kategori = kategori;
            TempCode_username = tempCode_username;
            StartDato = startDato;
            SlutDato = slutDato;
            Opretter = opretter;
        }
    }
}