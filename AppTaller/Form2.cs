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
    public partial class Form2 : Form
    {
        conexion cn = new conexion();
        private Form1 m_frm;
        public Form2(Form1 frm)
        {
            InitializeComponent();
            m_frm = frm;
        }
        //public MySqlConnection cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");


        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listar();
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
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select*from clientes order by id desc", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            add();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_frm.button1.Text = "holllalal";
            m_frm.actualizarCliente();
            //this.Close();
            //DialogResult dialogo = MessageBox.Show("¿Desea cerrar el programa?",
            //   "Cerrar el programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogo == DialogResult.No)
            //{
            //    e.Cancel = true;
            //    //m_frm.BackColor = Color.AliceBlue;



            //}
            //else
            //{
            //    e.Cancel = false;
            //}
        }

    }
}
