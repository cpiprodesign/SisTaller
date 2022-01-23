using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.IO;
using System.Drawing.Imaging;

namespace AppTaller
{
    public partial class Form1  : Form 
    {
        conexion cn = new conexion();//
        //datos de la empresa
        string nombreEmpresa, direccion,email,Numero;
        int telefono;
        

        public Form1()
        {
            InitializeComponent();
            
        }
        
       // public MySqlConnection cn = new MySqlConnection("server=localhost;Database=tallercell;Uid=root;Pwd=;");
        string estado;
        int p = 1;
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
            GetClient();
            Gettecnico();
            desabilitar();
            estados();
            getEmpresa();
           
           
        }
        public  void GetClient()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from clientes order by id desc", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboclientes.DataSource = dt;
                cboclientes.DisplayMember = "Nombres";
                cboclientes.ValueMember = "id";
                label27.DataBindings.Add(new Binding("Text", dt, "id"));
                txtdireccion.DataBindings.Add(new Binding("Text", dt, "Direccion"));
                txtemail.DataBindings.Add(new Binding("Text", dt, "Email"));
                txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));

               
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
        private void Gettecnico()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from tecnicos", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbotecnico.DataSource = dt;
                cbotecnico.DisplayMember = "Nombres";
               cbotecnico.ValueMember = "idTecnicos";
               label26.DataBindings.Add(new Binding("Text", dt, "idTecnicos"));
                //txtemail.DataBindings.Add(new Binding("Text", dt, "Email"));
                //txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                //txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));


                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombres"])); // columna de la consulta sql
                }
                cbotecnico.AutoCompleteCustomSource = coleccion;
                cbotecnico.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbotecnico.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblfecha.Text= DateTime.Now.ToString("dddd, d MMMM yyyy");
            lbhora.Text = DateTime.Now.ToString("h:mm:ss");
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //GetClient();
            actualizarCliente();
            p = 1;
            limpiar();
            habilitar();
            btncancelar.Visible = true;
            bunifuFlatButton5.Visible = true;
            bunifuFlatButton5.Text = "Guardar el orden.";
           generar();
            


        }
        private void llenar()
        {
            MySqlCommand cmd = new MySqlCommand("listarOrden",cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idordenes", MySqlDbType.Int64).Value = int.Parse(textBox1.Text);

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            //this.dataGridView1.DataSource = dt;
        }


        private void limpiar()
        {
            textBox1.Text = "";
            cboclientes.Text = "";
            txtdireccion.Text = "";
            txtemail.Text = "";
            txttelefono.Text = ""; ;
            txtdocumento.Text = "";
            txtequipo.Text = "";
            txtmarca.Text = "";
            txtmodelo.Text = "";
            txtserial.Text = "";
            txtclave.Text = "";
            txtaccesorio.Text = "";
            txtobservaciones.Text = "";
            txtfalla.Text = "";
            txtreparacion.Text = "";
            dtpentrada.Text = "";
            dtpfechaentrega.Text = "";
            cbotecnico.Text = "";
            txtpagoadelantado.Text = "0.00";
            txtsaldo.Text = "0.00";
            txttotal.Text = "0.00";
            cboclientes.Focus();
        }
        private void filtro()
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("listarOrden", cn.ObtenerConeccion());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idordenes", MySqlDbType.Int64).Value = int.Parse(textBox1.Text);
                
                cn.ObtenerConeccion();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {


                        cboclientes.Text = (dr.GetString(1).ToString());
                        this.txtequipo.Text = dr.GetString(2).ToString();
                        this.txtmarca.Text = dr.GetString(3).ToString();
                        this.txtmodelo.Text = dr.GetString(4).ToString();
                        this.txtserial.Text = dr.GetString(5).ToString();
                        this.txtclave.Text = dr.GetString(6).ToString();
                        this.txtaccesorio.Text = dr.GetString(7).ToString();
                        this.txtobservaciones.Text = dr.GetString(8).ToString();
                        this.txtfalla.Text = dr.GetString(9).ToString();
                        this.txtreparacion.Text = dr.GetString(10).ToString();
                        this.dtpentrada.Text = dr.GetString(11).ToString();
                        this.dtpfechaentrega.Text = dr.GetString(12).ToString();
                        this.txtpagoadelantado.Text = dr.GetString(13).ToString();
                        this.txttotal.Text = dr.GetString(14).ToString();
                        this.cbotecnico.Text = dr.GetString(16).ToString();
                        estado = dr.GetString(15).ToString();
                        if (estado == "No entregado")
                        {
                            bunifuiOSSwitch1.Value = false;
                            estado = "No entregado";
                            Lblestado.Text = estado;
                            // Lblestado.Visible = false;
                        }
                        else
                        {
                            bunifuiOSSwitch1.Value = true;
                            // lblnoentregado.Visible = false;
                            Lblestado.Visible = true;
                            label28.Visible = true;
                            estado = "Entregado";
                            Lblestado.Text = estado;
                            bunifuiOSSwitch1.Visible = false;

                        }
                    }
                }
                else
                {
                    MessageBox.Show("No hay datos con ese codigo de orden","Sistema");
                    limpiar();
                    desabilitar();

                }
               


                totalizar();


                dr.Close();
                cn.DescargarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
                
            }
        }
        private void totalizar()
        {
            decimal adelanto;
            adelanto = Convert.ToDecimal(txtpagoadelantado.Text);
            txtsaldo.Text = Convert.ToString(Convert.ToDecimal(txttotal.Text) - (adelanto));
        }
        private void desabilitar()
        {
            panelclientes.Enabled = false;
            panelequipo.Enabled = false;
            panelfechas.Enabled = false;
            bunifuCards3.Enabled = false;
            //oculta estado
            label28.Visible = false;
            Lblestado.Visible = false;
            bunifuiOSSwitch1.Visible = false;
            btnnuevo.Visible = false;
            bunifuFlatButton5.Visible = false;
            btncancelar.Visible = false;
        }
        private void habilitar()
        {
            bunifuCards3.Enabled = true;
            panelclientes.Enabled = true;
            panelequipo.Enabled = true;
            panelfechas.Enabled = true;
            Lblestado.Visible = false;
            label28.Visible = false;
            btnnuevo.Visible = true;

        }

        private void txtorden_OnTextChange(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //llenar();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //filtro();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                filtro();
            }
        }

        private void bunifuMaterialTextbox13_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            p = 2;
            habilitar();
            bunifuFlatButton5.Visible = true;
            bunifuFlatButton5.Text = "Actualizar el orden.";
            //muestra estado
            label28.Visible = true;
            Lblestado.Visible = true;
            if (Lblestado.Text == "Entregado")
            {
                bunifuiOSSwitch1.Visible = false;
            }
            else
            {
                bunifuiOSSwitch1.Visible = true;
            }
            
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (p == 1)
            {
                if (cboclientes.Text == "")
                {
                    MessageBox.Show("Debes seleccionar el cliente", "Sistema");
                    cboclientes.Focus();
                }
                else if (cbotecnico.Text == "") 
                 {
                    MessageBox.Show("Debes seleccionar el tecnico", "Sistema");
                    cbotecnico.Focus();
                }
                else if (txttotal.Text=="0.00")
                {
                    MessageBox.Show("Debes ingresar Total", "Sistema");
                    txttotal.Focus();
                }
                else if (txtpagoadelantado.Text == "")
                {
                    MessageBox.Show("Debes ingresar pago adelantado", "Sistema");
                    txtpagoadelantado.Focus();
                }

                else
                {
                    DialogResult dialogResult = MessageBox.Show("Deseas Imprimir el Comprobante ?", "Sistema", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        impri();
                        //impri();
                    }
                      guardar();
                    limpiar();
                    desabilitar();
                    textBox1.Focus();
                }
            }
            else
            {
                Actualizar();
                limpiar();
                desabilitar();
            }
            
            

        }

        private void panelfechas_Paint(object sender, PaintEventArgs e)
        {

        }

        private void estados()
        {
            if (bunifuiOSSwitch1.Value == true)
            {

                Lblestado.Visible = true;
                estado = "Entregado";
                Lblestado.Text = estado;

               
            }
            else
            {
                estado = "No Entregado";
                Lblestado.Text = estado;
               
            }
        }
        private void bunifuiOSSwitch1_OnValueChange(object sender, EventArgs e)
        {


            //bunifuiOSSwitch1.Value = true;
            estados();


        }
        private void guardar()
        {

            try
            {
                MySqlCommand cmd = new MySqlCommand("InsertarOrden",cn.ObtenerConeccion());
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@idOrden", MySqlDbType.Int64).Value = textBox1.Text;
                cmd.Parameters.Add("@idCliente", MySqlDbType.Int64).Value = label27.Text;
                cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar, 100).Value = txtequipo.Text;
                cmd.Parameters.Add("@Marca", MySqlDbType.VarChar, 45).Value = txtmarca.Text;
                cmd.Parameters.Add("@Modelo", MySqlDbType.VarChar, 45).Value = txtmodelo.Text;
                cmd.Parameters.Add("@Serial", MySqlDbType.VarChar, 45).Value = txtserial.Text;
                cmd.Parameters.Add("@Clave", MySqlDbType.VarChar, 45).Value = txtclave.Text;               
                cmd.Parameters.Add("@Accesorios", MySqlDbType.VarChar, 45).Value = txtobservaciones.Text;
                cmd.Parameters.Add("@Observaciones", MySqlDbType.VarChar, 100).Value = txtobservaciones.Text;
                cmd.Parameters.Add("@FallaEquipo", MySqlDbType.VarChar, 100).Value = txtfalla.Text;
                cmd.Parameters.Add("@Reparacion", MySqlDbType.VarChar, 200).Value = txtreparacion.Text;
                cmd.Parameters.Add("@FechaEntrada", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpentrada.Text);
                cmd.Parameters.Add("@FechaEntrega", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpfechaentrega.Text);
                cmd.Parameters.Add("@PagoAdelantado", MySqlDbType.Decimal).Value = txtpagoadelantado.Text;
                cmd.Parameters.Add("@TotalPagar", MySqlDbType.Decimal).Value = txttotal.Text;
                cmd.Parameters.Add("@Estado", MySqlDbType.VarChar, 50).Value = "No entregado";
                cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int64).Value = label26.Text;
                cn.ObtenerConeccion();
                cmd.ExecuteNonQuery();
                MessageBox.Show("orden creado Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiar();
                desabilitar();
                cn.DescargarConexion();
                textBox1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Actualizar()
        {

            try
            {
                if (txtpagoadelantado.Text != txttotal.Text)
                {
                    MessageBox.Show("Recuerda actualizar el pago por el servicio", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtpagoadelantado.Focus();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("UpdateOrden", cn.ObtenerConeccion());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idOrdenes", MySqlDbType.Int64).Value = textBox1.Text;
                    cmd.Parameters.Add("@idCliente", MySqlDbType.Int64).Value = label27.Text;
                    cmd.Parameters.Add("@Nombre", MySqlDbType.VarChar, 100).Value = txtequipo.Text;
                    cmd.Parameters.Add("@Marca", MySqlDbType.VarChar, 45).Value = txtmarca.Text;
                    cmd.Parameters.Add("@Modelo", MySqlDbType.VarChar, 45).Value = txtmodelo.Text;
                    cmd.Parameters.Add("@Serial", MySqlDbType.VarChar, 45).Value = txtserial.Text;
                    cmd.Parameters.Add("@Clave", MySqlDbType.VarChar, 45).Value = txtclave.Text;
                    cmd.Parameters.Add("@Accesorios", MySqlDbType.VarChar, 45).Value = txtobservaciones.Text;
                    cmd.Parameters.Add("@Observaciones", MySqlDbType.VarChar, 100).Value = txtobservaciones.Text;
                    cmd.Parameters.Add("@FallaEquipo", MySqlDbType.VarChar, 100).Value = txtfalla.Text;
                    cmd.Parameters.Add("@Reparacion", MySqlDbType.VarChar, 200).Value = txtreparacion.Text;
                    cmd.Parameters.Add("@FechaEntrada", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpentrada.Text);
                    cmd.Parameters.Add("@FechaEntrega", MySqlDbType.DateTime).Value = Convert.ToDateTime(dtpfechaentrega.Text);
                    cmd.Parameters.Add("@PagoAdelantado", MySqlDbType.Decimal).Value = txtpagoadelantado.Text;
                    cmd.Parameters.Add("@TotalPagar", MySqlDbType.Decimal).Value = txttotal.Text;
                    cmd.Parameters.Add("@Estado", MySqlDbType.VarChar, 50).Value = estado;
                    cmd.Parameters.Add("@IdEmpleado", MySqlDbType.Int64).Value = label26.Text;
                    cn.ObtenerConeccion();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("orden actualizado Corectamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //carga();
                    desabilitar();
                    cn.DescargarConexion();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtpagoadelantado_OnValueChanged(object sender, EventArgs e)
        {
          
        }

        private void txtpagoadelantado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                totalizar();
            }
        }

        private void txttotal_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txttotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                totalizar();
            }
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);
            f.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        public void actualizarCliente()
        {
            cboclientes.DataSource = null;
            cboclientes.DataBindings.Clear();
            label27.DataBindings.Clear();
            txtdireccion.DataBindings.Clear();
            txtemail.DataBindings.Clear();
            txttelefono.DataBindings.Clear();
            txtdocumento.DataBindings.Clear();

            GetClient();

        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            Form2 fr = new Form2(this);
            fr.ShowDialog();


        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            {
                fr.ShowDialog();
            }
        }

        private void printDocument_PrintPagePequeño(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Image i = new Bitmap(200, 50);
            //Graphics g = Graphics.FromImage(i);
            //obtener id
            //Image image = Resources.;
            Image image = panel1.BackgroundImage;
            string cliente;
            cliente = cboclientes.Text;
           
            //e.Graphics.DrawString("BOTICA", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, 15));
            e.Graphics.DrawString(nombreEmpresa, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 10));
            e.Graphics.DrawString("Nro:Orden: " + textBox1.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 30));
            e.Graphics.DrawString("" + DateTime.Now, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(80, 30));

            e.Graphics.DrawString("Cliente: " + cliente, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 40));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 50));
            e.Graphics.DrawString("DATOS DEL EQUIPO:", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(50, 63));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 70));
            e.Graphics.DrawString("Equipo: " + txtequipo.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 80));
            e.Graphics.DrawString("Marca: " + txtmarca.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 90));
            e.Graphics.DrawString("Accesorios: " + txtaccesorio.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 100));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 110));

            e.Graphics.DrawString("OBSERVACIONES:", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(30, 123));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 130));

            e.Graphics.DrawString("Falla de equipo: " + txtfalla.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 140));
            e.Graphics.DrawString("Reparacion: " + txtfalla.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 150));
            e.Graphics.DrawString("Fecha de ingreso: " + dtpentrada.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 160));
            e.Graphics.DrawString("Fechaa de Entrega: " + dtpfechaentrega.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 170));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 180));

            e.Graphics.DrawString("Pago adelanto: " + txtpagoadelantado.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(50, 200));
            e.Graphics.DrawString("Saldo : " + txtsaldo.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(70, 210));
            e.Graphics.DrawString("Total por servicio: " + txttotal.Text, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(50, 220));

            e.Graphics.DrawImage(image, 50, 240, image.Width, image.Height);



            //e.Graphics.DrawString("TELEFONO: " + telefon, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 50));
            //e.Graphics.DrawString("EMAIL: " + email, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 60));
            //e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 70));
            //factura o boleta








        }
        //ticket 80
        private void printDocument_PrintPagePequeñoGrande(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Image i = new Bitmap(200, 50);
            //Graphics g = Graphics.FromImage(i);
            //obtener id
            //Image image = Resources.;
            Image image = panel1.BackgroundImage;
            string cliente;
            cliente = cboclientes.Text;

            //e.Graphics.DrawString("BOTICA", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, 15));
            e.Graphics.DrawString(nombreEmpresa, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, new Point(30, 1));
            e.Graphics.DrawString(direccion, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 17));

            e.Graphics.DrawString(  "Telefono: " +Convert.ToString(telefono), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 27));
            e.Graphics.DrawString("Orden NO: " + textBox1.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 37));
            e.Graphics.DrawString("" + DateTime.Now, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(120, 37));

            e.Graphics.DrawString("Cliente: " + cliente, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 47));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 50));
            e.Graphics.DrawString("DATOS DEL EQUIPO:", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, 65));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 70));
            e.Graphics.DrawString("Equipo: " + txtequipo.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 83));
            e.Graphics.DrawString("Marca: " + txtmarca.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 93));
            e.Graphics.DrawString("Accesorios: " + txtaccesorio.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 103));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 110));

            e.Graphics.DrawString("OBSERVACIONES:", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(30, 124));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 130));

            e.Graphics.DrawString("Obs: " + txtfalla.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 142));
            e.Graphics.DrawString("Rep: " + txtreparacion.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 152));
            //e.Graphics.DrawString("Fecha de ingreso: " + dtpentrada.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 163));
            e.Graphics.DrawString("Fecha de Entrega: " + dtpfechaentrega.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 173));
            e.Graphics.DrawString("_________________________________________", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 180));

            //e.Graphics.DrawString("Pago adelanto: " + txtpagoadelantado.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(80, 200));
            //e.Graphics.DrawString("Saldo : " + txtsaldo.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(90, 210));
            //e.Graphics.DrawString("Total por servicio: " + txttotal.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(80, 220));

            //e.Graphics.DrawString("Saldo : " + txtsaldo.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(90, 210));
            e.Graphics.DrawString("Total por servicio: " + txttotal.Text + " $ ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(80, 200));
            e.Graphics.DrawString("Pago adelantado: " + txtpagoadelantado.Text + " $ ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(80, 210));
            e.Graphics.DrawString("Pendiente : "  + txtsaldo.Text + " $ ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(90, 220));


            e.Graphics.DrawImage(image, 80, 240, image.Width, image.Height);
            e.Graphics.DrawString("La fecha de entrega es condicional.", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(50, 340));
            e.Graphics.DrawString(" Puede variar según disponibilidad de repuesto.", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 350));
            e.Graphics.DrawString(" * ESTIMADO CLIENTE CONSERVE SU TICKET *", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(10, 362));
            //e.Graphics.DrawString(" Puede variar según disponibilidad de repuesto.. : " + txttotal.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, new Point(20, 280));


            //e.Graphics.DrawString("TELEFONO: " + telefon, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 50));
            //e.Graphics.DrawString("EMAIL: " + email, new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(5, 60));
            //e.Graphics.DrawString("_________________________________________", new Font("Arial", 6, FontStyle.Regular), Brushes.Black, new Point(3, 70));
            //factura o boleta








        }
        private void generar()
        {

            MySqlCommand cmd = new MySqlCommand("generar", cn.ObtenerConeccion());
            cmd.CommandType = CommandType.StoredProcedure;
            ///cmd.Parameters.AddWithValue("@idordenes", MySqlDbType.Int64).Value = int.Parse(textBox1.Text);

            cn.ObtenerConeccion();
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {


                    textBox1.Text = (dr.GetString(0).ToString());
                    cboclientes.Focus();
                }
            }
            dr.Close();
            cn.DescargarConexion();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }
        private void impri()
        {
            DialogResult dialogResult = MessageBox.Show("Deseas Imprimir el Comprobante ?", "Sistema", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                generarQr();
                //printDocument.Print();
                PrintDocument pd = new PrintDocument();
                //PaperSize ps = new PaperSize("Boleta", 200, 400);
                //medida para ticketera 80
                 PaperSize ps = new PaperSize("Boleta", 300, 380);

                pd.PrintPage += new PrintPageEventHandler(printDocument_PrintPagePequeñoGrande);

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
                MessageBox.Show("No se imprimio el comprobante");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            generarQr();
        }
        private void generarQr()
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();

            string unir = Convert.ToString(DateTime.Now+"|" + textBox1.Text +"|"+ cboclientes.Text + "|" + txtpagoadelantado.Text + "|" + txtsaldo.Text+ "|" + txttotal.Text);
            qrEncoder.TryEncode(unir, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(100, 100)));
            panel1.BackgroundImage = imagen;

            // Guardar en el disco duro la imagen (Carpeta del proyecto)
           // imagen.Save("imagen.png", ImageFormat.Png);
            
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Form4 fr = new Form4();
            fr.ShowDialog();
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
            desabilitar();
            bunifuFlatButton5.Visible = false;
            btncancelar.Visible = false;
        }

        private void Lblestado_Click(object sender, EventArgs e)
        {

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
                    Numero =Convert.ToString (dr["Numero"].ToString());
                    
                    telefono =Convert.ToInt32 (dr["telefono"].ToString());
                    email = dr["email"].ToString();
                }




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
    }
}
