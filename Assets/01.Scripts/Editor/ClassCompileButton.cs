using UnityEditor;

namespace JMT.Editor
{
    public class ClassCompileButton
    {
        [MenuItem("JMT/Compile All Classes %g")]
        public static void CompileAllClasses()
        {
            UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
            UnityEngine.Debug.Log("All classes compiled successfully.");
        }
    }
}