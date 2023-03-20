using AjedrezMonogame.Class.Piezas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace AjedrezMonogame.Class {
    internal class Tablero {
        public struct RegistroJugada {
            public Posicion o; //origen
            public Pieza fichaOrigen;
            public Posicion f; //destino
            public Pieza fichaFin;
            public RegistroJugada(Posicion o, Posicion f, Pieza fichaOrigen, Pieza fichaFin) {
                this.o = o;
                this.f = f;
                this.fichaOrigen = fichaOrigen;
                this.fichaFin = fichaFin;
            }
        }
        public Posicion Puntero { get; set; }
        private Posicion seleccion;
        private List<Posicion> jugadas;
        private Casilla[,] casillas;
        private Casilla casillaSeleccion;
        private List<RegistroJugada> registro;
        public Tablero(GraphicsDevice graphicsDevice, Posicion puntero, Texture2D tileset) {
            this.Puntero = puntero;
            casillas = new Casilla[8, 8];
            jugadas = new List<Posicion>();

            Color blanca = Color.FromNonPremultiplied(250, 220, 175, 255);
            Color negra = Color.FromNonPremultiplied(200, 150, 100, 255);

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    Color color = ((x + y) % 2 == 0) ? blanca : negra;
                    casillas[x, y] = new Casilla(graphicsDevice, x, y,
                        graphicsDevice.Viewport.Height / 8,
                        color
                    );
                }
            }
            ColocarPiezas(tileset);
            seleccion = null;
            casillaSeleccion = null;
            registro = new List<RegistroJugada>();
        }
        public Tablero(GraphicsDevice graphicsDevice, Texture2D tileset)
            : this(graphicsDevice, new Posicion(), tileset) { }
        public void Update(Posicion puntero) {
            jugadas.Clear();
            this.Puntero = puntero;
            //seleccionar
            if (seleccion != null) {
                if (casillas[seleccion.X, seleccion.Y].Ficha != null) { //si hay ficha
                    jugadas = casillas[seleccion.X, seleccion.Y].Ficha.CalcularJugadas(this, seleccion, true);
                }
            }
            foreach (var casilla in casillas) {
                casilla.Puntero = casilla.Pos.Equals(puntero);
                casilla.Seleccion = casilla.Pos.Equals(seleccion);
                casilla.Jugada = jugadas.Exists((j) => casilla.Pos.Equals(j));
            }
        }
        public void Draw(SpriteBatch _spriteBatch) {
            foreach (Casilla casilla in casillas)
                casilla.Draw(_spriteBatch);
        }
        private void ColocarPiezas(Texture2D tileset) {
            //peones blancos
            for (int i = 0; i < 8; i++)
                casillas[i, 6].Ficha = new Peon(tileset, 0);
            ////peones negros
            for (int i = 0; i < 8; i++)
                casillas[i, 1].Ficha = new Peon(tileset, 1);
            //torres blancas
            casillas[7, 7].Ficha = new Torre(tileset, 0);
            casillas[0, 7].Ficha = new Torre(tileset, 0);
            //torres negras
            casillas[7, 0].Ficha = new Torre(tileset, 1);
            casillas[0, 0].Ficha = new Torre(tileset, 1);
            //alfiles blancos
            casillas[2, 7].Ficha = new Alfil(tileset, 0);
            casillas[5, 7].Ficha = new Alfil(tileset, 0);
            //alfiles negros
            casillas[2, 0].Ficha = new Alfil(tileset, 1);
            casillas[5, 0].Ficha = new Alfil(tileset, 1);
            //caballos blancos
            casillas[1, 7].Ficha = new Caballo(tileset, 0);
            casillas[6, 7].Ficha = new Caballo(tileset, 0);
            //caballos negros
            casillas[1, 0].Ficha = new Caballo(tileset, 1);
            casillas[6, 0].Ficha = new Caballo(tileset, 1);
            //reina blanca
            casillas[3, 7].Ficha = new Reina(tileset, 0);
            //reina negra
            casillas[3, 0].Ficha = new Reina(tileset, 1);
            //rey blanco
            casillas[4, 7].Ficha = new Rey(tileset, 0);
            //rey negro
            casillas[4, 0].Ficha = new Rey(tileset, 1);
        }
        public void Seleccionar() {
            if (casillaSeleccion == null) { //no hay seleccion
                seleccion = Puntero.Clone;
                casillaSeleccion = casillas[seleccion.X, seleccion.Y];
            } else if (jugadas.Exists((j) => Puntero.Equals(j))) { //se apunta hacia una casilla que es una jugada
                MoverPieza();
            }
        }
        public void QuitarSeleccion() {
            seleccion = null;
            casillaSeleccion = null;
        }
        private void MoverPieza() {
            //guardar
            Pieza origen = null;
            if (casillas[seleccion.X, seleccion.Y].Ficha != null)
                origen = casillas[seleccion.X, seleccion.Y].Ficha.Clone();
            Pieza final = null;
            if (casillas[Puntero.X, Puntero.Y].Ficha != null)
                final = casillas[Puntero.X, Puntero.Y].Ficha.Clone();
            registro.Add(new RegistroJugada(seleccion.Clone, Puntero.Clone,
                    origen,
                    final
                )
            );
            casillas[Puntero.X, Puntero.Y].Ficha = casillaSeleccion.Ficha;
            casillaSeleccion.Ficha = null;
            QuitarSeleccion();
        }
        public void MoverPieza(Posicion fin, Posicion ori) {
            //guardar
            Pieza origen = null;
            if (casillas[ori.X, ori.Y].Ficha != null)
                origen = casillas[ori.X, ori.Y].Ficha.Clone();
            Pieza final = null;
            if (casillas[fin.X, fin.Y].Ficha != null)
                final = casillas[fin.X, fin.Y].Ficha.Clone();
            registro.Add(new RegistroJugada(ori.Clone, fin.Clone,
                    origen,
                    final
                )
            );
            casillas[fin.X, fin.Y].Ficha = origen;
            casillas[ori.X, ori.Y].Ficha = null;
        }
        private bool IsInside(Posicion pos) {
            return pos.X >= 0 && pos.X <= 7 &&
                pos.Y >= 0 && pos.Y <= 7;
        }
        public bool IsEmpty(Posicion pos) {
            return IsInside(pos) && casillas[pos.X, pos.Y].Ficha == null;
        }
        public bool IsEnemy(Posicion pos, int lado) {
            return IsInside(pos) && casillas[pos.X, pos.Y].Ficha != null &&
            casillas[pos.X, pos.Y].Ficha.Lado != lado;
        }
        public bool IsInJaque(int lado) {
            Posicion posRey = GetRey(lado);
            List<Posicion> jugadas = new List<Posicion>();
            foreach (Casilla casilla in casillas)
                if (IsEnemy(casilla.Pos, lado))
                    jugadas.AddRange(casilla.Ficha.CalcularJugadas(this, casilla.Pos, false));
            //coger todas las jugadas de las casillas enemigas y comprobar que el rey no esta en ellas
            return jugadas.Exists(j => j.Equals(posRey));
        }
        private Posicion GetRey(int lado) {
            foreach (Casilla casilla in casillas)
                if (!IsEnemy(casilla.Pos, lado) && casilla.Ficha is Rey)
                    return casilla.Pos;
            return null;
        }
        public void Retroceder() {
            if (registro.Count > 0) {
                RegistroJugada registroJugada = registro.Last();
                //Debug.WriteLine(registroJugada.o.X + "\t" + registroJugada.o.Y + "\n" + registroJugada.f.X + "\t" + registroJugada.f.Y);
                //seleccion = null;
                //Puntero = registroJugada.f;
                //Seleccionar();
                //Puntero = registroJugada.o;
                //MoverPieza(false);
                casillas[registroJugada.o.X, registroJugada.o.Y].Ficha = registroJugada.fichaOrigen;
                casillas[registroJugada.f.X, registroJugada.f.Y].Ficha = registroJugada.fichaFin;
                registro.RemoveAt(registro.Count - 1);
            }
        }
    }
}
