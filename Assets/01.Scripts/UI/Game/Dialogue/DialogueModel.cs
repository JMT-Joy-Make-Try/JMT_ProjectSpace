using JMT.DialogueSystem;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace JMT.UISystem.Dialogue
{
    public class DialogueModel
    {
        private readonly string address = "https://docs.google.com/spreadsheets/d/1xFX13o37b1zgGuKPkc6bBW8uTjuZ8H_zj1usoMJypAo";
        private readonly long sheetID = 0;

        public async Task<string> LoadDataAsync(string range)
        {
            // TSV 파일로 변환
            var TSVdata = ReadSheetSystem.GetTSVAddress(address, range, sheetID);
            UnityWebRequest www = UnityWebRequest.Get(TSVdata);

            await www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
                return www.downloadHandler.text;

            return string.Empty;
        }
    }
}
