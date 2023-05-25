#nullable enable
using TMPro;
using UnityEngine;

namespace SimplePomodoro.UI
{
    public sealed class TimerText : MonoBehaviour
    {
        private SimpleTimer _timer = null!;

        [SerializeField]
        private TMP_Text _text = null!;

        private void Start()
        {
            _timer = AppStarter.Instance.SimpleTimer;
            UpdateText();
        }

        private void Update()
        {
            if (_timer.TimerState == TimerState.Stopped)
                return;
            UpdateText();
        }

        private void UpdateText()
            => _text.text = _timer.Time.ToString(@"hh\:mm\:ss");
    }
}