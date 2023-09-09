#nullable enable

using SimplePomodoro.UI;
using UnityEngine;

namespace SimplePomodoro.DeepWork
{
    public sealed class PanelsSwitcher : MonoBehaviour
    {
        [SerializeField]
        private DeepWorker _deepWorker = null!;

        [SerializeField]
        private PomodoroTimer _pomodoroTimer = null!;

        public void ShowPomodoro()
        {
            _deepWorker.Hide();
            _pomodoroTimer.Show();
        }

        public void ShowDeepWork()
        {
            _pomodoroTimer.Hide();
            _deepWorker.Show();
        }
    }
}