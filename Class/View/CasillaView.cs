using AjedrezMonogame.Class.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AjedrezMonogame.Class.View {
    internal class CasillaView : IObserver<CasillaModel> {

        Texture2D texture;
        Texture2D textureJugada;
        Texture2D texturePuntero;
        Texture2D textureSeleccion;
        Texture2D textureAtaque;
        Texture2D tileset;
        SpriteBatch _spriteBatch;

        private Rectangle square;
        public CasillaView(GraphicsDevice graphicsDevice, SpriteBatch _spriteBatch, int x, int y, int size, Color color, Texture2D tileset, CasillaModel casillaModel) {

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

            this.tileset = tileset;
            this._spriteBatch = _spriteBatch;

            casillaModel.Subscribe(this);
        }
        public void Draw(CasillaModel model) {
            _spriteBatch.Draw(texture, square, Color.White);
            if (model.Jugada) {
                if (model.Ficha == null)
                    _spriteBatch.Draw(textureJugada, square, Color.White);
                else
                    _spriteBatch.Draw(textureAtaque, square, Color.White);
            }
            if (model.Seleccion) {
                _spriteBatch.Draw(textureSeleccion, square, Color.White);
            }
            if (model.Puntero) {
                _spriteBatch.Draw(texturePuntero, square, Color.White);
            }
            if (model.Ficha != null)  //si tiene una pieza
                DrawPieza(_spriteBatch, square.X, square.Y, model.Ficha.CodPieza, model.Ficha.Lado);   //dibujar la pieza
        }
        private void DrawPieza(SpriteBatch spriteBatch, int x, int y, int pieza, int lado) {
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
        public void OnNext(CasillaModel casillaModel) {
            Draw(casillaModel);
        }
        public void OnCompleted() {
            // Implementación
        }
        public void OnError(Exception error) {
            // Implementación
        }
    }
}
