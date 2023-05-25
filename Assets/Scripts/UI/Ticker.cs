#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace SimplePomodoro.UI
{
    public sealed class Ticker : MonoBehaviour
    {
        private readonly List<ITickable> _tickables = new();

        private void Update()
        {
            foreach (ITickable tickable in _tickables)
                tickable.Tick();
        }

        public void Register(ITickable tickable)
            => _tickables.Add(tickable);
    }
}