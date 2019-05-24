using CapaDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class GestionUsuarios
    {

        private UtilMethods utilMethods;

        public GestionUsuarios()
        {
            this.utilMethods = new UtilMethods();
        }

        public List<Terapeuta> buscarTerapeutas(int id)
        {
            try
            {
                String queryTerapeuta = "SELECT ID_TERAPEUTA,ID_USUARIO,ID_ESPECIALIDAD FROM BIOCENTRO_DB.dbo.TERAPEUTA WHERE ID_ESPECIALIDAD = "+id;
                DataSet dataBdTerapeuta = this.utilMethods.listarObject(queryTerapeuta, "TERAPEUTA");
                return generarListTerapeuta(dataBdTerapeuta);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }

        public List<Especialidad> generarListaEspecialidad()
        {
            try
            {
                String queryTerapeuta = "SELECT ID_ESPECIALIDAD,NOMBRE FROM BIOCENTRO_DB.dbo.ESPECIALIDAD";
                DataSet dataBdTerapeuta = this.utilMethods.listarObject(queryTerapeuta, "ESPECIALIDAD");
                List<Especialidad> list = new List<Especialidad>();
                foreach (DataRow row in dataBdTerapeuta.Tables[0].Rows)
                {
                    list.Add(generarObjetEspecialidad(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo buscarTerapeutas ", ex);
            }
            return null;
        }
        public List<RolUsuario> generarListaRoles()
        {
            try
            {
                String query = "SELECT * FROM BIOCENTRO_DB.dbo.ROL_USUARIO";
                DataSet data = this.utilMethods.listarObject(query, "ROL_USUARIO");
                List<RolUsuario> list = new List<RolUsuario>();
                foreach (DataRow row in data.Tables[0].Rows)
                {
                    list.Add(generarObjectRolUsuario(row));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarListaRoles ", ex);
            }
            return null;
        }

        private List<Terapeuta> generarListTerapeuta(DataSet dataBdTerapeuta)
        {
            List<Terapeuta> list = new List<Terapeuta>();
            foreach (DataRow row in dataBdTerapeuta.Tables[0].Rows)
            {
                list.Add(generarObjectTerapeuta(row));
            }
            return list;
        }

        private Terapeuta generarObjectTerapeuta(DataRow row)
        {
            Terapeuta terapeuta = new Terapeuta();
            terapeuta.IdUsuario = generarObjetUsuarioPorId(Convert.ToInt32(row["ID_USUARIO"]));
            terapeuta.IdEspecialidad = generarObjetEspecialidadPorId(Convert.ToInt32(row["ID_ESPECIALIDAD"]));
            return terapeuta;
        }

        private Usuario generarObjetUsuarioPorId(int v)
        {
            try
            {
                String queryUsuario = "SELECT ID_USUARIO,ID_ROL,ID_PERSONA,ESTADO FROM BIOCENTRO_DB.dbo.USUARIO WHERE ID_USUARIO = "+v;
                DataSet dataBdUsuario = this.utilMethods.listarObject(queryUsuario, "USUARIO");
                if (dataBdUsuario != null)
                {
                    DataRow row = dataBdUsuario.Tables[0].Rows[0];
                    Usuario usuario = new Usuario();
                    usuario.IdUsuario = Convert.ToInt32(row["ID_USUARIO"]);
                    usuario.IdRol = null;
                    usuario.IdPersona = generarObjetPersona(Convert.ToInt32(row["ID_PERSONA"]));
                    usuario.Estado = Convert.ToInt32(row["ESTADO"]);
                    return usuario;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetUsuario ", ex);
            }
            return null;
        }

        private Persona generarObjetPersona(int v)
        {
            try
            {
                String queryUsuario = "SELECT ID_PERSONA,RUT,NOMBRE,APELLIDO_PATERNO,APELLIDO_MATERNO FROM BIOCENTRO_DB.dbo.PERSONA WHERE ID_PERSONA = " + v;
                DataSet dataBdUsuario = this.utilMethods.listarObject(queryUsuario, "PERSONA");
                if (dataBdUsuario != null)
                {
                    DataRow row = dataBdUsuario.Tables[0].Rows[0];
                    Persona persona = new Persona();
                    persona.IdPersona = Convert.ToInt32(row["ID_PERSONA"]);
                    persona.Rut = Convert.ToString(row["RUT"]);
                    persona.Nombre = Convert.ToString(row["NOMBRE"]);
                    persona.ApellidoPaterno = Convert.ToString(row["APELLIDO_PATERNO"]);
                    persona.ApellidoMaterno = Convert.ToString(row["APELLIDO_MATERNO"]);
                    return persona;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetPersona ", ex);
            }
            return null;
        }

        private Especialidad generarObjetEspecialidadPorId(int v)
        {
            try
            {
                String queryEspecialidad = "SELECT ID_ESPECIALIDAD,NOMBRE FROM BIOCENTRO_DB.dbo.ESPECIALIDAD WHERE ID_ESPECIALIDAD = " + v;
                DataSet dataBdEspecialidad = this.utilMethods.listarObject(queryEspecialidad, "ESPECIALIDAD");
                if (dataBdEspecialidad != null)
                {
                    return generarObjetEspecialidad(dataBdEspecialidad.Tables[0].Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se produjo un error en el metodo generarObjetEspecialidad ", ex);
            }
            return null;
        }

        private Especialidad generarObjetEspecialidad(DataRow row)
        {
            Especialidad especialidad = new Especialidad();
            especialidad.IdEspecialidad = Convert.ToInt32(row["ID_ESPECIALIDAD"]);
            especialidad.Nombre = Convert.ToString(row["NOMBRE"]);
            return especialidad;
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
