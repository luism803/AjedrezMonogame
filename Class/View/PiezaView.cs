using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AjedrezMonogame.Class.View {
    internal class PiezaView {
        protected Texture2D tileset;
        public PiezaView(Texture2D tileset) {
            this.tileset = tileset;
        }
        public void Draw(SpriteBatch spriteBatch, int x, int y, int pieza, int lado) {

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
    }
}
