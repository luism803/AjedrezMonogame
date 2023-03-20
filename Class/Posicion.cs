namespace Pong.Class {
    public class Posicion {
        public Posicion Clone {
            get { return new Posicion(X, Y); }
        }
        public int X { set; get; }
        public int Y { set; get; }
        public Posicion(int x, int y) {
            X = x;
            Y = y;
        }
        public Posicion() {
            X = 0;
            Y = 0;
        }
        public bool Equals(Posicion other) {
            return other != null && other.X == X && other.Y == Y;
        }
        public void Set(Posicion newPos) {
            X = newPos.X;
            Y = newPos.Y;
        }
        public void Set(int x, int y) {
            X = x;
            Y = y;
        }
        public void Arriba() {
            if (Y > 0)
                Y--;
        }
        public void Abajo() {
            if (Y < 7)
                Y++;
        }
        public void Izquierda() {
            if (X > 0)
                X--;
        }
        public void Derecha() {
            if (X < 7)
                X++;
        }
    }
}
