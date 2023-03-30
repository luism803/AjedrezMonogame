using AjedrezMonogame.Class;
using AjedrezMonogame.Class.Model.Piezas;

namespace AjedrezMonogame.Structs {
    internal struct RegistroJugada {
        public Posicion o; //origen
        public Pieza fichaOrigen;
        public Posicion f; //final
        public Pieza fichaFin;
        public RegistroJugada(Posicion o, Posicion f, Pieza fichaOrigen, Pieza fichaFin) {
            this.o = o;
            this.f = f;
            this.fichaOrigen = fichaOrigen;
            this.fichaFin = fichaFin;
        }
    }
}
