namespace JMT.Core.Tool.PoolManager.Core
{
    public class CodeFormat
    {
        public static string PoolingTypeFormat =
            @"namespace JMT.Core.Tool.PoolManager
{{
    public enum PoolingType
    {{
        {0}
    }}
}}";

        public static string EnemyTypeFormat =
            @"public enum EnemyType
{{
{0}
}}";
    }
}