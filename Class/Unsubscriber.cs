﻿using System;
using System.Collections.Generic;

namespace AjedrezMonogame.Class {
    internal class Unsubscriber<T> : IDisposable {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer) {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
