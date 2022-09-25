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
    public partial class Form6 : Form
    {
        conexion cn = new conexion();
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            listar();
            txtcodigo.Text = "";
            txtcodigo.Focus();
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select descripcion,PrecioVenta,stock from articulos", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
            
        }
    }
}
