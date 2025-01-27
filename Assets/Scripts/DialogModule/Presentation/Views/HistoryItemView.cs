using TMPro;
using UnityEngine;

public class HistoryItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    public void SetData(CalculationRecord record)
    {
        _textField.text = $"{record.Expression} = {record.Result}";
    }
}