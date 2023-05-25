#nullable enable
using UnityEngine;

namespace SimplePomodoro.UI
{
    public sealed class AlarmPlayer : MonoBehaviour
    {
        private SimpleTimer _timer = null!;
        
        [SerializeField]
        private AudioSource _alarmSource = null!;

        private void Start()
        {
            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnTimerFinished += OnTimerFinished;
        }

        private void OnTimerFinished()
            => _alarmSource.Play();
    }
}