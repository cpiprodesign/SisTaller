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
    public partial class Form4 : Form
    {
        conexion cn = new conexion();
        public Form4()
        {
            InitializeComponent();
        }
        //public MySqlConnection cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");


        private void Form4_Load(object sender, EventArgs e)
        {

        }
        private void filtro()
        {
            MySqlCommand cmd = new MySqlCommand("Buscarfechas", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@fechaInicial", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpinicial.Text);
            cmd.Parameters.Add("@fechaFinal", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpfinal.Text);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }
        private void filtrarNombresCliente()
        {
            MySqlCommand cmd = new MySqlCommand("BuscarOrdenClienteNombre", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@nombres", MySqlDbType.VarChar).Value = txtnombre.Text;
           

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            filtro();
            sumarTotalServicio();
            sumarTotalAdelantado();
            cuentacobrar();
        }
        private void sumarTotalServicio()
        {
            decimal suma = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                suma += Convert.ToDecimal(row.Cells["TotalPagar"].Value);

                //txttotal.Text = Convert.ToString(suma.ToString("C"));
                txttotal.Text = Convert.ToString(suma);

            }
        }
        private void sumarTotalAdelantado()
        {
            decimal suma = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                suma += Convert.ToDecimal(row.Cells["PagoAdelantado"].Value);

                txtadelantado.Text = Convert.ToString(suma);

            }
        }
        private void cuentacobrar()
        {
            txtcobrar.Text =Convert.ToString(Convert.ToDecimal(txttotal.Text) - (Convert.ToDecimal(txtadelantado.Text)));
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Datos exportados correctamente","Sistema" ,MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filtrarNombresCliente();
        }

        private void txtnombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtrarNombresCliente();
            }
        }
    }
}
