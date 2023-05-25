#nullable enable
using UnityEngine;

namespace SimplePomodoro.UI
{
    public sealed class OnCloseSaver : MonoBehaviour
    {
        private void OnDestroy()
            => AppStarter.Instance.SaveController.Save();
    }
}