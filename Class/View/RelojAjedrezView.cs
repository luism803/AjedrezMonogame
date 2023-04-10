using AjedrezMonogame.Class.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AjedrezMonogame.Class.View {
    internal class RelojAjedrezView : IObserver<RelojAjedrezModel> {
        SpriteBatch _spriteBatch;
        private Vector2[] vectorJugadores;
        SpriteFont font;
        public RelojAjedrezView(SpriteBatch _spriteBatch, SpriteFont font, Vector2[] vectorJugadores, string tiempo) {
            this._spriteBatch = _spriteBatch;
            this.font = font;
            this.vectorJugadores = vectorJugadores;
            CalibrarVectores(tiempo);
        }
        private void CalibrarVectores(string tiempo) {
            int length = (int)font.MeasureString(tiempo).X;
            vectorJugadores[0].X -= length / 2;
            vectorJugadores[1].X -= length / 2;
        }
        public void DrawTiempoJugador(RelojAjedrezModel model, int jugador) {
            _spriteBatch.DrawString(font, TiempoToString(model, jugador), vectorJugadores[jugador], Color.Red);
        }
        private string TiempoToString(RelojAjedrezModel model, int jugador) {
            int minutos = (int)(model.GetTiempoJugadores()[jugador] / (1000 * 60));
            int segundos = (int)((model.GetTiempoJugadores()[jugador] / 1000) % 60);
            int milisegundos = (int)(model.GetTiempoJugadores()[jugador] % 1000) / 10;
            return string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos);
        }
        public void OnNext(RelojAjedrezModel model) {
            DrawTiempoJugador(model, 0);
            DrawTiempoJugador(model, 1);

        }
        public void OnCompleted() {
            // Implementación
        }
        public void OnError(Exception error) {
            // Implementación
        }
    }
}
