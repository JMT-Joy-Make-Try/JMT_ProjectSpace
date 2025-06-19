using JMT.DialogueSystem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace JMT.UISystem.Dialogue
{
    public struct DialogueData
    {
        public string name;
        public string desc;

        public DialogueData(string name, string desc)
        {
            this.name = name;
            this.desc = desc;
        }
    }
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
        // 스트링 필터링해야딤

        public Queue<DialogueData> SettingDialogueData(string data)
        {
            Queue<DialogueData> result = new();
            // StringSplitOptions.RemoveEmptyEntries : 비어있는 문자열 항목 제거
            string[] splitEnterData = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach(string str in splitEnterData)
            {
                string[] splitTabData = str.Split('\t');
                result.Enqueue(new(splitTabData[0], splitTabData[1]));
            }
            return result;
        }
    }
}
