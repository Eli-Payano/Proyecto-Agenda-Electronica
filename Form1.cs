using AgendaElectronica.Datos;
using AgendaElectronica.Entidades;
using AgendaElectronica.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaElectronica.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int contactoSeleccionadoId = -1;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Contacto nuevo = new Contacto
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Direccion = txtDireccion.Text,
                Genero = cmbGenero.Text,
                EstadoCivil = cmbEstadoCivil.Text,
                Movil = txtMovil.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };
            string error;
            if (!ContactoValidator.Validar(nuevo, out error))
            {
                MessageBox.Show(error, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ContactoDal datos = new ContactoDal();
            if (datos.Insertar(nuevo))
            {
                MessageBox.Show("Contacto guardado correctamente.");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al guardar el contacto.");
            }
        }

        private void CargarContactos()
        {
            ContactoDal datos = new ContactoDal();
            dgvContactos.DataSource = null;
            dgvContactos.DataSource = datos.ObtenerTodos();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvContactos.Visible = false;
            CargarContactos();
        }

        private void dgvContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContactos.Rows[e.RowIndex];
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtApellido.Text = fila.Cells["Apellido"].Value.ToString();
                dtpFechaNacimiento.Value = Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value);
                txtDireccion.Text = fila.Cells["Direccion"].Value.ToString();
                cmbGenero.Text = fila.Cells["Genero"].Value.ToString();
                cmbEstadoCivil.Text = fila.Cells["EstadoCivil"].Value.ToString();
                txtMovil.Text = fila.Cells["Movil"].Value.ToString();
                txtTelefono.Text = fila.Cells["Telefono"].Value.ToString();
                txtEmail.Text = fila.Cells["CorreoElectronico"].Value.ToString();

                // Guarda el ID para futuras operaciones (modificar o eliminar)
                contactoSeleccionadoId = Convert.ToInt32(fila.Cells["Id"].Value);

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (contactoSeleccionadoId == -1)
            {
                MessageBox.Show("Selecciona un contacto para modificar.");
                return;
            }

            Contacto modificado = new Contacto
            {
                Id = contactoSeleccionadoId,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Direccion = txtDireccion.Text,
                Genero = cmbGenero.Text,
                EstadoCivil = cmbEstadoCivil.Text,
                Movil = txtMovil.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };

            ContactoDal datos = new ContactoDal();
            if (datos.Actualizar(modificado))
            {
                MessageBox.Show("Contacto modificado con éxito.");
                CargarContactos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al modificar el contacto.");
            }
        }

        private void btnCargarParaModificar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            string[] partes = texto.Split(' ');

            if (partes.Length == 2)
            {
                string nombre = partes[0];
                string apellido = partes[1];

                ContactoDal datos = new ContactoDal();
                var contacto = datos.ObtenerPorNombreApellido(nombre, apellido);

                if (contacto != null)
                {
                    txtNombre.Text = contacto.Nombre;
                    txtApellido.Text = contacto.Apellido;
                    dtpFechaNacimiento.Value = contacto.FechaNacimiento;
                    txtDireccion.Text = contacto.Direccion;
                    cmbGenero.Text = contacto.Genero;
                    cmbEstadoCivil.Text = contacto.EstadoCivil;
                    txtMovil.Text = contacto.Movil;
                    txtTelefono.Text = contacto.Telefono;
                    txtEmail.Text = contacto.Email;

                    contactoSeleccionadoId = contacto.Id;
                }
                else
                {
                    MessageBox.Show("No se encontró ese contacto.");
                }
            }
            else
            {
                MessageBox.Show("Escribe nombre y apellido separados por espacio.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (contactoSeleccionadoId == -1)
            {
                MessageBox.Show("Selecciona un contacto para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Estás segura de que deseas eliminar este contacto?",
                                          "Confirmar eliminación", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                ContactoDal datos = new ContactoDal();
                if (datos.Eliminar(contactoSeleccionadoId))
                {
                    MessageBox.Show("Contacto eliminado con éxito.");
                    contactoSeleccionadoId = -1;
                    LimpiarCampos();
                    CargarContactos();
                }
                else
                {
                    MessageBox.Show("Error al eliminar el contacto.");
                }
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            dtpFechaNacimiento.Value = DateTime.Now;
            txtDireccion.Text = "";
            cmbGenero.SelectedIndex = -1;
            cmbEstadoCivil.SelectedIndex = -1;
            txtMovil.Text = "";
            txtTelefono.Text = "";
            txtEmail.Text = "";
            txtBuscar.Text = "";
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            ContactoDal datos = new ContactoDal();
            dgvContactos.DataSource = datos.BuscarPorNombre(txtBuscar.Text);
        }


    }
    
}
