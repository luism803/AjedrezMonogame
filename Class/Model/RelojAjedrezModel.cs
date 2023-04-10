using System;
using System.Collections.Generic;

namespace AjedrezMonogame.Class.Model {
    internal class RelojAjedrezModel : IObservable<RelojAjedrezModel> {
        List<IObserver<RelojAjedrezModel>> observers;
        private float[] tiempoJugadores;
        public float[] GetTiempoJugadores() { return tiempoJugadores; }
        public RelojAjedrezModel(float tiempo) {
            observers = new List<IObserver<RelojAjedrezModel>>();
            tiempoJugadores = new float[2];
            tiempoJugadores[0] = tiempo;
            tiempoJugadores[1] = tiempo;
        }
        public void restarTiempo(float tiempoTranscurrido, int lado) {
            tiempoJugadores[lado] -= tiempoTranscurrido;
        }
        public void ActualizarObservadores() {
            observers.ForEach(o => o.OnNext(this));
        }
        public IDisposable Subscribe(IObserver<RelojAjedrezModel> observer) {
            if (!observers.Contains(observer)) {
                observers.Add(observer);
            }
            return new Unsubscriber<RelojAjedrezModel>(observers, observer);
        }
    }
}
