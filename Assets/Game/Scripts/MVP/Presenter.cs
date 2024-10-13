using System;
using System.Collections.Generic;

namespace MVP
{
    public abstract class Presenter
    {
        protected event Action DisposeEvent;
        private readonly HashSet<Presenter> _subPresenters = new();
        private Presenter _parent;


        protected abstract void Init();

        protected virtual void Dispose()
        {
            _parent?._subPresenters.Remove(this);
            foreach (var child in _subPresenters)
            {
                child.Dispose();
            }
            DisposeEvent?.Invoke();
            DisposeEvent = null;
        }

        protected T OpenPresenter<T>(Action disposeCallback) where T : Presenter, new()
        {
            var child = OpenPresenter<T>();
            child.DisposeEvent += disposeCallback;
            return child;
        }

        protected T OpenPresenter<T>() where T : Presenter, new()
        {
            var child = new T();
            _subPresenters.Add(child);
            child!.Init();
            return child;
        }
    }
}