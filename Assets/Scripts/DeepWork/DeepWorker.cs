#nullable enable

using System;
using SimplePomodoro.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro.DeepWork
{
    public sealed class DeepWorker : MonoBehaviour
    {
        private SimpleTimer _timer = null!;
        private IDisposable? _sub;
        private bool _isShown;

        [SerializeField]
        private Button _startWorkButton = null!;

        [SerializeField]
        private Button _pauseButton = null!;

        [SerializeField]
        private Button _stopTimerButton = null!;

        [SerializeField]
        private Button _showPomodoro = null!;

        [SerializeField]
        private TMP_Text _pauseButtonText = null!;

        [SerializeField]
        private TMP_InputField _sessionLength = null!;

        [SerializeField]
        private string _pauseText = null!;

        [SerializeField]
        private string _resumeText = null!;

        [SerializeField]
        private TimerText _timerText = null!;

        private void Awake()
        {
            _startWorkButton.onClick.AddListener(OnStartWork);
            _pauseButton.onClick.AddListener(OnPause);
            _stopTimerButton.onClick.AddListener(OnStop);
            _showPomodoro.onClick.AddListener(OnShowPomodoro);
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
        {
            if (!int.TryParse(_sessionLength.text, out var session))
                return;
            _timer.StartWork(session);
        }

        private void OnStop()
            => _timer.Stop();

        private void OnShowPomodoro()
            => AppStarter.Instance.ShowPomodoro();

        private void OnPause()
            => _timer.Pause();

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