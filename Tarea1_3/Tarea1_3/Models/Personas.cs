using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Tarea1_3.Models
{
    public class Personas
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(70)]
        public String name { get; set; }

        [MaxLength(70)]
        public String sname { get; set; }

        public int edad { get; set; }

        [MaxLength(100)]
        public String dir { get; set; }

        [MaxLength(100)]
        public String email { get; set; }


        public override string ToString(){return "Nombres: " + this.name + " | Apellidos: " + this.sname + " | Edad: " + this.edad;}

    }
}
