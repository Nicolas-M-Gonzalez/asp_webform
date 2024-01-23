using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominiopokemon;

namespace pokemondatos
{
    public class Elementodatos
    {

        public List<Elemento> listar()
        {

             List <Elemento> lista = new List<Elemento> ();
            Accesodatos datos = new Accesodatos ();

            try
            {
                datos.setearconsulta("Select Id, Descripcion from ELEMENTOS");
                datos.ejecutarlectura();

                while (datos.lector.Read())
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int)datos.lector["id"];
                    aux.Descripcion = (string)datos.lector["Descripcion"];

                    lista.Add(aux);
                }
                return lista;
            }
            
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarconexion();
            }


        }



    }
}
