using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Registro_de_contactos
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=LAPMASIEL;Database=kallutolv;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetDatos();
        }

        private void CrearContacto(string nombre, string telefono)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Contactos (Nombre, Telefono) VALUES (@Nombre, @Telefono)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Contacto agregado exitosamente.");
                GetDatos();
            }
        }

        private void GetDatos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Contactos";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void ActualizarContacto(string nombreNuevo, string telefonoNuevo)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Contactos SET Nombre = @NombreNuevo, Telefono = @TelefonoNuevo WHERE Nombre = @NombreAntiguo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@NombreNuevo", nombreNuevo);
                    cmd.Parameters.AddWithValue("@TelefonoNuevo", telefonoNuevo);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Contacto actualizado exitosamente.");
                GetDatos();
            }
        }

        private void EliminarContacto(string nombre)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Contactos WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Contacto eliminado exitosamente.");
                GetDatos();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CrearContacto(txtNombre.Text, txtNumero.Text);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            string nombreNuevo = txtNombre.Text;
            string telefonoNuevo = txtNumero.Text;

            ActualizarContacto(nombreNuevo, telefonoNuevo);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarContacto(txtNombre.Text);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Contactos WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }
    }
}
