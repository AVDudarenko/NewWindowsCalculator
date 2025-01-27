using Data;
using Domain.UseCases;
using Presentation.Views;
using System;
using UnityEngine;

namespace Presentation.Presenters
{
    /*
    * The presenter responsible for handling the interaction between the Calculator Dialog view and business logic.
    * Manages user input, validation, and updates to the view.
    */
    public class CalculatorDialogPresenter
    {
        private readonly ICalculatorDialogView _view;
        private readonly CalculatorUseCase _useCase;
        private readonly IHistoryRepository _history;

        public CalculatorDialogPresenter(ICalculatorDialogView view)
        {
            _view = view;
            _useCase = new CalculatorUseCase();
            _history = new PlayerPrefsHistoryRepository();

            // Display all history upon initializing the view.
            ShowAllHistory();
        }

        public void OnCalculateClicked(string expression)
        {
            string result;

            if (!_useCase.ValidateExpression(expression))
            {
                result = "ERROR";
                _view.ShowError("Please check the expression you just entered");
            }
            else
            {
                try
                {
                    int calculationResult = _useCase.Calculate(expression);
                    result = calculationResult.ToString();
                }
                catch (Exception ex)
                {
                    result = "ERROR";
                    Debug.LogError($"Calculation failed: {ex.Message}");
                }
            }

            // Save the result to the history.
            var record = new CalculationRecord(expression, result);
            _history.SaveRecord(record);

            // Update the UI with the new history.
            ShowAllHistory();

            // Clear the input field.
            _view.ClearInput();
        }

        public void OnClearHistoryClicked()
        {
            _history.Clear();
            ShowAllHistory();
        }

        private void ShowAllHistory()
        {
            var list = _history.GetAllRecords();
            _view.ShowHistory(list);
        }
    }
}