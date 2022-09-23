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
    public partial class Form5 : Form
    {
        conexion cn = new conexion();
        int p = 0;
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listar();
            
            desabilitar();
            btnguardar.Enabled = false;
            bunifuFlatButton2.Enabled = false;
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("ListarArticulos", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
            this.dataGridView2.DataSource = dt;
        }
        private void filtro()
        {
            MySqlCommand cmd = new MySqlCommand("BuscarArticuloNombre", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = Convert.ToString(textBox1.Text);
            //cmd.Parameters.Add("@fechaFinal", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpfinal.Text);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView2.DataSource = dt;
        }
        private void limpiar()
        {
            txtid.Text = "";
            txtcodigobarras.Text = "";
            txtdescripcion.Text = "";
            txtcosto.Text = "";
            txtventa.Text = "";
            txtpreciomayor.Text = "";
            txtstock.Text = "";
            txtcodigobarras.Focus();
        }
        private void desabilitar()
        {
            txtid.Enabled = false;
            txtcodigobarras.Enabled = false;
            txtdescripcion.Enabled = false;
            txtcosto.Enabled = false;
            txtventa.Enabled = false;
            txtpreciomayor.Enabled = false;
            txtstock.Enabled = false;
            txtstockminimo.Enabled = false;
            cbocategoria.Enabled = false;
            cbomarca.Enabled = false;
        }
        private void habilitar()
        {
            txtid.Enabled = true;
            txtcodigobarras.Enabled = true;
            txtdescripcion.Enabled = true;
            txtcosto.Enabled = true;
            txtventa.Enabled = true;
            txtpreciomayor.Enabled = true;
            txtstock.Enabled = true;
            txtstockminimo.Enabled = true;
            cbocategoria.Enabled = true;
            cbomarca.Enabled = true;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtro();
        }

        private void txtvalor_OnTextChange(object sender, EventArgs e)
        {
            
        }

        private void txtvalor_KeyDown(object sender, EventArgs e)
        {
            filtro();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            filtro();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource != null)
                {
                    SaveFileDialog fichero = new SaveFileDialog();
                    fichero.Filter = "Excel (*.xls)|*.xls";
                    if (fichero.ShowDialog() == DialogResult.OK)
                    {
                        Microsoft.Office.Interop.Excel.Application aplicacion;
                        Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                        Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                        aplicacion = new Microsoft.Office.Interop.Excel.Application();
                        libros_trabajo = aplicacion.Workbooks.Add();
                        hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                        //exportar cabeceras dgvLog
                        for (int i = 1; i <= this.dataGridView1.Columns.Count; i++)
                        {
                            hoja_trabajo.Cells[1, i] = this.dataGridView1.Columns[i - 1].HeaderText;
                        }

                        //Recorremos el DataGridView rellenando la hoja de trabajo con los datos
                        for (int i = 0; i < this.dataGridView1.Rows.Count - 1; i++)
                        {
                            for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                            {
                                hoja_trabajo.Cells[i + 2, j + 1] = this.dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        libros_trabajo.SaveAs(fichero.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                        libros_trabajo.Close(true);
                        aplicacion.Quit();
                        MessageBox.Show("Datos exportados correctamente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            limpiar();
            desabilitar();
            btnguardar.Enabled = false;
            bunifuFlatButton1.Enabled = true;
            bunifuFlatButton2.Enabled = false;
            btneliminar.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["id"].Value.ToString();
                txtcodigobarras.Text = row.Cells["CodigoBarra"].Value.ToString();
                txtdescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtcosto.Text = row.Cells["PrecioCosto"].Value.ToString();
                txtventa.Text = row.Cells["PrecioVenta"].Value.ToString();
                txtpreciomayor.Text = row.Cells["PrecioMayor"].Value.ToString();
                txtstock.Text = row.Cells["Stock"].Value.ToString();
                txtstockminimo.Text = row.Cells["StockMinimo"].Value.ToString();
                cbocategoria.Text = row.Cells["Categoria"].Value.ToString();
                cbomarca.Text = row.Cells["Marca"].Value.ToString();

            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                txtid.Text = row.Cells["id"].Value.ToString();
                txtcodigobarras.Text = row.Cells["CodigoBarra"].Value.ToString();
                txtdescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtcosto.Text = row.Cells["PrecioCosto"].Value.ToString();
                txtventa.Text = row.Cells["PrecioVenta"].Value.ToString();
                txtpreciomayor.Text = row.Cells["PrecioMayor"].Value.ToString();
                txtstock.Text = row.Cells["Stock"].Value.ToString();
                txtstockminimo.Text = row.Cells["StockMinimo"].Value.ToString();
                cbocategoria.Text = row.Cells["Categoria"].Value.ToString();
                cbomarca.Text = row.Cells["Marca"].Value.ToString();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filtro();
        }
    }
       


    }

