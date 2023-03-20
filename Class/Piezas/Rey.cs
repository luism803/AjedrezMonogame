using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal class Rey : Pieza {
        public Rey(Texture2D tileset, int lado) : base(0, lado, tileset) { }
        public override List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y - 1));
            if (comprobar)
                ComprobarJugadas(tablero, pos);
            return jugadas;
        }
        public override Pieza Clone() {
            return new Rey(tileset, lado);
        }
    }
}
