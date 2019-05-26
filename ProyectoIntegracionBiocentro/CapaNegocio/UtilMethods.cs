using CapaConexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UtilMethods
    {
        private Conexion conexion;

        private Conexion Conexion { get => conexion; set => conexion = value; }
        private void configurarConexion()
        {
            this.conexion = new Conexion();
            this.conexion.NombreBaseDeDatos = "BIOCENTRO_DB";
            //Conex casa
            this.conexion.CadenaConexion = "Data Source=DESKTOP-E5RAPBM\\SQLSERVER;Initial Catalog=BIOCENTRO_DB;Integrated Security=True";
            //Conex pega
            //this.conexion.CadenaConexion = "Data Source=SAN-51KT2M2\\SQLEXPRESS;Initial Catalog=BIOCENTRO_DB;Integrated Security=True";
        }

        public int? guardarEliminarActualizarObjeto(String sqlInsert,Boolean esInsert)
        {
            this.configurarConexion();
            this.conexion.CadenaSQL = sqlInsert;
            this.conexion.EsSelect = false;
            this.conexion.EsInsert = esInsert;
            this.conexion.conectar();
            return this.conexion.IdAsignado;
        }
        public DataSet listarObjetoConTablaEspecifica(String sqlSelect,String nombreTabla)
        {
            this.configurarConexion();
            this.conexion.NombreTabla = nombreTabla;
            this.conexion.CadenaSQL = sqlSelect;
            this.conexion.EsSelect = true;
            this.conexion.conectar();
            return this.conexion.DbDataSet;
        }

        public DataSet listarObjetoMultiTabla(String sqlSelect)
        {
            this.configurarConexion();
            this.conexion.CadenaSQL = sqlSelect;
            this.conexion.EsMasiva = true;
            this.conexion.conectar();
            return this.conexion.DbDataSet;
        }



    }
}
