using System.Collections.Generic;
namespace AjedrezMonogame.Class.Model.Piezas {
    internal class Caballo : Pieza {
        public Caballo(int lado) : base(3, lado) { }
        public override List<Posicion> CalcularJugadas(TableroModel tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y + 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y + 2));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y - 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y - 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 2, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 2, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 2, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 2, pos.Y + 1));
            if (comprobar)
                ComprobarJugadas(tablero, pos);
            return jugadas;
        }
        public override Pieza Clone() {
            return new Caballo(lado);
        }
    }
}