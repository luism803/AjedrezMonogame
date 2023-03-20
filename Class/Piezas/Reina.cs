using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal class Reina : Pieza {
        public Reina(Texture2D tileset, int lado) : base(1, lado, tileset) { }
        public override List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos, bool comprobar) {
            jugadas = new List<Posicion>();
            Posicion newJugada = new Posicion();
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
            return new Reina(tileset, lado);
        }
    }
}
