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
            public Action AccionTecla;

            public InfoTecla(Action accionTecla) {
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

        bool ratonPresionado;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            teclas.Add(Keys.Left, new InfoTecla(() => tablero.Izquierda()));
            teclas.Add(Keys.Right, new InfoTecla(() => tablero.Derecha()));
            teclas.Add(Keys.Up, new InfoTecla(() => tablero.Arriba()));
            teclas.Add(Keys.Down, new InfoTecla(() => tablero.Abajo()));
            teclas.Add(Keys.Enter, new InfoTecla(() => tablero.Seleccionar()));
            teclas.Add(Keys.Space, new InfoTecla(() => tablero.QuitarSeleccion()));
            teclas.Add(Keys.Tab, new InfoTecla(() => tablero.Retroceder()));

            _graphics.PreferredBackBufferWidth = (int)(_graphics.GraphicsDevice.DisplayMode.Width * 0.75);
            _graphics.PreferredBackBufferHeight = (int)(_graphics.GraphicsDevice.DisplayMode.Height * 0.75);
            _graphics.ApplyChanges();

            base.Initialize();
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
            ratonPresionado = false;
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //CREAR CLASE CONTROLES
            //CREAR CLASE CONTROLES
            //CREAR CLASE CONTROLES
            //CREAR CLASE CONTROLES
            //CREAR CLASE CONTROLES
            //CREAR CLASE CONTROLES
            //RATON
            MouseState mouseState = Mouse.GetState();

            if (!ratonPresionado && mouseState.LeftButton == ButtonState.Pressed) {
                ratonPresionado = true;
                tablero.SeleccionarRaton(new Posicion(mouseState.X, mouseState.Y));
            }
            if (mouseState.RightButton == ButtonState.Pressed)
                tablero.QuitarSeleccion();
            if (ratonPresionado && mouseState.LeftButton == ButtonState.Released) {
                ratonPresionado = false;
                tablero.SeleccionarRaton(new Posicion(mouseState.X, mouseState.Y));
            }
            //TECLADO
            KeyboardState kbs = Keyboard.GetState();

            foreach (var kvp in teclas) {
                InfoTecla infoTecla = kvp.Value;
                if (infoTecla.TiempoPulsacion <= 0) {   //si esta disponble
                    if (kbs.IsKeyDown(kvp.Key)) {   //si la tecla esta siendo presionada
                        infoTecla.AccionTecla.Invoke();
                        infoTecla.TiempoPulsacion = tiempoLimite;
                    }
                } else {
                    infoTecla.TiempoPulsacion -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                }
                teclas[kvp.Key] = infoTecla;
            }

            tablero.Update();
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