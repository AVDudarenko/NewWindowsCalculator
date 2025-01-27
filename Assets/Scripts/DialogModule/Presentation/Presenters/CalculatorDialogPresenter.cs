using Data;
using Domain.UseCases;
using Presentation.Views;
using System;
using UnityEngine;

namespace Presentation.Presenters
{
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

            // При создании вьюхи сразу показать историю, если уже есть
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


            // Сохраняем в историю
            var record = new CalculationRecord(expression, result);
            _history.SaveRecord(record);

            // Обновим UI
            ShowAllHistory();

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