using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AppTaller
{
    public partial class Form6 : Form
    {
        conexion cn = new conexion();
        convertidor conv = new convertidor();
        string descripcion,codigo;
        decimal precio, importe;
        int cantidad, stock;
        //
        decimal montoTotal;
        //comprobante
        int numero;
        //convertir
        public string numeros, letras;
        //datos de la empresa
        string nombreEmpresa, direccion, email, Numero;
        string telefono;
        string letraInicialComprobante;
        string nombrecomprobante;
        //imprimir
        private int numberOfItemsPerPage = 0;
        private int numberOfItemsPrintedSoFar = 0;
        public string idproducto,descripcionArticulo;
        public string vencimientoI, loteI;

        public decimal cantidadArticulo, precioventaArticulo, importeArticulo;
        string nombrecomprobanteImpresion;
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
           
            //limpiar();
            listar();
            txtcodigo.Text = "";
            txtcodigo.Focus();
            GetTipoPago();
            GetDocumento();
            Getcliente();
            getEmpresa();
            txtcodigo.Focus();
            limpiar();
            cn.DescargarConexion();
        }
        private void limpiar()
        {
            txtsubtotal.Text = "";
            txtigv.Text = "";
            lbtotal.Text = "";
            cbotipopago.Text = "";
            cbodocumento.Text = "";
            cboclientes.Text = "";
            txtdni.Text = "";
            listView1.Items.Clear();
            txtcodigo.Text = "";
            txtcodigo.Focus();
            panel1.BackgroundImage = null;


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
                MySqlCommand cmd = new MySqlCommand("select*from clientes order by id desc ", cn.ObtenerConeccion());
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
           FrmAddClienteVenta f = new FrmAddClienteVenta(this);
            f.ShowDialog();
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
           
            //generarNumero();
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

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
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

        private void button4_Click_2(object sender, EventArgs e)
        {
            if (lbtotal.Text == "" || lbtotal.Text == "0.00" || lbtotal.Text == "0")
            {
                MessageBox.Show("Debes generar una venta antes de imprimir", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Deseas Imprimir el Comprobante ?", "Sistema", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    leer();
                    generarQr();
                    //printDocument.Print();
                    PrintDocument pd = new PrintDocument();
                    //a4
                    // PaperSize ps = new PaperSize("factura", 827, 1169);
                    PaperSize ps = new PaperSize("Boleta", 300, 900);
                    // PaperSize ps = new PaperSize("Boleta", 200, 600);
                    pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

                    pd.PrintController = new StandardPrintController();
                    pd.DefaultPageSettings.Margins.Left = 0;
                    pd.DefaultPageSettings.Margins.Right = 0;
                    pd.DefaultPageSettings.Margins.Top = 0;
                    pd.DefaultPageSettings.Margins.Bottom = 0;

                    pd.DefaultPageSettings.PaperSize = ps;
                    pd.Print();
                    
                }
                else
                {
                    MessageBox.Show("No se imprimio el comprobante","Sistema");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //guardarVenta();
            if (cbotipopago.Text == "")
            {
                MessageBox.Show("Debes seleccionar el tipo de pago", "Sistema");
                cbotipopago.Focus();
            }
            else if (cbodocumento.Text == "")
            {
                MessageBox.Show("Debes seleccionar el documento", "Sistema");
                cbodocumento.Focus();
            }
            else if (cboclientes.Text == "")
            {
                MessageBox.Show("Debes seleccionar el cliente", "Sistema");
                cboclientes.Focus();
            }
            else if (listView1.Items.Count == 0)
            {
                MessageBox.Show("No has ingresado los Articulos","sistema");
                txtcodigo.Focus();
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Deseas Imprimir el Comprobante ?", "Sistema", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                    button4_Click_2(sender, e);
                    
                }
                //guarda la venta
                insertarVentaTransaccion();
                listar();
                limpiar();
            }


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

        private void txtcodigo_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addArticulo();
            }
        }

        private void generarNumero()
        {
            MySqlCommand cmd = new MySqlCommand("GenerarNumeroVenta", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@comprobante", MySqlDbType.Int64).Value =Convert.ToInt32(label11.Text);

            cn.ObtenerConeccion();
            MySqlDataReader dr = cmd.ExecuteReader();
            cn.DescargarConexion();

            if (dr.HasRows)
            {
                while (dr.Read())
                {


                    numero = Convert.ToInt32(dr.GetString(0).ToString());
                    //cboclientes.Focus();
                }
            }
            dr.Close();
            cn.DescargarConexion();
        }
        private void guardarVenta()
        {

            try
            {
                //genera numnero de comprobante
                generarNumero();
                MySqlCommand cmd = new MySqlCommand("InsertarVenta", cn.ObtenerConeccion());
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@idOrden", MySqlDbType.Int64).Value = textBox1.Text;
                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = Convert.ToDateTime("2022-10-23 11:25:55");
                cmd.Parameters.Add("@idCliente", MySqlDbType.Int32).Value = label12.Text;
                cmd.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = 1;
                cmd.Parameters.Add("@tipodePagos", MySqlDbType.Int32).Value = label10.Text;
                cmd.Parameters.Add("@tipoDeComprobante", MySqlDbType.Int32).Value = label11.Text;
                cmd.Parameters.Add("@numero", MySqlDbType.Int32).Value = numero;
                cmd.Parameters.Add("@impuesto", MySqlDbType.Decimal).Value = txtigv.Text;
                cmd.Parameters.Add("@subTotal", MySqlDbType.Decimal).Value = txtsubtotal.Text;
                cmd.Parameters.Add("@igv", MySqlDbType.Decimal).Value = txtigv.Text;
                cmd.Parameters.Add("@total", MySqlDbType.Decimal).Value = lbtotal.Text;
                cmd.Parameters.Add("@estado", MySqlDbType.VarChar).Value = "Registrado";

                cn.ObtenerConeccion();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Venta Registrado Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //limpiar();
                //desabilitar();
                cn.DescargarConexion();
                //textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void insertarVentaTransaccion()
        {
            {
                try
                {
                    generarNumero();
                    int nuevoId;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        
                        using (MySqlConnection cn1 = cn.ObtenerConeccion())
                        {
                             

                            using (MySqlCommand cmd = cn1.CreateCommand())
                            {
                                //venta
                                //genera numnero de comprobante
                                
                                // MySqlCommand cmd = new MySqlCommand("InsertarVenta", cn.ObtenerConeccion());
                                cmd.CommandText = "InsertarVenta";
                                cmd.CommandType = CommandType.StoredProcedure;
                                //cmd.Parameters.Add("@idOrden", MySqlDbType.Int64).Value = textBox1.Text;
                                cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString("G"));
                                //cmd.Parameters.Add("@fecha", MySqlDbType.DateTime).Value = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));

                                cmd.Parameters.Add("@idCliente", MySqlDbType.Int32).Value = label12.Text;
                                cmd.Parameters.Add("@idUsuario", MySqlDbType.Int32).Value = "1";
                                cmd.Parameters.Add("@tipodePagos", MySqlDbType.Int32).Value = label10.Text;
                                cmd.Parameters.Add("@tipoDeComprobante", MySqlDbType.Int32).Value = label11.Text;
                                cmd.Parameters.Add("@numero", MySqlDbType.Int32).Value = numero;
                                cmd.Parameters.Add("@impuesto", MySqlDbType.Decimal).Value = txtigv.Text;
                                cmd.Parameters.Add("@subTotal", MySqlDbType.Decimal).Value = txtsubtotal.Text;
                                cmd.Parameters.Add("@igv", MySqlDbType.Decimal).Value = txtigv.Text;
                                cmd.Parameters.Add("@total", MySqlDbType.Decimal).Value = lbtotal.Text;
                                cmd.Parameters.Add("@estado", MySqlDbType.VarChar).Value = "Registrado";

                              // cn1.Open();
                                //cmd.ExecuteNonQuery();
                                nuevoId = int.Parse(cmd.ExecuteScalar().ToString());
                                //cn1.Close();
                                //nuevoId = Convert.ToInt32(cmd.Parameters.Add("@NuevoId").Value);
                                //nuevoId = Convert.ToInt32(cmd.Parameters["@idNuevo"].Value);
                                //cn1.Close();

                                //detalle

                                String idproducto;
                                decimal cantidad, precioventa, importe;
                                cmd.CommandText = "InsertarDetalleVenta";
                                cmd.CommandType = CommandType.StoredProcedure;


                                foreach (ListViewItem item in listView1.Items)
                                {
                                    idproducto = Convert.ToString(item.SubItems[0].Text);
                                    cantidad = Convert.ToDecimal(item.SubItems[3].Text);
                                    precioventa = Convert.ToDecimal(item.SubItems[2].Text);
                                    importe = Convert.ToDecimal(item.SubItems[4].Text);

                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@idVenta", SqlDbType.Int).Value = nuevoId;
                                    cmd.Parameters.AddWithValue("@idArticulo", idproducto);
                                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                                    cmd.Parameters.AddWithValue("@precioVenta", precioventa);
                                    cmd.Parameters.AddWithValue("@igv", SqlDbType.Decimal).Value = Convert.ToDecimal(txtigv.Text);
                                    cmd.Parameters.AddWithValue("@descuento", SqlDbType.Decimal).Value = Convert.ToDecimal("0.00");
                                    cmd.Parameters.AddWithValue("@subTotal", SqlDbType.Decimal).Value = Convert.ToDecimal(importe);
                                    cmd.Parameters.AddWithValue("@Total", SqlDbType.Decimal).Value = Convert.ToDecimal(lbtotal.Text);
                                    
                                    cmd.ExecuteNonQuery();

                                   //cn1.Close();
                                    //MessageBox.Show("Venta registrado correctamente");
                                }
                                MessageBox.Show("Venta Registrado Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        scope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Error al guardar la venta", ex);
                }
            }
        }
        private void getEmpresa()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("select*from empresa  ", cn.ObtenerConeccion());
                cmd.CommandType = CommandType.Text;

                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read() == true)
                {
                    direccion = dr["direccion"].ToString();
                    nombreEmpresa = dr["NombreComercial"].ToString();
                    Numero = Convert.ToString(dr["Numero"].ToString());

                    telefono = (dr["telefono"].ToString());
                    email = dr["email"].ToString();
                }




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)//TICKETERA 8.00
        {
            // Image image = Resources.ecuador.jpeg;
            Image image = panel1.BackgroundImage;

            // e.Graphics.DrawImage(image, 25, 25, image.Width, image.Height);
            e.Graphics.DrawString(nombreEmpresa, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(50, 35));
            e.Graphics.DrawString("DIRECCIÓN: " + direccion, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 60));
            e.Graphics.DrawString("RUC: " + numero, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 90));
            e.Graphics.DrawString("TELEFONO: " + telefono, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 120));

            //e.Graphics.DrawString("________________________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(15, 120));
            //factura o boleta

            if (cbodocumento.SelectedIndex == 0)
            {
                nombrecomprobante = "BOLETA DE VENTA";
                letraInicialComprobante = "B";
            }
            else if (cbodocumento.SelectedIndex == 1)
            {
                nombrecomprobante = "FACTURA DE VENTA";
                letraInicialComprobante = "F";
            }
            else if (cbodocumento.SelectedIndex == 2)
            {
                nombrecomprobante = "TICKET DE VENTA";
                letraInicialComprobante = "T";
            }

            e.Graphics.DrawString(nombrecomprobante, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(70, 140));
            e.Graphics.DrawString(letraInicialComprobante + "-" + (numero), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(80, 160));

            e.Graphics.DrawString(Convert.ToString(numero), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(150, 160));

            e.Graphics.DrawString("___________________________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 170));



            e.Graphics.DrawString("Fecha de Emisión: " + DateTime.Now.ToShortDateString(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 190));
            e.Graphics.DrawString("Vendedor: " + "Administrador", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 210));

            e.Graphics.DrawString("Nombre Cliente: " + cboclientes.Text.Trim(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 230));
            e.Graphics.DrawString(" D.N.I o R.U.C: " + txtdni.Text.Trim(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 250));

            e.Graphics.DrawString("___________________________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 270));
            e.Graphics.DrawString("Descripción", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(30, 290));
            e.Graphics.DrawString("Cant ", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(170, 290));
            e.Graphics.DrawString("P.Unit ", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(200, 290));
            e.Graphics.DrawString("Total", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(240, 290));
            e.Graphics.DrawString("___________________________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, 300));

            int yPos = 330;
            for (int i = numberOfItemsPrintedSoFar; i < listView1.Items.Count; i++)
            {
                numberOfItemsPerPage++;

                if (numberOfItemsPerPage <= 25)
                {
                    numberOfItemsPrintedSoFar++;

                    if (numberOfItemsPrintedSoFar <= listView1.Items.Count)
                    {

                        foreach (ListViewItem item in listView1.Items)
                        {
                            descripcionArticulo = Convert.ToString(item.SubItems[1].Text);
                            idproducto = Convert.ToString(item.SubItems[0].Text);
                            cantidadArticulo = Convert.ToInt32(item.SubItems[3].Text);
                            precioventaArticulo = Convert.ToDecimal(item.SubItems[2].Text);
                            importeArticulo = Convert.ToDecimal(item.SubItems[4].Text);

                            e.Graphics.DrawString(descripcionArticulo, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(15, yPos));
                            e.Graphics.DrawString(cantidadArticulo.ToString(), new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(190, yPos));
                            e.Graphics.DrawString(precioventaArticulo.ToString(), new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(220, yPos));
                            e.Graphics.DrawString(importeArticulo.ToString(), new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(250, yPos));

                            yPos += 20;
                        }
                        break;

                    }
                    else
                    {
                        e.HasMorePages = false;
                    }
                }
                else
                {
                    numberOfItemsPerPage = 0;
                    e.HasMorePages = true;
                    return;
                }
            }

            e.Graphics.DrawString("___________________________________", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(10, yPos));

            e.Graphics.DrawString("SUB TOTAL: S/. " + txtsubtotal.Text.Trim(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(120, yPos + 30));
            // e.Graphics.DrawString("OP.GRAVADAS:     s/ " + lblgravada.Text.Trim(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(220, yPos + 60));
            e.Graphics.DrawString("IVA:           S/. " + txtigv.Text.Trim(), new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(140, yPos + 60));

            //e.Graphics.DrawString("Descuento : s/ " , new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(550, yPos + 60));
            e.Graphics.DrawString("TOTAL A PAGAR: S/. " + lbtotal.Text.Trim(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(90, yPos + 90));

            e.Graphics.DrawString("SON:" + (letras + " S/."), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(15, yPos + 120));

            e.Graphics.DrawString("¡CANJEAR BOLETA O FACTURA !", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(15, yPos + 150));


            e.Graphics.DrawString("GRACIAS POR SU PREFERENCIA:  " + " ", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(15, yPos + 180));
            //e.Graphics.DrawString("Regrese Pronto. :  "+TextBox4.Text, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(15, yPos + 210));
            e.Graphics.DrawImage(image, 90, yPos + 195, image.Width, image.Height);

            // reset the variables
            numberOfItemsPerPage = 0;
            numberOfItemsPrintedSoFar = 0;



        }
        private void leer()
        {
            numeros = lbtotal.Text;
            letras = conv.enletras(numeros);

        }
        private void generarQr()
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();

            string unir = Convert.ToString(nombreEmpresa + "|" + DateTime.Now + "|" + numero + "|" + cboclientes.Text+ "|" + nombrecomprobante + "|" + numero + "|" + txtsubtotal.Text + "|" + txtigv.Text + "|" + lbtotal.Text);
            qrEncoder.TryEncode(unir, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(100, 100)));
            panel1.BackgroundImage = imagen;

            // Guardar en el disco duro la imagen (Carpeta del proyecto)
            //imagen.Save("imagen.png", ImageFormat.Png);

        }
        public void actualizarCliente()
        {
            cboclientes.DataSource = null;
            cboclientes.DataBindings.Clear();
            label12.DataBindings.Clear();
            txtdni.DataBindings.Clear();


            Getcliente();

        }

    }
    
}
