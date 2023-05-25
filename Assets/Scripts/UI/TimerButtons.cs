#nullable enable

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro.UI
{
    public sealed class TimerButtons : MonoBehaviour
    {
        private SimpleTimer _timer = null!;

        [SerializeField]
        private Button _startWorkButton = null!;

        [SerializeField]
        private Button _startRestButton = null!;

        [SerializeField]
        private Button _pauseButton = null!;

        [SerializeField]
        private Button _stopTimerButton = null!;

        [SerializeField]
        private TMP_Text _pauseButtonText = null!;

        [SerializeField]
        private string _pauseText = null!;

        [SerializeField]
        private string _resumeText = null!;

        private void Awake()
        {
            _startWorkButton.onClick.AddListener(OnStartWork);
            _startRestButton.onClick.AddListener(OnStartRest);
            _pauseButton.onClick.AddListener(OnPause);
            _stopTimerButton.onClick.AddListener(OnStop);
        }

        private void Start()
        {
            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnStateChanged += OnStateChanged;
            UpdateButtons();
        }

        private void OnStartWork()
            => _timer.StartWork();

        private void OnStartRest()
            => _timer.StartRest();

        private void OnStop()
            => _timer.Stop();

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