using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominiopokemon;
//incluis la libreria sql

namespace pokemondatos
{
    public class Accesodatos
    {

        private SqlConnection Conexion;
        public SqlCommand Comando;
        private SqlDataReader Lector;
        //atributos/variables necesarios para manipular la base de datos

        public SqlDataReader lector

        {
            get { return Lector; } 
        }
        //esta propiedad sirve para leer solamente los datos.

        public Accesodatos ()
        {
            Conexion = new SqlConnection ("server = DESKTOP-497OCOR\\SQLEXPRESS01; DataBase=POKEDEX_DB; integrated security = true ");
            Comando = new SqlCommand();
        }
        //creamos el constructor para que cuando nace mi objeto se conecte a mi base de datos 
        // directamente.

        public void setearconsulta(string consulta)
        {
            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = consulta;
        }
        //de esta manera seteo la consulta que hice en pokemondatos.

        public void ejecutarlectura()
        {
            Comando.Connection = Conexion;

            try
            {
                Conexion.Open();
                Lector = Comando.ExecuteReader();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            //esta es la funcion que ejecuta la lectura.
        }

        public void Ejecutaraccion()
        {
            Comando.Connection = Conexion;

            try
            {
                Conexion.Open();

                Comando.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void Setearparametro(string nombre, object valor )
        //se crea la funcion para setear los valore que se agregaron con @.
        //por parametro que reciba el nombre y el valor.
        {
            Comando.Parameters.AddWithValue( nombre, valor);
            //te deja agregarle el nombre con el valor.
        }

        public void cerrarconexion()
        {
            if ( lector != null)
            {
                lector.Close();
            }

            Conexion.Close();
        }
        //cierro el lector y la conexion.

       

    }
}
