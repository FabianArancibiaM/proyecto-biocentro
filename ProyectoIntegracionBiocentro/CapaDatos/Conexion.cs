using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;


namespace CapaConexion
{
    public class Conexion
    {
        //Variables de instancia
        private String nombreBaseDeDatos;

        public String NombreBaseDeDatos
        {
            get { return nombreBaseDeDatos; }
            set { nombreBaseDeDatos = value; }
        }

        private String nombreTabla;

        public String NombreTabla
        {
            get { return nombreTabla; }
            set { nombreTabla = value; }
        }

        private String cadenaConexion;

        public String CadenaConexion
        {
            get { return cadenaConexion; }
            set { cadenaConexion = value; }
        }

        private String cadenaSQL;

        public String CadenaSQL
        {
            get { return cadenaSQL; }
            set { cadenaSQL = value; }
        }

        private SqlConnection dbConnection;

        public SqlConnection DbConnection
        {
            get { return dbConnection; }
            set { dbConnection = value; }
        }

        private DataSet dbDataSet;

        public DataSet DbDataSet
        {
            get { return dbDataSet; }
            set { dbDataSet = value; }
        }

        private SqlDataAdapter dbDataAdapter;

        public SqlDataAdapter DbDataAdapter
        {
            get { return dbDataAdapter; }
            set { dbDataAdapter = value; }
        }

        private Boolean esSelect;

        public Boolean EsSelect
        {
            get { return esSelect; }
            set { esSelect = value; }
        }

        //Variables creadas para el proyecto

        private Boolean esInsert;
        private int idAsignado;
        public Boolean EsInsert { get => esInsert; set => esInsert = value; }
        public int IdAsignado { get => idAsignado; set => idAsignado = value; }

        

        //Abrir la conexion

        public void abrir()
        {
            try
            {
                this.DbConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje Sistema", "Error al abrir la conexion " + ex.Message);
                return;
            } //Fin try abrir
        } // Fin Abrir
          

        //Metodo cerrar

        public void cerrar()
        {
            try
            {
                this.DbConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje Sistema", "Error al cerrar la conexion " + ex.Message);
                return;
            } //Fin try cerrar
           
        }//Fin cerrar

        //Metodo de coneccion

        public void conectar()
        { 
            //Se validan variables de instancia
            if (this.NombreBaseDeDatos.Length == 0)
            {
                MessageBox.Show("Mensaje Sistema", "Falta Nombre Base de Datos");
                return;
            }

            /*if (this.NombreTabla.Length == 0)
            {
                MessageBox.Show("Mensaje Sistema", "Falta Nombre Tabla");
                return;
            }*/

            if (this.CadenaConexion.Length == 0)
            {
                MessageBox.Show("Mensaje Sistema", "Falta Cadena Conexion");
                return;
            }

            if (this.CadenaSQL.Length == 0)
            {
                MessageBox.Show("Mensaje Sistema", "Falta cadena SQL");
                return;
            }

            //Se instancia la conexion

            try
            {
                this.DbConnection = new SqlConnection(this.CadenaConexion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mensaje Sistema", "Error de conexion " + ex.Message);
                return;

            } //Fin try conexion

            this.abrir();

            // Preguntar si es Select o no

            if (this.EsSelect)
            {
                //SE carga el dataSet
                this.DbDataSet = new DataSet();
                try
                {
                    this.DbDataAdapter = new SqlDataAdapter(this.CadenaSQL, this.DbConnection);
                    this.DbDataAdapter.Fill(this.DbDataSet, this.NombreTabla);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mensaje Sistema", "Error al cargar el DataSet " + ex.Message);
                    return;
                }

            }
            else
            { 
                //Se ejecutan las instrucciones INSERT - DELETE- UPDATE
                try
                {
                    SqlCommand variableSQL = new SqlCommand(this.CadenaSQL, this.DbConnection);
                    if (this.esInsert)
                    {
                        this.IdAsignado = (int)variableSQL.ExecuteScalar();
                    }
                    else
                    {
                        variableSQL.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mensaje Sistema", "Error SQL " + ex.Message);
                    return;
                }


            } //Fin if es select
            this.cerrar();

        } //Fin conectar

        //public static void Main(String[] args)
        //{
        //    Conexion conec1 = new Conexion();
        //    conec1.NombreBaseDeDatos = "martes";
        //    conec1.NombreTabla = "cliente";
        //    conec1.CadenaConexion = "Data Source=tamponi-pc;Initial Catalog=Martes;Integrated Security=True";
        //    conec1.CadenaSQL = "INSERT INTO " + conec1.NombreTabla +
        //                       " (rut,nombre) VALUES ('1112-3','El super Guru')";
        //    conec1.EsSelect = false;
        //    conec1.conectar();
        
        //}


    } // Fin Clase Conexion
}// Fin namespace
