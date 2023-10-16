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
    public class AsignaturaDatos
    {

        //public int Validar(int IdAsignatura, string passwd)
        //{
        //    //AsignaturaDatos oAsignatura = new AsignaturaDatos();
        //    int validado = 0;
        //    var con = new Conexion();
        //    try
        //    {
        //        NpgsqlCommand cmd = new NpgsqlCommand("SELECT estatus FROM Asignaturas WHERE idAsignatura = " + IdAsignatura + " AND passwd='" + passwd + "';", con.AbrirConexion());
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


        public List<AsignaturaModel> Consultar()
        {
            var oListaAsignaturas = new List<AsignaturaModel>();
            var con = new Conexion();
            NpgsqlConnection conn = con.AbrirConexion();

            string sql = "SELECT * FROM Asignaturas ORDER BY id;";
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
                            IdAsignatura = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
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


        public bool Guardar(AsignaturaModel oAsignatura)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Asignaturas_insert (" /*+ oAsignatura.IdAsignatura*/ + "'" + oAsignatura.Nombre + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Actualizar(AsignaturaModel oAsignatura)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Asignaturas_update (" + oAsignatura.IdAsignatura + ",'" + oAsignatura.Nombre + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Eliminar(int IdAsignatura)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_Asignaturas_delete (" + IdAsignatura + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public AsignaturaModel Obtener(int idAsignatura)
        {
            var oAsignatura = new AsignaturaModel();
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Asignaturas WHERE id='" + idAsignatura + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oAsignatura.IdAsignatura = Convert.ToInt32(dr["id"]);
                    oAsignatura.Nombre = dr["nombre"].ToString();
                }
            }
            return oAsignatura;
        }

        public AsignaturaModel Obtener(string Nombre)
        {
            var oAsignatura = new AsignaturaModel();
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM Asignaturas WHERE nombre='" + Nombre + "';", con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oAsignatura.IdAsignatura = Convert.ToInt32(dr["id"]);
                    oAsignatura.Nombre = dr["nombre"].ToString();
                }
            }
            return oAsignatura;
        }


    }
}

