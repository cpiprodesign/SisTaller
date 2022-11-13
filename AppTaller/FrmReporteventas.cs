using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace AppTaller
{
    public partial class FrmReporteventas : Form
    {
        conexion cn = new conexion();
        public FrmReporteventas()
        {
            InitializeComponent();
        }

        private void FrmReporteventas_Load(object sender, EventArgs e)
        {

        }
        private void ventasFechas()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("ConsultaVentasRangoFechas", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@fechainicial", MySqlDbType.DateTime).Value = Convert.ToDateTime(dateTimePicker1.Text);
            da.SelectCommand.Parameters.Add("@fechafinal", MySqlDbType.DateTime).Value = Convert.ToDateTime(dateTimePicker2.Text);
            da.Fill(dt);
            this.dataGridView1.DataSource = (dt);

            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 80;
            DataGridViewColumn fecha = dataGridView1.Columns[1];
            fecha.Width = 80;
            txtnumero.Text = Convert.ToString(dt.Rows.Count);

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            ventasFechas();
            sumartotal();
        }
        private void sumartotal()
        {

            try
            {
                decimal suma = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    suma += Convert.ToDecimal(row.Cells["total"].Value);

                    txttotalventas.Text = Convert.ToString(suma.ToString("C"));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        private void sumarproductos()
        {
            int suma = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                suma += Convert.ToInt32(row.Cells["cantidad"].Value);

                textBox2.Text = Convert.ToString(suma);

            }
        }
        private void sumarTotalPorventa()
        {
            decimal suma = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                suma += Convert.ToDecimal(row.Cells["subTotal"].Value);

                textBox4.Text = Convert.ToString(suma.ToString("C"));

            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //obtener dato seleccionado
            string id = "";
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                id = row.Cells["Id"].Value.ToString();
            }
            MySqlDataAdapter da = new MySqlDataAdapter("ConsultaDetalleVentasPorId", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@id", MySqlDbType.Int32).Value = (id);
            // da.SelectCommand.Parameters.Add("@fechafinal", SqlDbType.DateTime).Value = Convert.ToDateTime(dateTimePicker2.Text);
            da.Fill(dt);
            this.dataGridView2.DataSource = (dt);

            sumarproductos();
            sumarTotalPorventa();
            DataGridViewColumn column = dataGridView2.Columns[0];
            column.Width = 80;
            DataGridViewColumn fecha = dataGridView2.Columns[1];
            fecha.Width = 80;
            //txtnumero.Text = Convert.ToString(dt.Rows.Count);
            sumartotal();
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
                        MessageBox.Show("Datos exportados correctamente","Sistema");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
