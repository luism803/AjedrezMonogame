using AjedrezMonogame.Class.Piezas;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AjedrezMonogame.Class {
    internal class Tablero {
        public struct RegistroJugada {
            public Posicion o; //origen
            public Pieza fichaOrigen;
            public Posicion f; //final
            public Pieza fichaFin;
            public RegistroJugada(Posicion o, Posicion f, Pieza fichaOrigen, Pieza fichaFin) {
                this.o = o;
                this.f = f;
                this.fichaOrigen = fichaOrigen;
                this.fichaFin = fichaFin;
            }
        }
        private Posicion puntero;
        private Posicion seleccion;
        private List<Posicion> jugadas;
        private Casilla[,] casillas;
        private Casilla casillaSeleccion;
        private List<RegistroJugada> registro;
        private Texture2D tileset;
        private bool coronando;
        private int ladoCoronacion;
        Pieza[] piezasActuales;
        Posicion[] posiciones;
        public Tablero(GraphicsDevice graphicsDevice, Posicion puntero, Texture2D tileset) {
            this.puntero = puntero;
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
            this.tileset = tileset;
            ColocarPiezas();
            seleccion = null;
            casillaSeleccion = null;
            registro = new List<RegistroJugada>();
            coronando = false;
            ladoCoronacion = 0;
            piezasActuales = new Pieza[3];
            posiciones = new Posicion[3];
        }
        public Tablero(GraphicsDevice graphicsDevice, Texture2D tileset)
            : this(graphicsDevice, new Posicion(), tileset) { }
        public void Update() {
            //GUARDAR JUGADAS DE LA CASILLA SELECCIONADA
            if (!coronando)
                jugadas.Clear();    //vaciar las jugadas posibles
            if (seleccion != null) {    //si hay seleccionada una casilla
                if (casillas[seleccion.X, seleccion.Y].Ficha != null) { //si hay una ficha en la casilla seleccionada
                    jugadas = casillas[seleccion.X, seleccion.Y].Ficha.CalcularJugadas(this, seleccion, true);  //guardar las jugadas posibles de esa jugada
                }
            }
            //ACTUALIZAR COLORES DE LAS CASILLAS
            foreach (var casilla in casillas) {
                casilla.Puntero = casilla.Pos.Equals(puntero);  //si la casilla esta siendo apuntada
                casilla.Seleccion = casilla.Pos.Equals(seleccion);  //si la casilla esta sinedo seleccionada
                casilla.Jugada = jugadas.Exists((j) => casilla.Pos.Equals(j));  //si la casilla es una jugada posible
            }
            //CORONACION
            if (PeonCoronando() != null) {
                coronando = true;
                ElegirCoronacion();
            }
        }
        private void ElegirCoronacion() {
            Posicion peonCoronado = PeonCoronando();
            ladoCoronacion = 0;
            //blancas
            if (casillas[peonCoronado.X, peonCoronado.Y].Ficha.Lado == 0) {
                //posiciones
                posiciones[0] = new Posicion(peonCoronado.X, peonCoronado.Y + 1);
                posiciones[1] = new Posicion(peonCoronado.X, peonCoronado.Y + 2);
                posiciones[2] = new Posicion(peonCoronado.X, peonCoronado.Y + 3);
                ladoCoronacion = 0;
            } else {
                //posiciones
                posiciones[0] = new Posicion(peonCoronado.X, peonCoronado.Y - 1);
                posiciones[1] = new Posicion(peonCoronado.X, peonCoronado.Y - 2);
                posiciones[2] = new Posicion(peonCoronado.X, peonCoronado.Y - 3);
                ladoCoronacion = 1;
            }
            //guardar fichas actuales
            piezasActuales[0] = casillas[posiciones[0].X, posiciones[0].Y].Ficha;
            piezasActuales[1] = casillas[posiciones[1].X, posiciones[1].Y].Ficha;
            piezasActuales[2] = casillas[posiciones[2].X, posiciones[2].Y].Ficha;
            //Mostrar opciones
            casillas[peonCoronado.X, peonCoronado.Y].Ficha = new Reina(tileset, ladoCoronacion);
            casillas[posiciones[0].X, posiciones[0].Y].Ficha = new Caballo(tileset, ladoCoronacion);
            casillas[posiciones[1].X, posiciones[1].Y].Ficha = new Torre(tileset, ladoCoronacion);
            casillas[posiciones[2].X, posiciones[2].Y].Ficha = new Alfil(tileset, ladoCoronacion);
            //añadir opciones a jugadas
            jugadas.Add(peonCoronado);
            jugadas.Add(posiciones[0]);
            jugadas.Add(posiciones[1]);
            jugadas.Add(posiciones[2]);
        }
        private Posicion PeonCoronando() {
            foreach (Casilla casilla in casillas)
                if (casilla.Ficha is Peon && (casilla.Pos.Y == 0 || casilla.Pos.Y == 7))
                    return casilla.Pos;
            return null;
        }
        public void Draw(SpriteBatch _spriteBatch) {    //dibujar todas las casillas
            foreach (Casilla casilla in casillas)
                casilla.Draw(_spriteBatch);
        }
        private void ColocarPiezas() {
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
        public void Seleccionar() { //FALTA CREAR UNA SOBRECARGA QUE RECIBA EL PUNTERO (Posicion) si se mete jugar con el raton
            if (!coronando) {
                if (casillaSeleccion == null) { //si no hay seleccion
                    int ladoActual = CalcularLadoActual(); //lado de la ficha que le toca jugar
                    seleccion = puntero.Clone;  //actualizar la seleccion (Posicion)
                    casillaSeleccion = casillas[seleccion.X, seleccion.Y];  //actualizar la casilla de seleccion
                    if (casillaSeleccion.Ficha == null ||               //si la casilla seleccionada esta vacia
                        casillaSeleccion.Ficha.Lado != ladoActual) {    //o casilla seleccionada no es del color correspondiente
                        QuitarSeleccion();
                    }
                } else if (jugadas.Exists((j) => puntero.Equals(j))) { //si se apunta hacia una casilla que es una jugada
                    MoverPieza();   //mover la pieza seleecionada hacia la casilla apuntada
                }
            } else {    //se elige la pieza de coronacion
                casillas[posiciones[0].X, posiciones[0].Y * 2 - posiciones[1].Y].Ficha = casillas[puntero.X, puntero.Y].Ficha;
                casillas[posiciones[0].X, posiciones[0].Y].Ficha = piezasActuales[0];
                casillas[posiciones[1].X, posiciones[1].Y].Ficha = piezasActuales[1];
                casillas[posiciones[2].X, posiciones[2].Y].Ficha = piezasActuales[2];
                coronando = false;
            }

        }
        private int CalcularLadoActual() {
            return (registro.Count == 0 || registro.Last().fichaOrigen.Lado == 1) ? 0 : 1;

        }
        public void QuitarSeleccion() {
            seleccion = null;
            casillaSeleccion = null;
        }
        private void MoverPieza() { //se puede llamar a la funion copia
            MoverPieza(puntero, seleccion);
            QuitarSeleccion();
        }
        public void MoverPieza(Posicion fin, Posicion ori) {
            //GUARDAR JUGADA EN EL REGISTRO
            RegistroJugada registroJugada;
            Pieza origen = null;
            Pieza final = null;
            if (casillas[ori.X, ori.Y].Ficha != null)
                origen = casillas[ori.X, ori.Y].Ficha.Clone();
            if (casillas[fin.X, fin.Y].Ficha != null)
                final = casillas[fin.X, fin.Y].Ficha.Clone();
            registroJugada = new RegistroJugada(ori.Clone, fin.Clone,
                    origen,
                    final
                );
            registro.Add(registroJugada);
            //PEON PASADO
            if (registroJugada.fichaOrigen is Peon && registroJugada.fichaFin == null
                && registroJugada.o.X != registroJugada.f.X)    //si el peon esta comiendo un peon pasado
                casillas[registroJugada.f.X, registroJugada.o.Y].Ficha = null;  //quitar el peon comido
            //ENROQUE
            if (registroJugada.fichaOrigen is Rey && Math.Abs(registroJugada.o.X - registroJugada.f.X) == 2) { //si el rey se esta moviendo dos casillas a la derecha o izquierda
                if (registroJugada.o.X - registroJugada.f.X < 0) {  //DERECHA
                    casillas[registroJugada.f.X + 1, registroJugada.f.Y].Ficha = null;
                    casillas[registroJugada.f.X - 1, registroJugada.f.Y].Ficha = new Torre(tileset, registroJugada.fichaOrigen.Lado);
                } else {  ////IZQUIERDA
                    casillas[registroJugada.f.X - 2, registroJugada.f.Y].Ficha = null;
                    casillas[registroJugada.f.X + 1, registroJugada.f.Y].Ficha = new Torre(tileset, registroJugada.fichaOrigen.Lado);
                }
            }
            //MOVER
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
        private List<Posicion> jugadasPosibles(int lado) {
            List<Posicion> jugadas = new List<Posicion>();
            foreach (Casilla casilla in casillas)
                if (IsEnemy(casilla.Pos, -lado + 1))
                    jugadas.AddRange(casilla.Ficha.CalcularJugadas(this, casilla.Pos, true));
            return jugadas;
        }
        public bool IsAtacada(Posicion pos, int lado) {
            List<Posicion> jugadas = new List<Posicion>();
            foreach (Casilla casilla in casillas)
                if (IsEnemy(casilla.Pos, lado))
                    jugadas.AddRange(casilla.Ficha.CalcularJugadas(this, casilla.Pos, false));
            //coger todas las jugadas de las casillas enemigas y comprobar que el rey no esta en ellas
            return jugadas.Exists(j => j.Equals(pos));
        }
        public bool IsInJaque(int lado) {//sacar funcion
            return IsAtacada(GetRey(lado), lado);
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
                casillas[registroJugada.o.X, registroJugada.o.Y].Ficha = registroJugada.fichaOrigen;
                casillas[registroJugada.f.X, registroJugada.f.Y].Ficha = registroJugada.fichaFin;
                registro.RemoveAt(registro.Count - 1);
                //SI HUBO PEON PASADO
                if (registroJugada.fichaOrigen is Peon && registroJugada.fichaFin == null
                && registroJugada.o.X != registroJugada.f.X)
                    casillas[registroJugada.f.X, registroJugada.o.Y].Ficha = new Peon(tileset, registroJugada.fichaOrigen.Lado * (-1) + 1);
                //SI HUBO ENROQUE
                if (registroJugada.fichaOrigen is Rey && Math.Abs(registroJugada.o.X - registroJugada.f.X) == 2) {
                    //DERECHA
                    if (registroJugada.o.X - registroJugada.f.X < 0) {
                        casillas[registroJugada.f.X + 1, registroJugada.f.Y].Ficha = new Torre(tileset, registroJugada.fichaOrigen.Lado);
                        casillas[registroJugada.f.X - 1, registroJugada.f.Y].Ficha = null;
                    }
                    //IZQUIERDA
                    else {
                        casillas[registroJugada.f.X - 2, registroJugada.f.Y].Ficha = new Torre(tileset, registroJugada.fichaOrigen.Lado);
                        casillas[registroJugada.f.X + 1, registroJugada.f.Y].Ficha = null;
                    }
                }
            }
        }
        public bool IsPeon(Posicion pos) {
            return IsInside(pos) && casillas[pos.X, pos.Y].Ficha is Peon;
        }
        public bool IsLastJugada(Posicion origen, Posicion final) {
            RegistroJugada registroJugada = registro.Last();
            return registroJugada.o.Equals(origen) && registroJugada.f.Equals(final);
        }
        public bool WasMoved(Posicion pos) {
            return registro.Exists(r => r.o.Equals(pos));
        }
        public void Arriba() {
            if (!coronando && puntero.Y > 0
                || puntero.Y > 4 && ladoCoronacion == 1
                || puntero.Y > 0 && ladoCoronacion == 0)
                puntero.Y--;
        }
        public void Abajo() {
            if (!coronando && puntero.Y < 7
                || coronando && puntero.Y < 3 && ladoCoronacion == 0
                || coronando && puntero.Y < 7 && ladoCoronacion == 1)
                puntero.Y++;
        }
        public void Izquierda() {
            if (puntero.X > 0 && !coronando)
                puntero.X--;
        }
        public void Derecha() {
            if (puntero.X < 7 && !coronando)
                puntero.X++;
        }
    }
}
