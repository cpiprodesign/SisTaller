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
    public partial class Frmgrafico : Form
    {
        conexion cn =new conexion();
        public Frmgrafico()
        {
            InitializeComponent();
        }

        private void Frmgrafico_Load(object sender, EventArgs e)
        {
            grafico();
            listarOrdenes();
        }
        private void listarOrdenes()
        {
            MySqlCommand cmd = new MySqlCommand("ObtenerUltimasOrdenes", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@nombres", MySqlDbType.VarChar).Value = txtnombre.Text;


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
            label3.Text = dt.Rows.Count.ToString();
        }
        private void listarOrdenesEstado()
        {
            MySqlCommand cmd = new MySqlCommand("sp_ObtenerOrdenesPorEstado", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@estadoBuscado", MySqlDbType.VarChar).Value = comboBox1.Text;


            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
            label3.Text=dt.Rows.Count.ToString();
        }
        private void grafico()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("ordenesPorMeses", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Clear();
            da.Fill(dt);
            //this.Grafico.Palette = ChartColorPalette.Chocolate;

            string mes;
            int total;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    dr = dt.Rows[i];

                    mes = dr.ItemArray[0].ToString();
                    total = Convert.ToInt32(dr.ItemArray[1]);

                    this.chart2.Series["ChartLinea"].Points.AddXY(mes, total);


                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listarOrdenesEstado();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listarOrdenesEstado();
        }
    }
}
