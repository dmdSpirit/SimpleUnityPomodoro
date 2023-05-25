#nullable enable
using UnityEngine;

namespace SimplePomodoro.UI
{
    [RequireComponent(typeof(TimerButtons))]
    public sealed class OnCloseSaver : MonoBehaviour
    {
        private TimerButtons _timerButtons = null!;

        private void Awake()
        {
            _timerButtons = GetComponent<TimerButtons>();
        }

        private void OnDestroy()
        {
            // Save
        }
    }
}