#nullable enable
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro
{
    public class Timer : MonoBehaviour
    {
        private TimeSpan _time;
        private TimerState _timerState;

        [SerializeField]
        private int _workDuration;

        [SerializeField]
        private int _restDuration;

        [SerializeField]
        private Image _icon = null!;

        [SerializeField]
        private Sprite _workIcon = null!;

        [SerializeField]
        private Sprite _restIcon = null!;

        [SerializeField]
        private Button _startWorkButton = null!;

        [SerializeField]
        private Button _startRestButton = null!;

        [SerializeField]
        private Button _pauseButton = null!;

        [SerializeField]
        private Button _stopTimerButton = null!;

        [SerializeField]
        private TMP_Text _text = null!;

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
            _icon.sprite = null;
            _pauseButton.interactable = false;
            _pauseButtonText.text = _pauseText;
            UpdateText();
        }

        private void Update()
        {
            if (_timerState != TimerState.Playing)
            {
                return;
            }

            float secondsLeft = Mathf.Max(0f, (float)(_time.TotalSeconds - Time.deltaTime));
            _time = TimeSpan.FromSeconds(secondsLeft);
            UpdateText();
        }

        private void OnStartWork()
        {
            _timerState = TimerState.Playing;
            _icon.sprite = _workIcon;
            _time = TimeSpan.FromMinutes(_workDuration);
            _pauseButton.interactable = true;
            _pauseButtonText.text = _pauseText;
            UpdateText();
        }

        private void OnStartRest()
        {
            _timerState = TimerState.Playing;
            _icon.sprite = _restIcon;
            _time = TimeSpan.FromMinutes(_restDuration);
            _pauseButton.interactable = true;
            _pauseButtonText.text = _pauseText;
            UpdateText();
        }

        private void OnStop()
        {
            _timerState = TimerState.Stopped;
            _time = TimeSpan.Zero;
            UpdateText();
            _icon.sprite = null;
            _pauseButton.interactable = false;
            _pauseButtonText.text = _pauseText;
        }

        private void OnPause()
        {
            switch (_timerState)
            {
                case TimerState.Stopped:
                    break;
                case TimerState.Paused:
                    _timerState = TimerState.Playing;
                    _pauseButtonText.text = _pauseText;
                    break;
                case TimerState.Playing:
                    _timerState = TimerState.Paused;
                    _pauseButtonText.text = _resumeText;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateText()
        {
            _text.text = _time.ToString(@"hh\:mm\:ss");
        }
    }
}