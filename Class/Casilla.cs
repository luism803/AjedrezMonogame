using AjedrezMonogame.Class.Piezas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AjedrezMonogame.Class {
    internal class Casilla {
        public Posicion Pos { get; set; }
        public bool Puntero { get; set; }
        public bool Jugada { get; set; }
        public bool Seleccion { get; set; }
        public Pieza Ficha { get; set; }

        Texture2D texture;
        Texture2D textureJugada;
        Texture2D texturePuntero;
        Texture2D textureSeleccion;
        Texture2D textureAtaque;

        private Rectangle square;
        public Casilla(GraphicsDevice graphicsDevice, int x, int y, int size, Color color, bool puntero = false) {
            Puntero = puntero;
            Jugada = false;

            Color colorPuntero = Color.FromNonPremultiplied(20, 160, 20, 255);
            Color colorJugada = Color.FromNonPremultiplied(130, 200, 255, 175);
            Color colorSeleccion = Color.FromNonPremultiplied(120, 120, 120, 255);
            Color colorAtaque = Color.FromNonPremultiplied(255, 50, 0, 175);

            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { color });

            texturePuntero = new Texture2D(graphicsDevice, 1, 1);
            texturePuntero.SetData(new[] { colorPuntero });

            textureJugada = new Texture2D(graphicsDevice, 1, 1);
            textureJugada.SetData(new[] { colorJugada });

            textureAtaque = new Texture2D(graphicsDevice, 1, 1);
            textureAtaque.SetData(new[] { colorAtaque });

            textureSeleccion = new Texture2D(graphicsDevice, 1, 1);
            textureSeleccion.SetData(new[] { colorSeleccion });

            square = new Rectangle(x * size, y * size, size, size);
            Pos = new Posicion(x, y);
        }
        public void Draw(SpriteBatch _spriteBatch) {
            _spriteBatch.Draw(texture, square, Color.White);
            if (Jugada) {
                if (Ficha == null)
                    _spriteBatch.Draw(textureJugada, square, Color.White);
                else
                    _spriteBatch.Draw(textureAtaque, square, Color.White);
            }
            if (Seleccion) {
                _spriteBatch.Draw(textureSeleccion, square, Color.White);
            }
            if (Puntero) {
                _spriteBatch.Draw(texturePuntero, square, Color.White);
            }
            if (Ficha != null)  //si tiene una pieza
                Ficha.Draw(_spriteBatch, square.X, square.Y);   //dibujar la pieza
        }
    }
}
