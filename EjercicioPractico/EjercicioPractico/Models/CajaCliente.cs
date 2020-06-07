using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico
{
    public partial class CajaCliente
    {
        public CajaCliente()
        {
        }

        public CajaCliente(int cajaID, int clienteID)
        {
            CajaID = cajaID;
            ClienteID = clienteID;
            EstadoCajaClienteID = (int)EnumEstadoCajaCliente.PorAtender;
        }
        
    }
}
