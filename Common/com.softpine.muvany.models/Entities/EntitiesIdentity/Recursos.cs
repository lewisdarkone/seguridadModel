﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace com.softpine.muvany.models.Entities.EntitiesIdentity
{
    public partial class Recursos
    {
        public Recursos()
        {
            Permisos = new HashSet<Permisos>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public int? IdModulo { get; set; }
        public int? EsMenuConfiguracion { get; set; }
        public string DescripcionMenuConfiguracion { get; set; }
        public int? OrdenMenu { get; set; }
        public string Url { get; set; }

        public virtual Modulos IdModuloNavigation { get; set; }
        public virtual ICollection<Permisos> Permisos { get; set; }
    }
}