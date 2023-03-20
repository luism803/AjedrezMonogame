using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal class Torre : Pieza {
        public Torre(Texture2D tileset, int lado) : base(4, lado, tileset) { }
        public override List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            Posicion newJugada;

            /*ARRIBA*/
            newJugada = new Posicion(pos.X, pos.Y + 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.Y++;
            }
            /*DERECHA*/
            newJugada = new Posicion(pos.X + 1, pos.Y);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X++;
            }
            /*ABAJO*/
            newJugada = new Posicion(pos.X, pos.Y - 1);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.Y--;
            }
            /*IZQUIERDA*/
            newJugada = new Posicion(pos.X - 1, pos.Y);
            while (AnyadirJugada(tablero, newJugada)) {
                newJugada.X--;
            }
            if (comprobar)
                ComprobarJugadas(tablero, pos);
            return jugadas;
        }
        public override Pieza Clone() {
            return new Torre(tileset, lado);
        }
    }
}
