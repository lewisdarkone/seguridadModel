﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace com.softpine.muvany.models.Entities.EntitiesIdentity
{
    public partial class EndpointsPermisos
    {
        public int Id { get; set; }
        public int EndpointId { get; set; }
        public int PermisoId { get; set; }
        public bool Estado { get; set; }

        public virtual Endpoints Endpoint { get; set; }
        public virtual Permisos Permiso { get; set; }
    }
}