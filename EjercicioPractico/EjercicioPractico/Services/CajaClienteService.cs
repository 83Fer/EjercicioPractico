using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioPractico.Services
{
    public class CajaClienteService
    {
        private int CajaID { get; set; }

        public void CreateCajaCliente(Cliente cliente)
        {
            using (EPracticoDBEntities entities = new EPracticoDBEntities())
            {
                if (ExistCajaInCajaCliente(entities) || entities.CajaCliente.Any())
                {
                    if (this.CajaID > 0)
                    {
                        entities.CajaCliente.Add(new CajaCliente(this.CajaID, cliente.ClienteID));
                    }
                    else
                    {
                        AddCajaCliente(cliente, entities);
                    }
                }
                else
                {
                    entities.CajaCliente.Add(new CajaCliente(entities.Caja.FirstOrDefault().CajaID, cliente.ClienteID));
                }
                entities.SaveChanges();
            }
        }

        private bool ExistCajaInCajaCliente(EPracticoDBEntities entities)
        {
            foreach (var item in entities.Caja.OrderBy(x => x.CajaID))
            {
                var existeCaja = entities.CajaCliente.Any(x => x.CajaID == item.CajaID);
                if (!existeCaja)
                {
                    this.CajaID = item.CajaID;
                    return true;
                }
            }

            return false;
        }

        public void AddCajaCliente(Cliente cliente, EPracticoDBEntities entities)
        {
            var cajas = entities.CajaCliente.Where(z => z.Caja.EstadoCajaID == 1).GroupBy(x => x.CajaID).Select(y => new { CajaID = y.Key, CantAtendido = y.Sum(x => x.EstadoCajaClienteID) });

            var cajaMenosUsada = cajas.OrderBy(x => x.CantAtendido).FirstOrDefault();
            entities.CajaCliente.Add(new CajaCliente(cajaMenosUsada.CajaID, cliente.ClienteID));
        }


        public void AttendCliente(int cajaID)
        {
            using (EPracticoDBEntities entities = new EPracticoDBEntities())
            {
                var cajaCliente = entities.CajaCliente.FirstOrDefault(x => x.CajaID == cajaID && x.EstadoCajaClienteID == 2);

                UpdateCajaCliente(cajaCliente);

                entities.SaveChanges();
            }
        }

        private void UpdateCajaCliente(CajaCliente cajaCliente)
        {
            cajaCliente.EstadoCajaClienteID = (int)EnumEstadoCajaCliente.Atendido;
        }
    }
}
