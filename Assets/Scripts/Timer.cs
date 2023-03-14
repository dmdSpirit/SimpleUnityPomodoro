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
        private bool _isRunning;

        [SerializeField]
        private int _minutes;

        [SerializeField]
        private Button _startTimerButton = null!;

        [SerializeField]
        private Button _stopTimerButton = null!;

        [SerializeField]
        private TMP_Text _text = null!;

        private void Awake()
        {
            _startTimerButton.onClick.AddListener(OnStartTimer);
            _stopTimerButton.onClick.AddListener(OnStopTimer);
        }

        private void Start()
        {
            UpdateText();
        }

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            float secondsLeft = Mathf.Max(0f, (float)(_time.TotalSeconds - Time.deltaTime));
            _time = TimeSpan.FromSeconds(secondsLeft);
            UpdateText();
        }

        private void OnStartTimer()
        {
            _time = TimeSpan.FromMinutes(_minutes);
            _isRunning = true;
            UpdateText();
        }

        private void OnStopTimer()
        {
            _isRunning = false;
        }

        private void UpdateText()
        {
            _text.text = _time.ToString(@"hh\:mm\:ss");
        }
    }
}