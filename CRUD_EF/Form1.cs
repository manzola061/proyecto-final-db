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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }
        private void cargar()
        {
            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                grid.DataSource = contexto.Clientes.ToList();

                if (grid.Columns.Contains("Compras"))
                {
                    grid.Columns["Compras"].Visible = false;
                }
            }
        }


        private void llenar()
        {
            this.textBox1.Text = grid.SelectedRows[0].Cells[0].Value.ToString();
            this.textBox2.Text = grid.SelectedRows[0].Cells[1].Value.ToString();
            this.textBox3.Text = grid.SelectedRows[0].Cells[2].Value.ToString();
            this.textBox4.Text = grid.SelectedRows[0].Cells[3].Value.ToString();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Todos los campos deben estar llenos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            int id = int.Parse(this.textBox1.Text);
            string nombre = textBox2.Text;
            string direccion = textBox3.Text;
            string telefono = textBox4.Text;

            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                Clientes clienteExistente = contexto.Clientes.FirstOrDefault(x => x.Cedula_Cliente == id);
                if (clienteExistente != null)
                {
                    MessageBox.Show("Ya existe un cliente con esa cedula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Clientes c = new Clientes
                {
                    Cedula_Cliente = id,
                    Nombre_Cliente = nombre,
                    Direccion_Cliente = direccion,
                    Telefono_Cliente = telefono
                };
                contexto.Clientes.Add(c);
                contexto.SaveChanges();
                cargar();
            }
        }

        private void grid_Click(object sender, EventArgs e)
        {
            llenar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            int id = Convert.ToInt32(this.textBox1.Text);
            string nombre = textBox2.Text;
            string direccion = textBox3.Text;
            string telefono = textBox4.Text;

            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                Clientes c = contexto.Clientes.FirstOrDefault(x => x.Cedula_Cliente == id);
                if (c != null)
                {
                    c.Nombre_Cliente = nombre;
                    c.Direccion_Cliente = direccion;
                    c.Telefono_Cliente = telefono;
                    contexto.SaveChanges();
                    cargar();
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Debe seleccionar un cliente para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(this.textBox1.Text);

            using (BaseProyectoFinalEntitiesConexion contexto = new BaseProyectoFinalEntitiesConexion())
            {
                try
                {
                    Clientes cliente = contexto.Clientes.FirstOrDefault(x => x.Cedula_Cliente == id);

                    if (cliente != null)
                    {
                        var comprasAsociadas = contexto.Compras.Where(c => c.Cedula_Cliente == id).ToList();

                        if (comprasAsociadas.Count > 0)
                        {
                            MessageBox.Show("No se puede eliminar el cliente porque tiene compras asociadas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            contexto.Clientes.Remove(cliente);
                            contexto.SaveChanges();
                            MessageBox.Show("Cliente eliminado exitosamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargar();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cliente no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio un error al intentar eliminar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
