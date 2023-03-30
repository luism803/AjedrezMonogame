using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace AjedrezMonogame.Class.Model {
    internal class RelojAjedrezModel {
        public float[] tiempoJugadores;
        public RelojAjedrezModel(float tiempo) {
            tiempoJugadores = new float[2];
            tiempoJugadores[0] = tiempo;
            tiempoJugadores[1] = tiempo;
        }
        public void restarTiempo(float tiempoTranscurrido, int lado) {
            tiempoJugadores[lado] -= tiempoTranscurrido;
        }
        public void Dedbug() {
            Debug.WriteLine(tiempoJugadores[0] + "\t" + tiempoJugadores[1]);
        }
        public void Draw(SpriteBatch _spriteBatch, SpriteFont font) {
            _spriteBatch.DrawString(font, (tiempoJugadores[0] / 1000).ToString("0.00"), new Vector2(10, 10), Color.Red);
        }
    }
}
