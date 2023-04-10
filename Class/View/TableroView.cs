using AjedrezMonogame.Class.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AjedrezMonogame.Class.View {
    internal class TableroView : IObserver<TableroModel> {
        private CasillaView[,] casillasView;
        private RelojAjedrezView relojView;
        public TableroView(GraphicsDevice graphicsDevice, SpriteBatch _spriteBatch, SpriteFont font, Texture2D tileset, TableroModel tablero, Vector2[] vectorJugadores, int height) {
            CasillaModel[,] casillasModel = tablero.GetCasillas();

            casillasView = new CasillaView[8, 8];
            relojView = new RelojAjedrezView(_spriteBatch, font, vectorJugadores, "00:00:00");

            Color blanca = Color.FromNonPremultiplied(250, 220, 175, 255);
            Color negra = Color.FromNonPremultiplied(200, 150, 100, 255);

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    Color color = (((x + y) & 1) == 0) ? blanca : negra;
                    casillasView[x, y] = new CasillaView(graphicsDevice, _spriteBatch, x, y,
                        height / 8,
                        color,
                        tileset
                    );
                    casillasModel[x, y].Subscribe(casillasView[x, y]);
                }
            }

            tablero.GetReloj().Subscribe(relojView);
        }
        public void Draw() {
        }
        public void OnNext(TableroModel casillaModel) {
        }
        public void OnCompleted() {
            // Implementación
        }
        public void OnError(Exception error) {
            // Implementación
        }
    }
}
