using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Piezas {
    internal abstract class Pieza {
        protected int pieza;
        protected int lado;
        protected List<Posicion> jugadas;
        public int Lado { get { return lado; } }
        protected Texture2D tileset;
        protected Pieza(int p, int l, Texture2D tileset) {
            pieza = p;
            lado = l;
            this.tileset = tileset;
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y) {

            int tileWidth = tileset.Width / 6;
            int tileHeight = tileset.Height / 2;
            Rectangle tileSourceRect = new Rectangle(
                pieza * tileWidth,
                lado * tileHeight,
                tileWidth,
                tileHeight);
            Vector2 tilePosition = new Vector2(x, y); // Position to draw the tile at
            spriteBatch.Draw(tileset, tilePosition, tileSourceRect, Color.White);
        }
        public abstract List<Posicion> CalcularJugadas(Tablero tablero, Posicion pos, bool comprobar);
        protected virtual bool AnyadirJugada(Tablero tablero, Posicion pos) {
            if (tablero.IsEmpty(pos) || tablero.IsEnemy(pos, lado))
                jugadas.Add(pos.Clone);
            return tablero.IsEmpty(pos);
        }
        public abstract Pieza Clone();
        protected private void ComprobarJugadas(Tablero tablero, Posicion pos) {
            jugadas = jugadas.FindAll(j => {
                tablero.MoverPieza(j.Clone, pos.Clone);
                bool res = !tablero.IsInJaque(lado);
                tablero.Retroceder();
                return res;
            });
        }
    }
}
