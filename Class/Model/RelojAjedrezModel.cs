using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace AjedrezMonogame.Class.Model {
    internal class RelojAjedrezModel {
        public float[] tiempoJugadores;
        Vector2[] vectorJugadores;
        public RelojAjedrezModel(float tiempo, Vector2[] vectorJugadores) {
            tiempoJugadores = new float[2];
            tiempoJugadores[0] = tiempo;
            tiempoJugadores[1] = tiempo;
            this.vectorJugadores = vectorJugadores;
        }
        public void restarTiempo(float tiempoTranscurrido, int lado) {
            tiempoJugadores[lado] -= tiempoTranscurrido;
        }
        public void Dedbug() {
            Debug.WriteLine(tiempoJugadores[0] + "\t" + tiempoJugadores[1]);
        }
        public void Draw(SpriteBatch _spriteBatch, SpriteFont font) {
            DrawTiempoJugador(_spriteBatch, font, 0);
            DrawTiempoJugador(_spriteBatch, font, 1);
        }
        public void DrawTiempoJugador(SpriteBatch _spriteBatch, SpriteFont font, int jugador) {
            int minutos = (int)(tiempoJugadores[jugador] / (1000 * 60));
            int segundos = (int)((tiempoJugadores[jugador] / 1000) % 60);
            int milisegundos = (int)(tiempoJugadores[jugador] % 1000) / 10;
            _spriteBatch.DrawString(font, string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos), vectorJugadores[jugador], Color.Red);
        }
    }
}
