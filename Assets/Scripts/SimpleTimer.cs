#nullable enable

using System;
using UnityEngine;

namespace SimplePomodoro
{
    public sealed class SimpleTimer : ITickable
    {
        private readonly TimerSettings _settings;

        public event Action? OnTimerFinished;
        public event Action? OnStateChanged;

        public TimerState TimerState { get; private set; }
        public bool IsWork { get; private set; }
        public TimeSpan Time { get; private set; }
        public int WorkCounter { get; private set; }

        public SimpleTimer(TimerSettings timerSettings)
        {
            _settings = timerSettings;
        }

        public void Tick()
        {
            if (TimerState != TimerState.Playing)
                return;

            float secondsLeft = Mathf.Max(0f, (float)(Time.TotalSeconds - UnityEngine.Time.deltaTime));
            if (secondsLeft == 0)
            {
                TimerFinished();
                return;
            }

            Time = TimeSpan.FromSeconds(secondsLeft);
        }

        public void AddWorkCount()
            => WorkCounter++;

        public void SubtractWorkCount()
        {
            if (WorkCounter == 0)
                return;
            WorkCounter--;
        }

        public void StartWork()
        {
            TimerState = TimerState.Playing;
            Time = TimeSpan.FromMinutes(_settings.WorkDuration);
            IsWork = true;
            OnStateChanged?.Invoke();
        }

        public void StartRest()
        {
            TimerState = TimerState.Playing;
            Time = TimeSpan.FromMinutes(_settings.RestDuration);
            IsWork = false;
            OnStateChanged?.Invoke();
        }

        public void Stop()
        {
            TimerState = TimerState.Stopped;
            Time = TimeSpan.Zero;
            OnStateChanged?.Invoke();
        }

        public void Pause()
        {
            switch (TimerState)
            {
                case TimerState.Stopped:
                    break;
                case TimerState.Paused:
                    TimerState = TimerState.Playing;
                    break;
                case TimerState.Playing:
                    TimerState = TimerState.Paused;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            OnStateChanged?.Invoke();
        }

        private void TimerFinished()
        {
            if (IsWork)
                WorkCounter++;

            Stop();
            OnTimerFinished?.Invoke();
        }
    }
}