using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPrototipo.Models
{
    public class Grado
    {
        public int GradoId { get; set; }
        public string Descripcion { get; set; }
        //Faltaria ver si un instructor puede tener mas de un grado?
        public List<Instructor> Instructores { get; set; }
    }
}
