using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTaller
{
    public partial class Form3 : Form
    {
        conexion cn = new conexion();
        public Form3()
        {
            InitializeComponent();
        }
        //public MySqlConnection cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");


        private void Form3_Load(object sender, EventArgs e)
        {
            listar();
        }
        private void limpiar()
        {
            txtid.Text = "";
            txtnombre.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtemail.Text = "";
            txtdni.Text = "";
            txtsueldo.Text = "";
            txtnombre.Focus();
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select*from tecnicos order by idTecnicos desc", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
        private void add()
        {

            try
            {
                String query = "insert into tecnicos(Nombres,Direccion,Telefono,Email,Documento,Sueldo,Estado)values('" + this.txtnombre.Text + "','" + this.txtdireccion.Text + "','" + this.txttelefono.Text + "','" + this.txtemail.Text + "','" + (this.txtdni.Text) + "','"+txtsueldo.Text+"','"+"Activo"+"')";
                MySqlCommand cm = new MySqlCommand(query, cn.ObtenerConeccion());
                cn.ObtenerConeccion();
                MySqlDataReader dr = cm.ExecuteReader();
                MessageBox.Show("Datos Guardados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.DescargarConexion();
                listar();
                limpiar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                cn.DescargarConexion();
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            add();
            listar();
        }
    }
}
