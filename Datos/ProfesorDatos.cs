using System;
using Npgsql;
using System.Data;
using WebReto.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WebReto.Datos
{
    public class ProfesorDatos
    {

        //public int Validar(int IdProfesor, string passwd)
        //{
        //    //ProfesorDatos oProfesor = new ProfesorDatos();
        //    int validado = 0;
        //    var con = new Conexion();
        //    try
        //    {
        //        NpgsqlCommand cmd = new NpgsqlCommand("SELECT estatus FROM Profesores WHERE idProfesor = " + IdProfesor + " AND passwd='" + passwd + "';", con.AbrirConexion());
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


        public List<ProfesorModel> Consultar()
        {
            var oListaProfesores = new List<ProfesorModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM Profesores ORDER BY id;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaProfesores.Add(new ProfesorModel()
                        {
                            IdProfesor = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
                            ApPaterno = dr["apellido_pat"].ToString(),
                            ApMaterno = dr["apellido_mat"].ToString(),
                            Edad = Convert.ToInt32(dr["edad"]),
                            Telefono = Convert.ToString(dr["telefono"]),
                            Email = Convert.ToString(dr["email"])
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

            return oListaProfesores;
        }

        public List<AsignaturaModel> ConsultarAsignaturas(int idProfesor)
        {
            var oListaAsignaturas = new List<AsignaturaModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM consultar_asignaturas_por_profesor('"+ idProfesor + "');";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oListaAsignaturas.Add(new AsignaturaModel()
                        {
                            Nombre = dr["nombre_asignatura"].ToString(),
                            Grupo = dr["nombre_grupo"].ToString()
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

            return oListaAsignaturas;
        }

        public bool GuardarAsignatura(ProfesorModel oProfesor)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Profesores_insert (" /*+ oProfesor.IdProfesor*/ + "'" + oProfesor.Nombre + "','" + oProfesor.ApPaterno + "','" + oProfesor.ApMaterno + "'," + oProfesor.Edad + ",'" + oProfesor.Telefono + "','" + oProfesor.Email + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }


        public bool Guardar(ProfesorModel oProfesor)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Profesores_insert (" /*+ oProfesor.IdProfesor*/ + "'" + oProfesor.Nombre + "','" + oProfesor.ApPaterno + "','" + oProfesor.ApMaterno + "'," + oProfesor.Edad + ",'" + oProfesor.Telefono + "','" + oProfesor.Email + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Actualizar(ProfesorModel oProfesor)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Profesores_update (" + oProfesor.IdProfesor + ",'" + oProfesor.Nombre + "','" + oProfesor.ApPaterno + "','" + oProfesor.ApMaterno + "'," + oProfesor.Edad + ",'" + oProfesor.Telefono + "','" + oProfesor.Email + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Eliminar(int IdProfesor)
        {
            bool flag;
            //var con = new Conexion();
            //string sql = "CALL sp_Profesores_delete (" + IdProfesor + ")";
            //NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            //cmd.ExecuteNonQuery();
            //flag = true;
            //con.CerrarConexion();
            //return flag;

            var con = new Conexion();

            // Llama a la función para eliminar referencias
            string sqlFunction = "SELECT fn_EliminarReferenciasProfesor(" + IdProfesor + ")";
            NpgsqlCommand cmdFunction = new NpgsqlCommand(sqlFunction, con.AbrirConexion());
            cmdFunction.ExecuteNonQuery();

            // Luego llama al procedimiento almacenado sp_Grupos_delete
            string sqlProcedure = "CALL sp_Profesores_delete (" + IdProfesor + ")";
            NpgsqlCommand cmdProcedure = new NpgsqlCommand(sqlProcedure, con.AbrirConexion());
            cmdProcedure.ExecuteNonQuery();

            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public ProfesorModel Obtener(int idProfesor)
        {
            var oProfesor = new ProfesorModel();
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Profesores WHERE id='" + idProfesor + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oProfesor.IdProfesor = Convert.ToInt32(dr["id"]);
                    oProfesor.Nombre = dr["nombre"].ToString();
                    oProfesor.ApPaterno = dr["apellido_pat"].ToString();
                    oProfesor.ApMaterno = dr["apellido_mat"].ToString();
                    oProfesor.Edad = Convert.ToInt32(dr["edad"]);
                    oProfesor.Telefono = Convert.ToString(dr["telefono"]);
                    oProfesor.Email = Convert.ToString(dr["email"]);


                }
            }
            return oProfesor;
        }


    }
}

