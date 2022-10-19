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
        string descripcion,codigo;
        decimal precio, importe;
        int cantidad, stock;
        //
        decimal montoTotal;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            listar();
            txtcodigo.Text = "";
            txtcodigo.Focus();
            GetTipoPago();
            GetDocumento();
            Getcliente();
        }
        private void listar()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("select descripcion,PrecioVenta,stock,id from articulos where articulos.Stock >= 1", cn.ObtenerConeccion());
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
            
        }

        private void GetTipoPago()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from tipodepago ", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbotipopago.DataSource = dt;
                cbotipopago.DisplayMember = "Nombre";
                cbotipopago.ValueMember = "id";
                label10.DataBindings.Add(new Binding("Text", dt, "id"));
               
                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombre"])); // columna de la consulta sql
                }
                cbotipopago.AutoCompleteCustomSource = coleccion;
                cbotipopago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbotipopago.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
        private void GetDocumento()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from tipodedocumento ", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbodocumento.DataSource = dt;
                cbodocumento.DisplayMember = "Nombre";
                cbodocumento.ValueMember = "id";
                label11.DataBindings.Add(new Binding("Text", dt, "id"));
                //txtemail.DataBindings.Add(new Binding("Text", dt, "Email"));
                //txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                //txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));


                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombre"])); // columna de la consulta sql
                }
                cbodocumento.AutoCompleteCustomSource = coleccion;
                cbodocumento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbodocumento.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
        private void Getcliente()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from clientes ", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboclientes.DataSource = dt;
                cboclientes.DisplayMember = "Nombres";
                cboclientes.ValueMember = "id";
                label12.DataBindings.Add(new Binding("Text", dt, "id"));
                txtdni.DataBindings.Add(new Binding("Text", dt, "dni"));
                //txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                //txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));


                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombres"])); // columna de la consulta sql
                }
                cboclientes.AutoCompleteCustomSource = coleccion;
                cboclientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboclientes.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Form2 frm = new Form2(this);
            //frm.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void filtro()
        {
            if (txtcodigo.Text == "")
            {
                listar();
            }
            else
            {
                MySqlDataAdapter da = new MySqlDataAdapter("BuscarArticuloCodigo", cn.ObtenerConeccion());
                DataTable dt = new DataTable();
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@CodigoBarra", MySqlDbType.VarChar).Value = txtcodigo.Text;
                //da.SelectCommand.Parameters.Add("@codigo", SqlDbType.VarChar).Value = textBox1.Text;
                dt.Clear();
                da.Fill(dt);
                this.dataGridView1.DataSource = (dt);
                //txtcodigo.Text = "";
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            filtro();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listar();
            }
            //else {
            //    filtro();
            //}


        }

        private void txtcodigo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{

            //    filtro();


            //}
           // filtro();
        }

        private void txtcodigo_TextChanged(object sender, EventArgs e)
        {
            //filtro();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DialogResult dialogResult = MessageBox.Show("Deseas Agregar el siguiente Articulo ? ", "Sistema", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                    codigo = row.Cells["id"].Value.ToString();
                    descripcion = row.Cells["Descripcion"].Value.ToString();
                    // cantidad = row.Cells["Direccion"].Value.ToString();
                    precio = Convert.ToDecimal(row.Cells["PrecioVenta"].Value.ToString());
                    stock = Convert.ToInt32(row.Cells["Stock"].Value.ToString());
                    addArticulo();
                }
                else
                {
                    MessageBox.Show("Articulo no agregado","Sistema",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                 
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dialogResult = MessageBox.Show("Deseas Eleminar el Producto ", "Sistema", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (listView1.SelectedItems.Count > 0)
                    {
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                        //actualiza la sumas de venta

                        actualizarDetalle();
                    }
                    else
                    {
                        MessageBox.Show("Seleccione una Fila");

                    }

                    ////do something
                    //DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index);
                    //// sumadata();
                    //Sumadata();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
           // filtro();
        }

        private void txtcodigo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                filtro();
                //addArticulo();


            }
        }

        private void cbodocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcularIgv();
        }

        private void addArticulo()
        {
            ListViewItem lista = new ListViewItem(codigo);
            lista.SubItems.Add(descripcion);
            lista.SubItems.Add(Convert.ToString(precio));
            lista.SubItems.Add("1");
            lista.SubItems.Add(Convert.ToString((Decimal.Parse("1") * Convert.ToDecimal(precio))));
            
            listView1.Items.Add(lista);
            actualizarDetalle();
        }
        private void actualizarDetalle()
        {
            Decimal dblSuma = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                dblSuma += Convert.ToDecimal(item.SubItems[4].Text);

            }
            montoTotal = Convert.ToDecimal(dblSuma);
            lbtotal.Text = Convert.ToString(montoTotal);
            //lblsubtotal.Text = Convert.ToString(dblSuma);
            calcularIgv();
            


        }
        private void calcularIgv()
        {
            if (cbodocumento.SelectedIndex == 0)
            {
                //montoTotal = Convert.ToDecimal(dblSuma);
                //lblsubtotal.Text = Convert.ToString(montoTotal);

                lbtotal.Text = Convert.ToString(montoTotal);
                txtigv.Text = "0.00";
                txtsubtotal.Text = lbtotal.Text;
                //generarComprobanteBoleta();

            }
            else if (cbodocumento.SelectedIndex == 1)
            {
                //igv sumando

                //decimal igvs, totald;
                //igvs = (Convert.ToDecimal(lblsubtotal.Text) * (Convert.ToDecimal(iva)));
                //lbligv.Text = Convert.ToString(Math.Round(igvs, 2));
                //totald = Convert.ToDecimal(lblsubtotal.Text) + Convert.ToDecimal(lbligv.Text);

                //lbltotal.Text = Convert.ToString(Math.Round(totald, 2));
                decimal igv, montobase;

                montobase = (montoTotal / Convert.ToDecimal(1 + (0.18)));
                igv = (montoTotal - montobase);
                txtigv.Text = Convert.ToString(Math.Round(igv, 2));
                lbtotal.Text = Convert.ToString(Math.Round(montoTotal, 2));

                txtsubtotal.Text = Convert.ToString(Math.Round(montobase, 2));


            }
        }
    }
}
