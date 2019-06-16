using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CapaNegocio
{
    public class TokenSecurity : System.Web.Services.Protocols.SoapHeader
    {
        private string stToken;
        private string autenticacionToken;
        //private string usuario;
        public string StToken { get => stToken; set => stToken = value; }
        public string AutenticacionToken { get => autenticacionToken; set => autenticacionToken = value; }
        //public string Usuario { get => usuario; set => usuario = value; }

        public bool credencialesValidas(string StToken)
        {
            try
            {
                //NegocioService negocio = new NegocioService();
                //negocio.login()
                if (stToken == DateTime.Now.ToString("ddMMyyyy")) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool credencialesValidas(TokenSecurity securityToken)
        {
            try
            {
                if (securityToken == null) return false;
                if (!string.IsNullOrEmpty(securityToken.AutenticacionToken))
                {
                    return (HttpRuntime.Cache[securityToken.AutenticacionToken] != null);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
