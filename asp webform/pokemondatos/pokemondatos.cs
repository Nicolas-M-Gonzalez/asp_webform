using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominiopokemon;
using pokemondatos;


namespace pokemondatos
{
    public class Pokemondatos
    {
        //clase que va a conectar la base de datos.

        public List<Pokemon> Listar()
        {
            //creamos un metodo/funcion que va a ser una lista de pokemon llamada listar
            // que lea de la base de datos.


            List<Pokemon> Lista = new List<Pokemon>();
            //creamos la lista.

            SqlConnection Conexion = new SqlConnection();
            //establecemos una conexion.

            SqlCommand Comando = new SqlCommand();
            //se crea el objeto para realizar comandos.

            SqlDataReader Lector;
            //para leer los datos se crea esta variable.




            try
            {

                Conexion.ConnectionString = "server = DESKTOP-497OCOR\\SQLEXPRESS01; DataBase=POKEDEX_DB; integrated security = true ";
                //nombre de tu base de datos./de la tabla / seguridad integrada.objeto

                Comando.CommandType = System.Data.CommandType.Text;
                //como voy a inyectar la consulta.objeto

                //Comando.CommandText = "select nombre ,numero, descripcion,urlimagen from POKEMONS";
                //la consulta

                Comando.CommandText = "select numero,nombre, P.descripcion,urlimagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id  from POKEMONS P,ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo And D.Id = P.IdDebilidad ";
                //consulta con relaciones. objeto

                Comando.Connection = Conexion;
                //todo el comando anterior se va a establecer con la palabra conexion.objeto

                Conexion.Open();
                //abro la conexion.

                Lector = Comando.ExecuteReader();
                //realizo la lectura en la variable lector.

                while (Lector.Read())
                {
                    //se va a fijar si hay una lectura; si la hay ? te posiciona en la primera fila.

                    Pokemon aux = new Pokemon();
                    //creamos el objeto y lo cargamos.

                    aux.Id = (int)Lector["Id"];

                    aux.Numero = (int)Lector["numero"];
                    aux.Nombre = (string)Lector["nombre"];

                    if (!(Lector["UrlImagen"] is DBNull))
                    aux.Urlimagen = (string)Lector["UrlImagen"];

                    aux.Descripcion = (string)Lector["descripcion"];
                    
                    aux.Tipo = new Elemento();
                    //creamos el objeto tipo de tipo elemento.
                    aux.Tipo.Id = (int)Lector["IdTipo"];

                    aux.Tipo.Descripcion = (string)Lector["tipo"];
                    //le pedimos estos datos

                    aux.Debilidad = new Elemento();
                    //creamos el objeto debilidad de tipo elemento.
                    aux.Debilidad.Id = (int)Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)Lector["Debilidad"];
                    //le pedimos estos datos.

                    //carga los datos de la base de datos

                    


                    Lista.Add(aux);
                }

                Conexion.Close();
                //cierro la conexion.
                return Lista;
                //devuelve una lista.
            }

            catch (Exception ex)
            {

                throw ex;

            }


        }
        public void Agregar(Pokemon nuevo)
        {

            Accesodatos datos = new Accesodatos();

            try 
            {
                datos.setearconsulta( "Insert into POKEMONS ( Numero , Nombre , Descripcion , Activo , IdTipo , IdDebilidad, urlImagen )values('" +nuevo.Numero+ "' , '" +nuevo.Nombre +"' ,'" + nuevo.Descripcion + "' , 1 , @IdTipo , @IdDebilidad, @urlImagen)");
                //seteo la consulta.
                datos.Setearparametro("@IdTipo", nuevo.Tipo.Id);
                datos.Setearparametro("@IdDebilidad", nuevo.Debilidad.Id);
                datos.Setearparametro("@urlimagen", nuevo.Urlimagen);
                //por parametro le doy el nombre y el tipo nuevo agregado.
                
                datos.Ejecutaraccion();
               
                
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

        public void Modificar (Pokemon poke)
        
        {
            Accesodatos datos = new Accesodatos();
            //pido lo datos.

            try 
            {
                datos.setearconsulta("update POKEMONS set Numero = @numero , Nombre = @nombre , Descripcion = @Descripcion , UrlImagen = @imagen , IdTipo = @IdTipo , IdDebilidad = @IdDebilidad Where Id = @Id ");
                //seteo la consulta.
                datos.Setearparametro("@numero", poke.Numero);
                datos.Setearparametro("@nombre", poke.Nombre);
                datos.Setearparametro("@Descripcion", poke.Descripcion);
                datos.Setearparametro("@imagen", poke.Urlimagen);
                datos.Setearparametro("@IdTipo", poke.Tipo.Id);
                datos.Setearparametro("@IdDebilidad", poke.Debilidad.Id);
                datos.Setearparametro("@Id", poke.Id);
                //le mando los datos.

                datos.Ejecutaraccion();
                //le digo que ejecute la accion.
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
    
    
        public void Eliminar( int Id)
        //se crea la funcion eliminar para que pueda eliminar un registro a traves de de Id.
        {
            try
            {

                Accesodatos datos = new Accesodatos();
                //creo el objeto para que entre ala base de datos.

                datos.setearconsulta(" delete from Pokemons where id = @id");
                //creo que la consulta sql en la base de datos para eliminar un registro de la base de datos.

                datos.Setearparametro("@id", Id);
                //le resolvemos el parametro de la funcion.

                datos.Ejecutaraccion();
                //ejecuto la eliminacion.
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    
    }
      

   
}
