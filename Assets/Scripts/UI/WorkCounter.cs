#nullable enable

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro.UI
{
    public class WorkCounter : MonoBehaviour
    {
        private SimpleTimer _timer = null!;

        [SerializeField]
        private TMP_Text _workCountText = null!;

        [SerializeField]
        private Button _addButton = null!;

        [SerializeField]
        private Button _subtractButton = null!;

        private void Awake()
        {
            _addButton.onClick.AddListener(AddCount);
            _subtractButton.onClick.AddListener(SubtractCount);
        }

        private void Start()
        {
            _timer = AppStarter.Instance.SimpleTimer;
            _timer.OnTimerFinished += UpdateText;
            _timer.OnDataLoaded += UpdateText;
            UpdateText();
        }

        private void AddCount()
        {
            _timer.AddWorkCount();
            UpdateText();
        }

        private void SubtractCount()
        {
            _timer.SubtractWorkCount();
            UpdateText();
        }

        private void UpdateText()
            => _workCountText.text = _timer.WorkCounter.ToString();
    }
}