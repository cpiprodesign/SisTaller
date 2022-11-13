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
    public partial class FrmAddClienteVenta : Form
    {
        conexion cn = new conexion();
        int p = 0;
        private Form6 m_frm;
        public FrmAddClienteVenta(Form6 frm)
        {

            InitializeComponent();
            m_frm = frm;
        }

        private void FrmAddClienteVenta_Load(object sender, EventArgs e)
        {
            listar();
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select*from clientes order by id desc", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
        private void limpiar()
        {
            txtnombre.Text = "";
            txtdireccion.Text = "";
            txttelefono.Text = "";
            txtemail.Text = "";
            txtdni.Text = "";
            txtnombre.Focus();


        }
        private void add()
        {

            try
            {
                String query = "insert into clientes(Nombres,Direccion,Telefono,Email,Dni)values('" + this.txtnombre.Text + "','" + this.txtdireccion.Text + "','" + this.txttelefono.Text + "','" + this.txtemail.Text + "','" + (this.txtdni.Text) + "')";
                MySqlCommand cm = new MySqlCommand(query, cn.ObtenerConeccion());
                cn.ObtenerConeccion();
                MySqlDataReader dr = cm.ExecuteReader();
                MessageBox.Show("Datos Guardados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.DescargarConexion();
                listar();
                limpiar();
                //bunifuFlatButton1.Enabled = true;
                btnguardar.Enabled = false;
                //bunifuFlatButton2.Enabled = false;
                //btneliminar.Enabled = true;


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
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void FrmAddClienteVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_frm.button1.Text = "holllalal";
            m_frm.actualizarCliente();
            
        }
    }
}
