using System.Runtime.CompilerServices;

namespace JMT.Core.Tool
{
    public static class DebugExtension
    {
        public static void LogWithClassName(this object message, [CallerFilePath] string filePath = "",
                                             [CallerLineNumber] int lineNumber = 0,
                                             [CallerMemberName] string memberName = "")
        {
            string className = message.GetType().Name;
            UnityEngine.Debug.Log($"[{className}] {message} (File: {filePath}, Line: {lineNumber}, Member: {memberName})");
        }
    }
}