using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public class PlayerPrefsHistoryRepository : IHistoryRepository
    {
        private const string KEY = "calc_history";

        public void SaveRecord(CalculationRecord record)
        {
            string newRecord = $"{record.Expression}={record.Result}";
            string existingData = PlayerPrefs.GetString(KEY, "");

            if (string.IsNullOrEmpty(existingData))
            {
                PlayerPrefs.SetString(KEY, newRecord);
            }
            else
            {
                string updateData = $"{existingData};{newRecord}";
                PlayerPrefs.SetString(KEY, updateData);
            }

            PlayerPrefs.Save();
        }

        public List<CalculationRecord> GetAllRecords()
        {
            string dataString = PlayerPrefs.GetString(KEY, "");
            if (string.IsNullOrEmpty(dataString))
                return new List<CalculationRecord>();

            string[] parts = dataString.Split(';')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();

            var result = new List<CalculationRecord>();
            foreach (var part in parts)
            {
                string[] exprRes = part.Split('=');
                if (exprRes.Length != 2) continue;

                string expression = exprRes[0];
                string value = exprRes[1];

                result.Add(new CalculationRecord(expression, value));
            }

            return result;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(KEY);
            PlayerPrefs.Save();
        }

        private void SaveAll(List<CalculationRecord> records)
        {
            // Формат: "12+5=17;98.12+48.1=ERROR;..."
            var joined = string.Join(";", records.Select(r => $"{r.Expression}={r.Result}"));
            PlayerPrefs.SetString(KEY, joined);
            PlayerPrefs.Save();
        }
    }
}