﻿using Sistema.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Negocio
{
    public class NRol
    {
        public static DataTable Listar()
        {
            DRol datos = new DRol();
            return datos.Listar();
        }
    }
}
