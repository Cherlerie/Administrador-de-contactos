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

        private void CrearContacto(string nombre, string telefono, string descripcion, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Contacto (Nombre, Telefono, Descripcion, Direccion) VALUES (@Nombre, @Telefono, @Descripcion, @Direccion)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
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
                string query = "SELECT * FROM Contacto";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void ActualizarContacto(string nombre, string telefono, string descripcion, string direccion)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Contacto SET Telefono = @Telefono, Descripcion = @Descripcion, Direccion = @Direccion WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Telefono", telefono);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);
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
                string query = "DELETE FROM Contacto WHERE Nombre = @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Contacto eliminado exitosamente.");
                GetDatos();
            }
        }

        private void BuscarContacto(string nombre)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Contacto WHERE Nombre LIKE @Nombre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nombre", $"%{nombre}%");
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CrearContacto(txtNombre.Text, txtNumero.Text, txtDescripcion.Text, txtDireccion.Text);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingresa el nombre del contacto a editar.");
                return;
            }

            ActualizarContacto(txtNombre.Text, txtNumero.Text, txtDescripcion.Text, txtDireccion.Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingresa el nombre del contacto a eliminar.");
                return;
            }

            EliminarContacto(txtNombre.Text);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingresa el nombre del contacto a buscar.");
                return;
            }

            BuscarContacto(txtNombre.Text);
        }
   
private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtNumero.Text = row.Cells["Telefono"].Value.ToString();
                txtDescripcion.Text = row.Cells["Descripcion"].Value.ToString();
                txtDireccion.Text = row.Cells["Direccion"].Value.ToString();
            }
        }

    }

}
