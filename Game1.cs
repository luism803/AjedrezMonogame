using AjedrezMonogame.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace AjedrezMonogame {
    public class Game1 : Game {
        public struct InfoTecla {
            public float TiempoPulsacion;
            public Action<Posicion> AccionTecla;

            public InfoTecla(Action<Posicion> accionTecla) {
                TiempoPulsacion = 0;
                AccionTecla = accionTecla;
            }
        }

        Dictionary<Keys, InfoTecla> teclas = new Dictionary<Keys, InfoTecla>();
        const float tiempoLimite = 150f;

        Texture2D tilesetTexture;

        Posicion puntero;
        Tablero tablero;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
            teclas.Add(Keys.Left, new InfoTecla((posicion) => posicion.Izquierda()));
            teclas.Add(Keys.Right, new InfoTecla((posicion) => posicion.Derecha()));
            teclas.Add(Keys.Up, new InfoTecla((posicion) => posicion.Arriba()));
            teclas.Add(Keys.Down, new InfoTecla((posicion) => posicion.Abajo()));
            teclas.Add(Keys.Enter, new InfoTecla((posicion) => tablero.Seleccionar()));
            teclas.Add(Keys.Space, new InfoTecla((posicion) => tablero.QuitarSeleccion()));
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tilesetTexture = Content.Load<Texture2D>("tilesetChess"); //GraphicsDevice.Viewport.Height / 8
                                                                      // Crear una nueva textura con el tamaño deseado
            int newWidth = (GraphicsDevice.Viewport.Height / 8) * 6;
            int newHeight = (GraphicsDevice.Viewport.Height / 8) * 2;
            Texture2D resizedTexture = new Texture2D(GraphicsDevice, newWidth, newHeight);

            // Redimensionar los datos de la textura original al nuevo tamaño
            Color[] originalData = new Color[tilesetTexture.Width * tilesetTexture.Height];
            tilesetTexture.GetData(originalData);
            Color[] newData = new Color[newWidth * newHeight];
            for (int y = 0; y < newHeight; y++) {
                for (int x = 0; x < newWidth; x++) {
                    int origX = x * tilesetTexture.Width / newWidth;
                    int origY = y * tilesetTexture.Height / newHeight;
                    newData[x + y * newWidth] = originalData[origX + origY * tilesetTexture.Width];
                }
            }

            // Copiar los datos redimensionados a la nueva textura
            resizedTexture.SetData(newData);

            // Usar la nueva textura redimensionada
            tilesetTexture = resizedTexture;

            tablero = new Tablero(GraphicsDevice, tilesetTexture);
            puntero = new Posicion();
            puntero.Set(6, 7);
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState kbs = Keyboard.GetState();

            foreach (var kvp in teclas) {
                InfoTecla infoTecla = kvp.Value;
                if (kvp.Value.TiempoPulsacion <= 0) {
                    if (kbs.IsKeyDown(kvp.Key)) {
                        infoTecla.AccionTecla.Invoke(puntero);
                        infoTecla.TiempoPulsacion = tiempoLimite;
                    }
                } else {
                    infoTecla.TiempoPulsacion -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                teclas[kvp.Key] = infoTecla;
            }

            tablero.Update(puntero);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            tablero.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}