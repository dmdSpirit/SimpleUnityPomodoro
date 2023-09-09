#nullable enable
using SimplePomodoro.Data;
using SimplePomodoro.DeepWork;
using SimplePomodoro.UI;
using UnityEngine;

namespace SimplePomodoro
{
    [RequireComponent(typeof(Ticker))]
    public sealed class AppStarter : MonoBehaviour
    {
        private static AppStarter? _instance;

        private Ticker _ticker = null!;

        [SerializeField]
        private TimerSettings _timerSettings = null!;

        [SerializeField]
        private PanelsSwitcher _panelsSwitcher = null!;

        public static AppStarter Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                AppStarter[]? instances = FindObjectsOfType<AppStarter>();
                if (instances.Length == 0)
                {
                    _instance = new GameObject().AddComponent<AppStarter>();
                    return _instance;
                }

                if (instances.Length > 1)
                    Debug.LogError($"Multiple instances of singleton {nameof(AppStarter)}");

                _instance = instances[0];
                return _instance;
            }
        }

        public SaveController SaveController { get; private set; } = null!;
        public DataToFileWriter DataToFileWriter { get; private set; } = null!;
        public SimpleTimer SimpleTimer { get; private set; } = null!;

        public TimerSettings TimerSettings => _timerSettings;

        public void ShowPomodoro()
            => _panelsSwitcher.ShowPomodoro();
        
        public void ShowDeepWork()
            => _panelsSwitcher.ShowDeepWork();

        private void Awake()
            => Initialize();

        private void Start()
            => StartApp();

        private void Initialize()
        {
            _ticker = GetComponent<Ticker>();
            DataToFileWriter = new DataToFileWriter();
            SimpleTimer = new SimpleTimer(_timerSettings);
            SaveController = new SaveController(DataToFileWriter, SimpleTimer);
            _ticker.Register(SimpleTimer);
        }

        private void StartApp()
        {
            SaveController.Load();
            _panelsSwitcher.ShowPomodoro();
        }
    }
}