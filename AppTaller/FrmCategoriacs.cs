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
    public partial class FrmCategoriacs : Form
    {
        conexion cn = new conexion();
        int p = 0;
        private Form5 m_frm;
        public FrmCategoriacs(Form5 frm)
        {
            InitializeComponent();
            m_frm = frm;
        }

        private void FrmCategoriacs_Load(object sender, EventArgs e)
        {
            listar();
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select*from categoria order by idCategoria desc", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
        private void limpiar()
        {
            txtid.Text = "";

            txtnombre.Text = "";
            txtnombre.Focus();
            btnguardar.Enabled = true;


        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void add()
        {

            try
            {
                String query = "insert into categoria(Nombres)values('" + this.txtnombre.Text + "')";
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

        private void FrmCategoriacs_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            m_frm.actualizarCategoria();
        }
    }
}
