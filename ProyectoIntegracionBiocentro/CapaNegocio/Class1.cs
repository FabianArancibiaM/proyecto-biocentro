using CapaConexion;
using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class Class1
    {
        private Conexion conexion;

        public Conexion Conexion { get => conexion; set => conexion = value; }

        public void configurarConexion()
        {
            this.conexion = new Conexion();
            this.conexion.NombreBaseDeDatos = "BIOCENTRO_DB";
            //Conex casa
            //this.conexion.CadenaConexion = "Data Source=DESKTOP-E5RAPBM\\SQLSERVER;Initial Catalog=BIOCENTRO_DB;Integrated Security=True";
            //Conex pega
            this.conexion.CadenaConexion = "Data Source=SAN-51KT2M2\\SQLEXPRESS;Initial Catalog=BIOCENTRO_DB;Integrated Security=True";
        }

        public void insertarUsuario(Persona perosona)
        {
            this.configurarConexion();
            this.conexion.CadenaSQL= "INSERT INTO BIOCENTRO_DB.dbo.PERSONA (RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO) OUTPUT INSERTED.ID_PERSONA VALUES " +
                " ('"+ perosona.Rut+ "','" + perosona.Nombre + "','" + perosona.ApellidoPaterno + "','" + perosona.ApellidoMaterno + "');";
            this.conexion.EsSelect = false;
            this.conexion.EsInsert = true;
            this.conexion.conectar();
            Console.WriteLine("Id Asignado es : "+ this.conexion.IdAsignado);
        }

        public List<RolUsuario> listarRoles()
        {
            this.configurarConexion();
            this.conexion.NombreTabla = "ROL_USUARIO";
            this.conexion.CadenaSQL = "SELECT * FROM BIOCENTRO_DB.dbo.ROL_USUARIO;";
            this.conexion.EsSelect = true;
            this.conexion.conectar();
            return retornarListaRolUsuario(this.conexion.DbDataSet);
        }

        private List<RolUsuario> retornarListaRolUsuario(DataSet ds)
        {
            List<RolUsuario> listRols = new List<RolUsuario>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                listRols.Add(generarObjectRolUsuario(row));
            }
            return listRols;
        }

        private RolUsuario generarObjectRolUsuario(DataRow row)
        {
            RolUsuario rolUsuario = new RolUsuario();
            rolUsuario.IdRol = Convert.ToInt32(row["ID_ROL"]);
            rolUsuario.Nombre = Convert.ToString(row["NOMBRE"]);
            return rolUsuario;
        }


    }
}
