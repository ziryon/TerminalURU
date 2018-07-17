﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Persistencia
{
    public class FabricaPersistencia
    {
        public static IPersistenciaEmpleado GetPersistenciaEmpleado() {
            return (PersistenciaEmpleado.GetInstancia());
        }
    }
}
