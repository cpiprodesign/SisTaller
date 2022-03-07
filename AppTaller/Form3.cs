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
        int p = 0;
        public Form3()
        {
            InitializeComponent();
        }
        //public MySqlConnection cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");


        private void Form3_Load(object sender, EventArgs e)
        {
            listar();
            desabilitar();
            btnguardar.Enabled = false;
            btncancelar.Enabled = false;
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
            habilitar();
            p = 1;
            btnguardar.Enabled = true;
            btnguardar.Text = "Guardar";
           btneditar.Enabled = false;
            btncancelar.Enabled = true;
            btneliminar.Enabled = false;
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
                String query = "insert into tecnicos(Nombres,Direccion,Telefono,Email,Documento,Sueldo,Estado)values('" + this.txtnombre.Text + "','" + this.txtdireccion.Text + "','" + this.txttelefono.Text + "','" + this.txtemail.Text + "','" + (this.txtdni.Text) + "','" + txtsueldo.Text + "','" + "Activo" + "')";
                MySqlCommand cm = new MySqlCommand(query, cn.ObtenerConeccion());
                cn.ObtenerConeccion();
                MySqlDataReader dr = cm.ExecuteReader();
                MessageBox.Show("Datos Guardados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cn.DescargarConexion();
                listar();
                limpiar();
                btneditar.Enabled = true;
                btnguardar.Enabled = false;
                btncancelar.Enabled = false;
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

            string update = "update tecnicos set Nombres='" + this.txtnombre.Text + "',Direccion='" + this.txtdireccion.Text + "',Telefono='" + this.txttelefono.Text + "',Email='" + this.txtemail.Text + "',Documento='" + this.txtdni.Text + "',sueldo='"+ txtsueldo.Text +"',estado='"+1+"' where idTecnicos='" + this.txtid.Text + "'";
            MySqlCommand cm = new MySqlCommand(update, cn.ObtenerConeccion());
            MySqlDataReader dr;
            try
            {
                dr = cm.ExecuteReader();
                MessageBox.Show("Datos Actualizados Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listar();
                btnguardar.Text = "Guardar";
                btnguardar.Enabled = false;
                btneditar.Enabled = true;
                btncancelar.Enabled = false;
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

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["idTecnicos"].Value.ToString();
                txtnombre.Text = row.Cells["Nombres"].Value.ToString();
                txtdireccion.Text = row.Cells["Direccion"].Value.ToString();
                txttelefono.Text = row.Cells["Telefono"].Value.ToString();
                txtemail.Text = row.Cells["Email"].Value.ToString();
                txtdni.Text = row.Cells["Documento"].Value.ToString();
                txtsueldo.Text = row.Cells["Sueldo"].Value.ToString();
                //txt.Text = row.Cells["Documento"].Value.ToString();

            }
        }
        private void desabilitar()
        {
            txtid.Enabled = false;
            txtnombre.Enabled = false;
            txtdireccion.Enabled = false;
            txttelefono.Enabled = false;
            txtemail.Enabled = false;
            txtdni.Enabled = false;
            txtsueldo.Enabled = false;
        }
        private void habilitar()
        {
            txtid.Enabled = true;
            txtnombre.Enabled = true;
            txtdireccion.Enabled = true;
            txttelefono.Enabled = true;
            txtemail.Enabled = true;
            txtdni.Enabled = true;
            txtsueldo.Enabled = true;
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Estas Seguro que quieres eleminar el Registro " + this.txtnombre.Text, "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string b = "delete from tecnicos where idTecnicos='" + this.txtid.Text + "'";
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

        private void btneditar_Click(object sender, EventArgs e)
        {
            p = 2;
            habilitar();
            btnguardar.Text = "Actualizar";
            btnguardar.Enabled = true;
            btneditar.Enabled = false;
            btncancelar.Enabled = false;
            btneliminar.Enabled = false;
            
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            desabilitar();
            btnguardar.Enabled = false;
            btneditar.Enabled = true;
            btncancelar.Enabled = false;
            btneliminar.Enabled = true;
        }
    }
}
