#nullable enable

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro.UI
{
    public sealed class PomodoroTimer : MonoBehaviour
    {
        private SimpleTimer _timer = null!;
        private bool _isShown;

        [SerializeField]
        private Button _startWorkButton = null!;

        [SerializeField]
        private Button _startRestButton = null!;

        [SerializeField]
        private Button _pauseButton = null!;

        [SerializeField]
        private Button _stopTimerButton = null!;

        [SerializeField]
        private Button _showDeepWork = null!;

        [SerializeField]
        private TMP_Text _pauseButtonText = null!;

        [SerializeField]
        private string _pauseText = null!;

        [SerializeField]
        private string _resumeText = null!;
        
        [SerializeField]
        private TimerText _timerText = null!;

        private void Awake()
        {
            _startWorkButton.onClick.AddListener(OnStartWork);
            _startRestButton.onClick.AddListener(OnStartRest);
            _pauseButton.onClick.AddListener(OnPause);
            _stopTimerButton.onClick.AddListener(OnStop);
            _showDeepWork.onClick.AddListener(OnShowDeepWork);
        }

        public void Show()
        {
            if (_isShown)
                return;
            gameObject.SetActive(true);
            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnStateChanged += OnStateChanged;
            UpdateButtons();
            _timerText.Show();
            _isShown = true;
        }

        public void Hide()
        {
            if (_timer != null!)
            {
                _timer.Stop();
                _timer.OnStateChanged -= OnStateChanged;
            }
            _timerText.Hide();
            gameObject.SetActive(false);
            _isShown = false;
        }

        private void OnStartWork()
            => _timer.StartWork();

        private void OnStartRest()
            => _timer.StartRest();

        private void OnStop()
            => _timer.Stop();

        private void OnPause()
            => _timer.Pause();

        private void OnShowDeepWork()
            => AppStarter.Instance.ShowDeepWork();

        private void UpdateButtons()
        {
            switch (_timer.TimerState)
            {
                case TimerState.Stopped:
                    _pauseButtonText.text = _pauseText;
                    _pauseButton.interactable = false;
                    _stopTimerButton.interactable = false;
                    break;
                case TimerState.Paused:
                    _pauseButtonText.text = _resumeText;
                    _pauseButton.interactable = true;
                    _stopTimerButton.interactable = true;
                    break;
                case TimerState.Playing:
                    _pauseButtonText.text = _pauseText;
                    _pauseButton.interactable = true;
                    _stopTimerButton.interactable = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnStateChanged()
            => UpdateButtons();
    }
}