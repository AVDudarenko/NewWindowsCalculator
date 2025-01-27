using TMPro;
using UnityEngine;

/*
* Represents a single item in the calculation history UI.
* Responsible for displaying a record of a calculation.
*/ </summary>
public class HistoryItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    public void SetData(CalculationRecord record)
    {
        _textField.text = $"{record.Expression} = {record.Result}";
    }
}