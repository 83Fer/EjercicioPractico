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

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace EjercicioPractico
{
    public partial class FormPrincipal : Form
    {
        DataTable clienteCajaTable;


        public FormPrincipal()
        {
            InitializeComponent();

            InstanceDataTable();

            this.dataGridView1.MultiSelect = true;

            RefreshDataGridView();
        }

        private void InstanceDataTable()
        {
            this.clienteCajaTable = new DataTable();

            this.clienteCajaTable.Columns.Add("CajaDescripcion", typeof(string));
            this.clienteCajaTable.Columns.Add("CajaID", typeof(int));
            this.clienteCajaTable.Columns.Add("Atendidos", typeof(int));
            this.clienteCajaTable.Columns.Add("PorAtender", typeof(int));
        }

        private void RefreshDataGridView()
        {
            using (EPracticoDBEntities entities = new EPracticoDBEntities())
            {
                this.clienteCajaTable.Clear();

                var cajas = entities.Caja.ToList();

                foreach (var item in cajas.Where(x => x.EstadoCajaID == 1))
                {
                    DataRow data = this.clienteCajaTable.NewRow();

                    data["CajaDescripcion"] = $"Caja{item.CajaID}";
                    data["CajaID"] = item.CajaID;
                    data["Atendidos"] = $"{ item.CajaCliente.Count(x => x.CajaID == item.CajaID && x.EstadoCajaClienteID == 1)}";
                    data["PorAtender"] = $"{item.CajaCliente.Count(x => x.CajaID == item.CajaID && x.EstadoCajaClienteID == 2)}";

                    this.clienteCajaTable.Rows.Add(data);
                }

                this.dataGridView1.DataSource = this.clienteCajaTable;

                this.dataGridView1.Columns["CajaID"].Visible = false;
            }
        }

        private void OpenCaja(object sender, EventArgs e)
        {
            try
            {
                Caja caja = new Caja(EnumEstadoCaja.Abierta);

                CajaService cajaService = new CajaService();
                cajaService.CreateCaja(caja);

                RefreshDataGridView();
                MessageBox.Show($"Se agrego la caja {caja.CajaID}");
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
            
        }

        private void CloseCaja(object sender, EventArgs e)
        {
            try
            {
                if (ValidaSeleccionCaja())
                {
                    CajaService cajaService = new CajaService();
                    int cajaID = int.Parse(this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells["CajaID"].Value.ToString());
                    cajaService.CloseCaja(cajaID);
                    this.RefreshDataGridView();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }
        }

        private void AddCliente(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount == 1)
            {
                MessageBox.Show($"Agregue primero una caja.");
                return;
            }
            FormCliente formCliente = new FormCliente();
            formCliente.ShowDialog();

            if(formCliente.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show($"Se agrego el cliente {formCliente.Cliente.Nombre}");

                CajaClienteService cajaClienteService = new CajaClienteService();
                cajaClienteService.CreateCajaCliente(formCliente.Cliente);
                this.RefreshDataGridView();
            }
        }

        private void AttendCliente(object sender, EventArgs e)
        {
            try
            {
                if (ValidaSeleccionCaja())
                {
                    CajaClienteService cajaClienteService = new CajaClienteService();
                    int cajaID = int.Parse(this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].Cells["CajaID"].Value.ToString());
                    cajaClienteService.AttendCliente(cajaID);
                    this.RefreshDataGridView();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"{err.Message}");
            }

        }

        private bool ValidaSeleccionCaja()
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex != 0 || this.dataGridView1.RowCount == 1)
            {
                throw new Exception("Seleccione una caja.");
            }
            return true;
        } 
    }
}
