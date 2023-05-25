#nullable enable
using UnityEngine;

namespace SimplePomodoro
{
    [CreateAssetMenu(menuName = "Create TimerSettings", fileName = "TimerSettings", order = 0)]
    public sealed class TimerSettings : ScriptableObject
    {
        [SerializeField]
        private float _workDuration = 40;

        [SerializeField]
        private float _restDuration = 5;

        public float WorkDuration => _workDuration;
        public float RestDuration => _restDuration;
    }
}