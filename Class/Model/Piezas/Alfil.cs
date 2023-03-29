using System.Collections.Generic;

namespace AjedrezMonogame.Class.Model.Piezas {
    internal class Alfil : Pieza {
        public Alfil(int lado) : base(2, lado) { }
        public override List<Posicion> CalcularJugadas(TableroModel tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            Posicion newJugada;

            /*ARRIBA DERECHA*/
            newJugada = new Posicion(pos.X + 1, pos.Y + 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X++;
                newJugada.Y++;
            }
            /*ARRIBA IZQUIERDA*/
            newJugada = new Posicion(pos.X - 1, pos.Y + 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X--;
                newJugada.Y++;
            }
            /*ABAJO DERECHA*/
            newJugada = new Posicion(pos.X + 1, pos.Y - 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X++;
                newJugada.Y--;
            }
            /*ABAJO IZQUIERDA*/
            newJugada = new Posicion(pos.X - 1, pos.Y - 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X--;
                newJugada.Y--;
            }
            if (comprobar)
                ComprobarJugadas(tablero, pos);
            return jugadas;
        }
        public override Pieza Clone() {
            return new Alfil(lado);
        }
    }
}
