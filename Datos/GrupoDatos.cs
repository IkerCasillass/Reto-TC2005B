using System;
using Npgsql;
using System.Data;
using WebLogin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WebLogin.Datos
{
    public class GrupoDatos
    {

        //public int Validar(int IdGrupo, string passwd)
        //{
        //    //GrupoDatos oGrupo = new GrupoDatos();
        //    int validado = 0;
        //    var con = new Conexion();
        //    try
        //    {
        //        NpgsqlCommand cmd = new NpgsqlCommand("SELECT estatus FROM Grupos WHERE idGrupo = " + IdGrupo + " AND passwd='" + passwd + "';", con.AbrirConexion());
        //        cmd.CommandType = CommandType.Text;

        //        using (var dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                validado = Convert.ToInt32(dr["estatus"].ToString());  
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string error = e.Message;
        //    }
        //    return validado;
        //}


        public List<GrupoModel> Consultar()
        {
            var oListaGrupos = new List<GrupoModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM Grupos ORDER BY nombre_grupo;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaGrupos.Add(new GrupoModel()
                        {
                            Nombre = dr["nombre_grupo"].ToString(),
                            Grado = Convert.ToInt32(dr["grado"]),
                            NumEstudiantes = Convert.ToInt32(dr["num_estudiantes"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrarla o lanzarla de nuevo)
                Console.WriteLine("Error al consultar la base de datos: " + ex.Message);
                throw ex;
            }
            finally
            {
                con.CerrarConexion(); // Asegurar que la conexión se cierre
            }

            return oListaGrupos;
        }


        public bool Guardar(GrupoModel oGrupo)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Grupos_insert (" /*+ oGrupo.IdGrupo*/ + "'" + oGrupo.Nombre + "'," + oGrupo.Grado + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        //public bool Actualizar(GrupoModel oGrupo)
        //{
        //    bool flag;
        //    var con = new Conexion();
        //    string sql = "CALL sp_Grupos_update (" + oGrupo.IdGrupo + ",'" + oGrupo.Nombre + "','" + oGrupo.ApPaterno + "','" + oGrupo.ApMaterno + "'," + oGrupo.Edad + "," + oGrupo.Grado + ")";
        //    NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
        //    cmd.ExecuteNonQuery();
        //    flag = true;
        //    con.CerrarConexion();
        //    return flag;
        //}

        public bool Eliminar(string nombre_grupo)
        {
            bool flag = false;
            GrupoModel oGrupo = Obtener(nombre_grupo);

            if(oGrupo.NumEstudiantes == 0)
            {
                var con = new Conexion();
                string sql = "CALL sp_Grupos_delete ('" + nombre_grupo + "')";
                NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
                cmd.ExecuteNonQuery();
                flag = true;
                con.CerrarConexion();
            }

            return flag;
        }

        public GrupoModel Obtener(string nombre_grupo)
        {
            var oGrupo = new GrupoModel();
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Grupos WHERE nombre_grupo='" + nombre_grupo + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oGrupo.Nombre = dr["nombre_grupo"].ToString();
                    oGrupo.Grado = Convert.ToInt32(dr["grado"]);
                    oGrupo.NumEstudiantes = Convert.ToInt32(dr["num_estudiantes"]);


                }
            }
            return oGrupo;
        }

        public List<GrupoModel> ListarAlumnosGrupo()
        {
            var oListaGrupos = new List<GrupoModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM Grupos ORDER BY nombre_grupo;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var grupo = new GrupoModel()
                        {
                            Nombre = dr["nombre_grupo"].ToString(),
                            Grado = Convert.ToInt32(dr["grado"]),
                            NumEstudiantes = Convert.ToInt32(dr["num_estudiantes"])
                        };

                        // Obtener la lista de estudiantes para este grupo
                        grupo.Estudiantes = ObtenerAlumnosPorGrupo(grupo.Nombre);

                        oListaGrupos.Add(grupo);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrarla o lanzarla de nuevo)
                Console.WriteLine("Error al consultar la base de datos: " + ex.Message);
                throw ex;
            }
            finally
            {
                con.CerrarConexion(); // Asegurar que la conexión se cierre
            }

            return oListaGrupos;
        }


        public List<AlumnoModel> ObtenerAlumnosPorGrupo(string nombre_grupo)
        {
            var oListaAlumnos = new List<AlumnoModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            //NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM alumnos WHERE id IN (SELECT id_alumno FROM grupos_alumnos WHERE grupo = '" + nombre_grupo + "');", con.AbrirConexion());
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM alumnos WHERE grupo='" + nombre_grupo + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaAlumnos.Add(new AlumnoModel()
                        {
                            IdAlumno = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
                            ApPaterno = dr["apellido_pat"].ToString(),
                            ApMaterno = dr["apellido_mat"].ToString(),
                            Edad = Convert.ToInt32(dr["edad"]),
                            Grado = Convert.ToInt32(dr["grado"]),
                            Grupo = dr["grupo"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción (por ejemplo, registrarla o lanzarla de nuevo)
                Console.WriteLine("Error al consultar la base de datos: " + ex.Message);
                throw ex;
            }
            finally
            {
                con.CerrarConexion(); // Asegurar que la conexión se cierre
            }

            return oListaAlumnos;
        }



    }
}

