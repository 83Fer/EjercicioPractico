using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico
{
    public partial class Cliente
    {
        public Cliente(string nombre)
        {
            Nombre = nombre;
        }

        public bool ValidaCliente()
        {
            if (this.Nombre == "")
            {
                throw new Exception("Tiene que ingresar un nombre.");
            }
            return true;
        }
    }
}
