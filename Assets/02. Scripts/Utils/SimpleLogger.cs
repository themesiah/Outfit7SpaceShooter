using UnityEngine;

namespace SpaceShooter.Utils
{
    public class SimpleLogger : MonoBehaviour
    {
        [System.Flags]
        public enum LogContext
        {
            PlayerController = (1 << 0)
        }

        [System.Flags]
        public enum LogLevel
        {
            Log = (1 << 0),
            Warning = (1 << 1),
            Error = (1 << 2)
        }

        [SerializeField] [EnumMask]
        private LogContext showContexts = default;

        [SerializeField] [EnumMask]
        private LogLevel showLogLevels = default;

        public static SimpleLogger Instance = null;

        private void Awake()
        {
            Instance = this;
        }

        public void Log(LogContext context, string message, params object[] args)
        {
            if (ShouldShow(context, LogLevel.Log))
            {
                Debug.LogFormat(message, args);
            }
        }

        public void Warning(LogContext context, string message, params object[] args)
        {
            if (ShouldShow(context, LogLevel.Warning))
            {
                Debug.LogWarningFormat(message, args);
            }
        }

        public void Error(LogContext context, string message, params object[] args)
        {
            if (ShouldShow(context, LogLevel.Error))
            {
                Debug.LogErrorFormat(message, args);
            }
        }

        private bool ShouldShow(LogContext context, LogLevel logLevel)
        {
            return showLogLevels.HasFlag(logLevel) && showContexts.HasFlag(context);
        }
    }
}