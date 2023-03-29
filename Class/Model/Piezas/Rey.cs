using System.Collections.Generic;
namespace AjedrezMonogame.Class.Model.Piezas {
    internal class Rey : Pieza {
        public Rey(int lado) : base(0, lado) { }
        public override List<Posicion> CalcularJugadas(TableroModel tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y - 1));
            if (comprobar) {
                AnyadirEnroques(tablero, pos);
                ComprobarJugadas(tablero, pos);
            }
            return jugadas;
        }
        private void AnyadirEnroques(TableroModel tablero, Posicion pos) {
            if (lado == 0 && pos.Y == 7 || lado == 1 && pos.Y == 0) {
                Posicion pos1 = new Posicion(pos.X + 1, pos.Y);
                Posicion pos2 = new Posicion(pos.X + 2, pos.Y);
                //DERECHA
                if (!tablero.WasMoved(pos) && !tablero.WasMoved(new Posicion(pos.X + 3, pos.Y)) &&
                    tablero.IsEmpty(pos1) && tablero.IsEmpty(pos2) &&
                    !tablero.IsAtacada(pos1, lado) && !tablero.IsAtacada(pos2, lado))
                    jugadas.Add(pos2);
                //IZQUIERDA
                pos1 = new Posicion(pos.X - 1, pos.Y);
                pos2 = new Posicion(pos.X - 2, pos.Y);
                Posicion pos3 = new Posicion(pos.X - 3, pos.Y);
                if (!tablero.WasMoved(pos) && !tablero.WasMoved(new Posicion(pos.X - 4, pos.Y)) &&
                    tablero.IsEmpty(pos1) && tablero.IsEmpty(pos2) && tablero.IsEmpty(pos3) &&
                    !tablero.IsAtacada(pos1, lado) && !tablero.IsAtacada(pos2, lado) && !tablero.IsAtacada(pos3, lado))
                    jugadas.Add(pos2);
            }
        }
        public override Pieza Clone() {
            return new Rey(lado);
        }
    }
}
