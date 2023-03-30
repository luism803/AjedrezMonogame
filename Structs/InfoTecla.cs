using System;

namespace AjedrezMonogame.Structs {
    public struct InfoTecla {
        public float TiempoPulsacion;
        public Action AccionTecla;

        public InfoTecla(Action accionTecla) {
            TiempoPulsacion = 0;
            AccionTecla = accionTecla;
        }
    }
}
