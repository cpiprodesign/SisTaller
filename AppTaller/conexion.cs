using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace AppTaller
{
    
   public class conexion
    {
        public MySqlConnection cn = new MySqlConnection();
        //public string ip= "server=192.168.0.102;Database=tallercell;Uid=root;Pwd=;";
       // public string ip= "server=localhost;Database=tallercell;Uid=root;Pwd=;";

        public void cargaIp()
        {

        }
        public MySqlConnection ObtenerConeccion()
        {
          // cn = new MySqlConnection("server=162.214.204.218;Database=rwwpixbn_Taller-uruguay;Uid=rwwpixbn_uruguay;Pwd=tatiana199024AA;");
            //cn = new MySqlConnection("server=190.102.142.250;Database=rwwpixbn_Taller;Uid=rwwpixbn_root;Pwd=tatiana199024;");
            //cn = new MySqlConnection("server=192.162.0.103;Database=tallercell;Uid=root;Pwd=;");
           // cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");//client urug
            //cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");//client urug

            cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
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
