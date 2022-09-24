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
            GetCategoria();
            GetMarca();
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
            txtcodigobarras.Focus();
            txtdescripcion.Text = "";
            txtcosto.Text = "";
            txtventa.Text = "";
            txtpreciomayor.Text = "";
            txtstock.Text = "";
            txtstockminimo.Text = "";
            cbocategoria.Text = "";
            cbomarca.Text = "";
            
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
                        MessageBox.Show("Datos exportados correctamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void GetCategoria()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from categoria", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbocategoria.DataSource = dt;
                cbocategoria.DisplayMember = "Nombres";
                cbocategoria.ValueMember = "idCategoria";
                label12.DataBindings.Add(new Binding("Text", dt, "idCategoria"));
                //txtemail.DataBindings.Add(new Binding("Text", dt, "Email"));
                //txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                //txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));


                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombres"])); // columna de la consulta sql
                }
                cbocategoria.AutoCompleteCustomSource = coleccion;
                cbocategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbocategoria.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
        private void GetMarca()
        {
            try
            {
                MySqlConnection conexion = new MySqlConnection();
                MySqlCommand cmd = new MySqlCommand("select*from marca", cn.ObtenerConeccion());
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
               cbomarca.DataSource = dt;
                cbomarca.DisplayMember = "Nombre";
                cbomarca.ValueMember = "Id";
                label13.DataBindings.Add(new Binding("Text", dt, "Id"));
                //txtemail.DataBindings.Add(new Binding("Text", dt, "Email"));
                //txttelefono.DataBindings.Add(new Binding("Text", dt, "Telefono"));
                //txtdocumento.DataBindings.Add(new Binding("Text", dt, "Dni"));


                //autocompletado
                AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
                foreach (DataRow row in dt.Rows)
                {
                    coleccion.Add(Convert.ToString(row["Nombre"])); // columna de la consulta sql
                }
                cbomarca.AutoCompleteCustomSource = coleccion;
                cbocategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbocategoria.AutoCompleteSource = AutoCompleteSource.CustomSource;




            }
            catch (Exception ex)
            {
                throw ex;


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
        private void add()
        {

            try
            {
                if (cbocategoria.Text == "")
                {
                    MessageBox.Show("Debe seleccionar la categoria","Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbocategoria.Focus();

                }
                else if  (cbomarca.Text == "")
                {
                    MessageBox.Show("Debe seleccionar la marca", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbomarca.Focus();
                }

                else
                {
                    String query = "insert into articulos(CodigoBarra,Descripcion,PrecioCosto,PrecioVenta,PrecioMayor,Stock,StockMinimo,IdCategoria,IdMarca)values('" + this.txtcodigobarras.Text + "','" + this.txtdescripcion.Text + "','" + this.txtcosto.Text + "','" + this.txtventa.Text + "','" + (this.txtpreciomayor.Text) + "','" + (this.txtstock.Text) + "','" + (this.txtstockminimo.Text) + "','" + (this.label12.Text) + "','" + (this.label13.Text) + "')";
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

            string update = "update articulos set CodigoBarra='" + this.txtcodigobarras.Text + "',Descripcion='" + this.txtdescripcion.Text + "',PrecioCosto='" + this.txtcosto.Text + "',PrecioVenta='" + this.txtventa.Text + "',PrecioMayor='" + this.txtpreciomayor.Text + "',Stock='" + this.txtstock.Text + "',StockMinimo='" + this.txtstockminimo.Text + "',IdCategoria='" + this.label12.Text + "',IdMarca='" + this.label13.Text + "' where Id='" + this.txtid.Text + "'";
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
                bunifuFlatButton2.Enabled = false;
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
        private void delete()
        {
            DialogResult result = MessageBox.Show("Estas Seguro que quieres eleminar el Registro " + this.txtdescripcion.Text, "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string b = "delete from articulos where id='" + this.txtid.Text + "'";
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

        private void btneliminar_Click(object sender, EventArgs e)
        {
            delete();
        }
    }    
}

