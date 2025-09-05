﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiBotica.SolPedido.Entidades.Core;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MiBotica.SolPedido.AccesoDatos.Core
{
    public class UsuarioDA
    {
        public List<Usuario> ListaUsuarios()
        {
            List<Usuario> listaEntidad = new List<Usuario>();
            Usuario entidad = null;

            using (SqlConnection conexion = new SqlConnection(
                ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["cnnSql"]].ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("paUsuarioLista", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    SqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        entidad = LlenarEntidad(reader);
                        listaEntidad.Add(entidad);
                    }
                }
                conexion.Close();
            }

            return listaEntidad;
        }

        public Usuario LlenarEntidad(IDataReader reader)
        {
            Usuario usuario = new Usuario();

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='IdUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["IdUsuario"]))
                    usuario.IdUsuario = Convert.ToInt32(reader["IdUsuario"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Clave'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Clave"]))
                {
                    // usuario.Clave = (byte[])reader["Clave"]; // si deseas mapear la clave
                }
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='CodUsuario'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["CodUsuario"]))
                    usuario.CodUsuario = Convert.ToString(reader["CodUsuario"]);
            }

            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName='Nombres'";
            if (reader.GetSchemaTable().DefaultView.Count.Equals(1))
            {
                if (!Convert.IsDBNull(reader["Nombres"]))
                    usuario.Nombres = Convert.ToString(reader["Nombres"]);
            }

            return usuario;
        }
        public void InsertarUsuario(Usuario usuario)
        {
            string csName = ConfigurationManager.AppSettings["cnnSql"] ?? "SQL";
            string connStr = ConfigurationManager.ConnectionStrings[csName].ConnectionString;

            using (SqlConnection cn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("paUsuarioInsertar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar, 50)
                               .Value = (object)usuario.CodUsuario ?? DBNull.Value;

                // -1 = VARBINARY(MAX)
                cmd.Parameters.Add("@Clave", SqlDbType.VarBinary, -1)
                               .Value = (object)usuario.Clave ?? DBNull.Value;

                cmd.Parameters.Add("@Nombres", SqlDbType.VarChar, 50)
                               .Value = (object)usuario.Nombres ?? DBNull.Value;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

