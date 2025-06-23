using UnityEngine;

namespace JMT.DialogueSystem
{
    public static class ReadSheetSystem
    {
        public static string GetTSVAddress(string address, string range, long sheetID)
            => $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }
}
