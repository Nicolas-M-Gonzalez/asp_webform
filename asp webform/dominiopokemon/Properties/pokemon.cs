using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominiopokemon
{
    public class Pokemon
    {
        //clase que va a modelar los pokemon


        public int Id { get; set; }

        public string Nombre { get; set; }
        [DisplayName ("Número")]
        //sirve para cambiarle el nombre o agregarle. se pone arriba de la propi que queres cambiar.
        public int Numero { get; set; }
        [DisplayName ("Descripción")]
        public string Descripcion { get; set; }

        public string Urlimagen { get; set; }

        public Elemento Tipo { get; set; }   
        //propiedad de tipo elemento, tipo es un objeto de elemento
        public Elemento Debilidad { get; set; }

    }
}
