using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_EF
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void cargar()
        {
            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                grid.DataSource = contexto.Compras.ToList();

                if (grid.Columns.Contains("Clientes"))
                {
                    grid.Columns["Clientes"].Visible = false;
                }
            }
        }


        private void llenar()
        {
            if (grid.SelectedRows.Count > 0)
            {
                this.textBox1.Text = grid.SelectedRows[0].Cells[0].Value.ToString();
                this.textBox2.Text = grid.SelectedRows[0].Cells[1].Value.ToString();
                this.textBox3.Text = grid.SelectedRows[0].Cells[2].Value.ToString();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                try
                {
                    Compras nuevaCompra = new Compras
                    {
                        Cedula_Cliente = int.Parse(textBox1.Text),
                        Fecha_Compra = DateTime.Parse(textBox2.Text),
                        Monto_Total = decimal.Parse(textBox3.Text)
                    };

                    contexto.Compras.Add(nuevaCompra);
                    contexto.SaveChanges();

                    MessageBox.Show("Compra agregada exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cargar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error al agregar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void grid_Click(object sender, EventArgs e)
        {
            llenar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
