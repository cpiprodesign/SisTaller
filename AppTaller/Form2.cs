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
        int p = 0;
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
           habilitar();           
            p = 1;
            btnguardar.Enabled = true;
            btnguardar.Text = "Guardar";
            bunifuFlatButton1.Enabled = false;
            bunifuFlatButton2.Enabled = true;
            btneliminar.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listar();
            desabilitar();
            btnguardar.Enabled = false;
            bunifuFlatButton2.Enabled = false;
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
                bunifuFlatButton1.Enabled = true;
                btnguardar.Enabled = false;
                bunifuFlatButton2.Enabled = false;
                btneliminar.Enabled = true;


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
        private void updates()
        {

            string update = "update clientes set Nombres='" + this.txtnombre.Text + "',Direccion='" + this.txtdireccion.Text + "',Telefono='" + this.txttelefono.Text + "',Email='" + this.txtemail.Text + "',Dni='" + this.txtdni.Text + "' where id='" + this.txtid.Text + "'";
            MySqlCommand cm = new MySqlCommand(update, cn.ObtenerConeccion());
            MySqlDataReader dr;
            try
            {
                dr = cm.ExecuteReader();
                MessageBox.Show("Datos Actualizados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
                btnguardar.Text = "Guardar";
                btnguardar.Enabled = false;
                bunifuFlatButton1.Enabled = true;
                bunifuFlatButton2.Enabled=false;
                btneliminar.Enabled = true;
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

        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select*from clientes order by id desc", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (p == 1)
            {
                add();
            }
            else
            {
                updates();
            }
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["id"].Value.ToString();
                txtnombre.Text = row.Cells["Nombres"].Value.ToString();
                txtdireccion.Text = row.Cells["Direccion"].Value.ToString();
                txttelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtemail.Text = row.Cells["Email"].Value.ToString();
                txtdni.Text = row.Cells["Dni"].Value.ToString();

            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["id"].Value.ToString();
                txtnombre.Text = row.Cells["Nombres"].Value.ToString();
                txtdireccion.Text = row.Cells["Direccion"].Value.ToString();
                txttelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtemail.Text = row.Cells["Email"].Value.ToString();
                txtdni.Text = row.Cells["Dni"].Value.ToString();

            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            p = 2;
            habilitar();
            btnguardar.Text = "Actualizar";
            btnguardar.Enabled = true;
            bunifuFlatButton2.Enabled = true;
            bunifuFlatButton1.Enabled = false;
            btneliminar.Enabled = false;
        }
        private void desabilitar()
        {
            txtid.Enabled = false;
            txtnombre.Enabled = false;
            txtdireccion.Enabled = false;
            txttelefono.Enabled = false;
            txtemail.Enabled = false;
            txtdni.Enabled = false;
            
        }
        private void habilitar()
        {
            txtid.Enabled = true;
            txtnombre.Enabled = true;
            txtdireccion.Enabled = true;
            txttelefono.Enabled = true;
            txtemail.Enabled = true;
            txtdni.Enabled = true;
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            limpiar();
            desabilitar();
            btnguardar.Enabled = false;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            btneliminar.Enabled = true;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Estas Seguro que quieres eleminar el Registro " + this.txtnombre.Text, "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string b = "delete from clientes where id='" + this.txtid.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(b, cn.ObtenerConeccion());
                    cmd.ExecuteNonQuery();
                    listar();
                    MessageBox.Show("Datos Eleminado Correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
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
        }
    }
}
