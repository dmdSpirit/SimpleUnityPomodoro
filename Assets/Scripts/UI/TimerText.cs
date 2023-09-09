#nullable enable
using TMPro;
using UnityEngine;

namespace SimplePomodoro.UI
{
    public sealed class TimerText : MonoBehaviour
    {
        private SimpleTimer _timer = null!;
        private bool _isShown;

        [SerializeField]
        private TMP_Text _text = null!;

        public void Show()
        {
            if (_isShown)
                return;

            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnStateChanged += UpdateText;
            UpdateText();
            _isShown = true;
        }

        public void Hide()
        {
            if (_timer != null!)
                _timer.OnStateChanged -= UpdateText;
            _isShown = false;
        }

        private void Update()
        {
            if (!_isShown)
                return;
            if (_timer.TimerState == TimerState.Stopped)
                return;
            UpdateText();
        }

        private void UpdateText()
            => _text.text = _timer.Time.ToString(@"hh\:mm\:ss");
    }
}