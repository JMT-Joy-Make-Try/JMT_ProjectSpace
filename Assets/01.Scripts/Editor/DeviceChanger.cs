using UnityEditor;

namespace JMT.Editor
{
    public static class DeviceChanger
    {
        [MenuItem("JMT/Build Settings/Change Device/Android")]
        public static void ChangeUnityRemoteDeviceToAndroid()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
        }
        
        [MenuItem("JMT/Build Settings/Change Device/Windows")]
        public static void ChangeUnityRemoteDeviceToWindows()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
        }
    }
}