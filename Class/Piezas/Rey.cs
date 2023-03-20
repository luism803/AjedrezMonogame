using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal class Rey : Pieza {
        public Rey(Texture2D tileset, int lado) : base(0, lado, tileset) { }
        public override List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos) {
            jugadas = new List<Posicion>();
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y - 1));
            return jugadas;
        }
    }
}
