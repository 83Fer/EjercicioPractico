﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EjercicioPractico
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EPracticoDBEntities : DbContext
    {
        public EPracticoDBEntities()
            : base("name=EPracticoDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Caja> Caja { get; set; }
        public virtual DbSet<CajaCliente> CajaCliente { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<EstadoCaja> EstadoCaja { get; set; }
        public virtual DbSet<EstadoCajaCliente> EstadoCajaCliente { get; set; }
    }
}
