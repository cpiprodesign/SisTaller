using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace AppTaller
{
    
   public class conexion
    {
        public MySqlConnection cn = new MySqlConnection();
        public MySqlConnection ObtenerConeccion()
        {
           cn = new MySqlConnection("server=162.144.57.183;Database=rwwpixbn_Taller;Uid=rwwpixbn_develop;Pwd=tatiana199024;");
            //cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");
            try
            {
                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DescargarConexion()
        {
            
            cn.Dispose();
            return true;
        }
    }
}
