#nullable enable
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro.UI
{
    public sealed class TimerIcon : MonoBehaviour
    {
        private SimpleTimer _timer = null!;
        
        [SerializeField]
        private Image _icon = null!;

        [SerializeField]
        private Sprite _workIcon = null!;

        [SerializeField]
        private Sprite _restIcon = null!;

        private void Start()
        {
            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnStateChanged += OnStateChanged;
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            if (_timer.TimerState == TimerState.Stopped)
            {
                _icon.sprite = null;
                return;
            }

            _icon.sprite = _timer.IsWork ? _workIcon : _restIcon;
        }
    }
}