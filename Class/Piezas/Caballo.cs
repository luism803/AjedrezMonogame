using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal class Caballo : Pieza {
        public Caballo(Texture2D tileset, int lado) : base(3, lado, tileset) { }
        public override List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos) {
            jugadas = new List<Posicion>();
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y + 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y + 2));
            AnyadirJugada(tablero, new Posicion(pos.X - 1, pos.Y - 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 1, pos.Y - 2));
            AnyadirJugada(tablero, new Posicion(pos.X + 2, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X + 2, pos.Y + 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 2, pos.Y - 1));
            AnyadirJugada(tablero, new Posicion(pos.X - 2, pos.Y + 1));
            return jugadas;
        }
    }
}