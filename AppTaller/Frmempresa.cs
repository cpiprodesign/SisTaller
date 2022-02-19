using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace AppTaller
{
    public partial class Frmempresa : Form
    {
        conexion cn = new conexion();
        public Frmempresa()
        {
            InitializeComponent();
        }

        private void Frmempresa_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            // validar();

            MySqlCommand cm = new MySqlCommand("select *from empresa", cn.ObtenerConeccion());
            cm.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            MySqlDataReader dr;
            dr = cm.ExecuteReader();
            while (dr.Read() == true)
            {
                txtid.Text = dr["id"].ToString(); ;
                txtnumero.Text = dr["Numero"].ToString();
                txtnomcomercial.Text = dr["NombreComercial"].ToString();
               // txtnombre.Text = dr["Nombre"].ToString();
                txtlogo.Text = dr["logo"].ToString();
                txttelefono.Text = dr["telefono"].ToString();
                txtemail.Text = dr["email"].ToString();               
                txtdireccion.Text = dr["Direccion"].ToString();
                //if (txtlogo.Text == "")
                //{
                //    pictureBox1.Image = null;
                //}
                //else
                //{
                //    //pictureBox1.Image = Image.FromFile(txtlogo.Text);
                //}


            }
            // validar();
        }
        private void update()
        {
            string update = "update empresa set numero='" + this.txtnumero.Text + "',NombreComercial='" + txtnomcomercial.Text + "',logo='" + txtlogo.Text + "',telefono='" + txttelefono.Text + "',email='" + txtemail.Text + "',direccion='" + txtdireccion.Text + "',estado='" + 1 + "' where id='" + this.txtid.Text + "'";
            MySqlCommand cm = new MySqlCommand(update, cn.ObtenerConeccion());
            MySqlDataReader dr;
            try
            {
                dr = cm.ExecuteReader();
                MessageBox.Show("Datos Actualizados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargar();
                //habilitar botones
                //desabilitar();



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.DescargarConexion();
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            update();
        }
    }

}
