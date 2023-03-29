using System.Collections.Generic;
namespace AjedrezMonogame.Class.Model.Piezas {
    internal abstract class Pieza {
        protected int codPieza;
        protected int lado;
        protected List<Posicion> jugadas;
        public int Lado { get { return lado; } }
        public int CodPieza { get { return codPieza; } }
        protected Pieza(int p, int l) {
            codPieza = p;
            lado = l;
        }
        public abstract List<Posicion> CalcularJugadas(TableroModel tablero, Posicion pos, bool comprobar);
        protected virtual bool AnyadirJugada(TableroModel tablero, Posicion pos) {
            if (tablero.IsEmpty(pos) || tablero.IsEnemy(pos, lado))
                jugadas.Add(pos.Clone);
            return tablero.IsEmpty(pos);
        }
        public abstract Pieza Clone();
        protected private void ComprobarJugadas(TableroModel tablero, Posicion pos) {
            jugadas = jugadas.FindAll(j =>
            {
                tablero.MoverPieza(j.Clone, pos.Clone);
                bool res = !tablero.IsInJaque(lado);
                tablero.Retroceder();
                return res;
            });
        }
    }
}
