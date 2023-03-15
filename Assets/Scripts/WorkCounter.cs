#nullable enable
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimplePomodoro
{
    public class WorkCounter : MonoBehaviour
    {
        private int _workCount;

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
            UpdateText();
        }

        public void AddCount()
        {
            _workCount++;
            UpdateText();
        }

        public void SubtractCount()
        {
            _workCount--;
            UpdateText();
        }

        private void UpdateText()
        {
            _workCountText.text = _workCount.ToString();
        }
    }
}