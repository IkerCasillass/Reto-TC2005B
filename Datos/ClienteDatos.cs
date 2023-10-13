using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using WebLogin.Models;

namespace WebLogin.Datos
{
    public class ClienteDatos
    {
        public List<ClienteModel> Consultar()
        {
            var oListaClientes = new List<ClienteModel>();
            var con = new Conexion();
            string sql = "SELECT * FROM fn_clientes();";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oListaClientes.Add(new ClienteModel()
                    {
                        IdCliente = Convert.ToInt32(dr["idCliente"]),
                        Nombre = dr["nombre"].ToString(),
                        ApPaterno = dr["ap_paterno"].ToString(),
                        ApMaterno = dr["ap_materno"].ToString(),
                        Edad = Convert.ToInt32(dr["edad"]),
                        Rfc = dr["rfc"].ToString()
                    });
                }
            }
            con.CerrarConexion();
            return oListaClientes;
        }


        public bool Guardar(ClienteModel oCliente)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_clientes_insert (" + oCliente.IdCliente + ",'" + oCliente.Nombre + "','" + oCliente.ApPaterno + "','" + oCliente.ApMaterno + "'," + oCliente.Edad + ",'" + oCliente.Rfc + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Actualizar(ClienteModel oCliente)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_clientes_update (" + oCliente.IdCliente + ",'" + oCliente.Nombre + "','" + oCliente.ApPaterno + "','" + oCliente.ApMaterno + "'," + oCliente.Edad + ",'" + oCliente.Rfc + "')";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public bool Eliminar(int pIdCliente)
        {
            bool flag;
            var con = new Conexion();
            string sql = "CALL sp_clientes_delete (" + pIdCliente + ")";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, con.AbrirConexion());
            cmd.ExecuteNonQuery();
            flag = true;
            con.CerrarConexion();
            return flag;
        }

        public ClienteModel Obtener(int oIdCliente)
        {
            var oCliente = new ClienteModel();
            var cn = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM clientes WHERE idCliente='" + oIdCliente + "';", cn.AbrirConexion());
            cmd.CommandType = CommandType.Text;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oCliente.IdCliente = Convert.ToInt32(dr["idCliente"]);
                    oCliente.Nombre = dr["nombre"].ToString();
                    oCliente.ApPaterno = dr["ap_paterno"].ToString();
                    oCliente.ApMaterno = dr["ap_materno"].ToString();
                    oCliente.Edad = Convert.ToInt32(dr["edad"]);
                    oCliente.Rfc = dr["rfc"].ToString();
                }
            }
            return oCliente;
        }
    }
}
