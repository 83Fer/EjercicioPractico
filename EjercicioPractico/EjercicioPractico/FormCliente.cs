using EjercicioPractico.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjercicioPractico
{
    public partial class FormCliente : Form
    {
        private Cliente _Cliente { get; set; }

        public Cliente Cliente { get { return _Cliente; } }

        public FormCliente()
        {
            InitializeComponent();
        }

        private void BtnAceptar(object sender, EventArgs e)
        {
            try
            {
                this._Cliente = new Cliente(this.textNombre.Text);

                ClienteService clienteService = new ClienteService();

                clienteService.CreateCliente(this._Cliente);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        private void BtnCancelar(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
