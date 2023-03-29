using System.Collections.Generic;
namespace AjedrezMonogame.Class.Model.Piezas {
    internal class Peon : Pieza {
        public Peon(int lado) : base(5, lado) { }
        public override List<Posicion> CalcularJugadas(TableroModel tablero, Posicion pos, bool comprobar = true) {
            jugadas = new List<Posicion>();
            if (lado == 0) {  //blancas
                //movimiento
                if (AnyadirJugada(tablero, new Posicion(pos.X, pos.Y - 1)) && pos.Y == 6)
                    AnyadirJugada(tablero, new Posicion(pos.X, pos.Y - 2));
                //ataque
                AnyadirAtaque(tablero, new Posicion(pos.X - 1, pos.Y - 1));
                AnyadirAtaque(tablero, new Posicion(pos.X + 1, pos.Y - 1));
            } else {    //negras
                //movimiento
                if (AnyadirJugada(tablero, new Posicion(pos.X, pos.Y + 1)) && pos.Y == 1)
                    AnyadirJugada(tablero, new Posicion(pos.X, pos.Y + 2));
                //ataque
                AnyadirAtaque(tablero, new Posicion(pos.X - 1, pos.Y + 1));
                AnyadirAtaque(tablero, new Posicion(pos.X + 1, pos.Y + 1));
            }
            if (comprobar)
                ComprobarJugadas(tablero, pos);
            return jugadas;
        }
        protected override bool AnyadirJugada(TableroModel tablero, Posicion pos) {
            if (tablero.IsEmpty(pos))
                jugadas.Add(pos.Clone);
            return tablero.IsEmpty(pos);
        }
        private void AnyadirAtaque(TableroModel tablero, Posicion pos) {
            if (tablero.IsEnemy(pos, lado))
                jugadas.Add(pos.Clone);
            if (lado == 0) {
                if (pos.Y == 2 && tablero.IsPeon(new Posicion(pos.X, pos.Y + 1))
                    && tablero.IsLastJugada(new Posicion(pos.X, pos.Y - 1), new Posicion(pos.X, pos.Y + 1)))
                    jugadas.Add(pos.Clone);
            }
            if (lado == 1) {
                if (pos.Y == 5 && tablero.IsPeon(new Posicion(pos.X, pos.Y - 1))
                    && tablero.IsLastJugada(new Posicion(pos.X, pos.Y + 1), new Posicion(pos.X, pos.Y - 1)))
                    jugadas.Add(pos.Clone);
            }
        }
        public override Pieza Clone() {
            return new Peon(lado);
        }
    }
}
