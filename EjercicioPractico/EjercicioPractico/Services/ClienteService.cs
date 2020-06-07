using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico.Services
{
    public class ClienteService
    {
        public void CreateCliente(Cliente cliente)
        {
            if (cliente.ValidaCliente())
            {
                using (EPracticoDBEntities entities = new EPracticoDBEntities())
                {
                    entities.Cliente.Add(cliente);

                    entities.SaveChanges();
                }
            }
        }
        
    }
}
