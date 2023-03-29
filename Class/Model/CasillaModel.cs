using AjedrezMonogame.Class.Model.Piezas;
using System;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Model {
    internal class CasillaModel : IObservable<CasillaModel> {
        private List<IObserver<CasillaModel>> observers;
        public Posicion Pos { get; set; }
        public bool Puntero { get; set; }
        public bool Jugada { get; set; }
        public bool Seleccion { get; set; }
        public Pieza Ficha { get; set; }

        public CasillaModel(int x, int y, bool puntero = false) {
            Puntero = puntero;
            Jugada = false;
            Pos = new Posicion(x, y);
            observers = new List<IObserver<CasillaModel>>();
        }
        public void ActualizarObservadores() {
            observers.ForEach(o => o.OnNext(this));
        }
        public IDisposable Subscribe(IObserver<CasillaModel> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber<CasillaModel>(observers, observer);
        }
    }
}
