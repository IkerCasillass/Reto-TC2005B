using System;
using Npgsql;
using System.Data;
using WebReto.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace WebReto.Datos
{
    public class AlumnoDatos
    {

        //public int Validar(int IdAlumno, string passwd)
        //{
        //    //AlumnoDatos oAlumno = new AlumnoDatos();
        //    int validado = 0;
        //    var con = new Conexion();
        //    try
        //    {
        //        NpgsqlCommand cmd = new NpgsqlCommand("SELECT estatus FROM alumnos WHERE idAlumno = " + IdAlumno + " AND passwd='" + passwd + "';", con.AbrirConexion());
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


        public List<AlumnoModel> Consultar()
        {
            var oListaAlumnos = new List<AlumnoModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM alumnos ORDER BY id;";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
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


        public bool Guardar(AlumnoModel oAlumno)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_alumnos_insert (" /*+ oAlumno.IdAlumno*/ + "'" + oAlumno.Nombre + "','" + oAlumno.ApPaterno + "','" + oAlumno.ApMaterno + "'," + oAlumno.Edad + ","  + oAlumno.Grado + ",'" + oAlumno.Grupo + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Actualizar(AlumnoModel oAlumno)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_alumnos_update (" + oAlumno.IdAlumno + ",'" + oAlumno.Nombre + "','" + oAlumno.ApPaterno + "','"  + oAlumno.ApMaterno + "'," + oAlumno.Edad + "," + oAlumno.Grado + ",'" + oAlumno.Grupo +"')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Eliminar(int IdAlumno)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_alumnos_delete (" + IdAlumno + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public AlumnoModel Obtener(int idAlumno)
        {
            var oAlumno = new AlumnoModel();
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM alumnos WHERE id='" + idAlumno + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oAlumno.IdAlumno = Convert.ToInt32(dr["id"]);
                    oAlumno.Nombre = dr["nombre"].ToString();
                    oAlumno.ApPaterno = dr["apellido_pat"].ToString();
                    oAlumno.ApMaterno = dr["apellido_mat"].ToString();
                    oAlumno.Edad = Convert.ToInt32(dr["edad"]);
                    oAlumno.Grado = Convert.ToInt32(dr["grado"]);
                    oAlumno.Grupo = dr["grupo"].ToString();


                }
            }
            return oAlumno;
        }


    }
}

