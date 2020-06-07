using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico.Services
{
    public class CajaService
    {
        public void CreateCaja(Caja caja)
        {
            using (EPracticoDBEntities entities = new EPracticoDBEntities())
            {
                entities.Caja.Add(caja);
                entities.SaveChanges();
            }
        }

        public void CloseCaja(int cajaID)
        {
            using (EPracticoDBEntities entities = new EPracticoDBEntities())
            {
                CajaClienteService cajaClienteService = new CajaClienteService();
                var caja = entities.Caja.FirstOrDefault(x => x.CajaID == cajaID);
                UpdateCaja(caja);
                entities.SaveChanges();
                var cajaClientes = entities.CajaCliente.Where(x => x.CajaID == caja.CajaID && x.EstadoCajaClienteID == 2);
                foreach (var item in cajaClientes)
                {
                    cajaClienteService.AddCajaCliente(item.Cliente, entities);
                }
                entities.SaveChanges();
            }
        }

        private void UpdateCaja(Caja caja)
        {
            caja.EstadoCajaID = (int)EnumEstadoCaja.Cerrada;
        }
    }
}
