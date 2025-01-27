using System.Collections.Generic;

namespace Presentation.Views
{
    public interface ICalculatorDialogView
    {
        void ShowError(string errorMessage);
        void ShowHistory(List<CalculationRecord> records);
        void ClearInput();
    }
}