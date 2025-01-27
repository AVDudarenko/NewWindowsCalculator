using System.Collections.Generic;

namespace Presentation.Views
{
    /*
    * Represents the interface for the calculator dialog view.
    * Provides methods for displaying errors, showing history, and clearing input fields.
    */
    public interface ICalculatorDialogView
    {
        void ShowError(string errorMessage);
        void ShowHistory(List<CalculationRecord> records);
        void ClearInput();
    }
}