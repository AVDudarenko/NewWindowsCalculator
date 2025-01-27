using Presentation.Presenters;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation.Views
{
    /*
    * The view responsible for displaying and managing the calculator dialog UI.
    * Handles user interaction and delegates logic to the presenter.
    */
    public class CalculatorDialogView : MonoBehaviour, ICalculatorDialogView
    {
        [SerializeField] private TMP_InputField _expressionInput;
        [SerializeField] private TMP_Text _errorText;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _resultButton;
        [SerializeField] private Button _clearHistoryButton;
        [SerializeField] private RectTransform _contentContainer;
        [SerializeField] private HistoryItemView _historyItemPrefab;
        [SerializeField] private GameObject _scrollView;
        [SerializeField] private Image _underline;
        [SerializeField] private Color _activeColor = new Color(0.22f, 0.52f, 0.93f, 1f);
        [SerializeField] private Color _inactiveColor = Color.gray;

        private CalculatorDialogPresenter _presenter;
        private CalculatorState _state = new CalculatorState();
        private List<HistoryItemView> _historyItemPool = new List<HistoryItemView>();

        private void Start()
        {
            if (_expressionInput == null || _errorText == null || _resultButton == null ||
                _clearHistoryButton == null || _contentContainer == null ||
                _historyItemPrefab == null || _scrollView == null)
            {
                Debug.LogError("Some UI fields are not assigned in the inspector!");
                return;
            }
            
            _presenter = new CalculatorDialogPresenter(this);

            _resultButton.onClick.AddListener(OnCalculateButtonClicked);
            _clearHistoryButton.onClick.AddListener(() =>
                _presenter.OnClearHistoryClicked());

            _expressionInput.onSelect.AddListener((text) => SetUnderlineActive(true));
            _expressionInput.onDeselect.AddListener((text) => SetUnderlineActive(false));

            SetUnderlineActive(false);
            _errorText.gameObject.SetActive(false);

            LoadState();
        }

        private void OnCalculateButtonClicked()
        {
            if (_state.IsErrorState)
            {
                ResetUI();
            }
            else
            {
                _state.LastExpression = _expressionInput.text;
                _presenter.OnCalculateClicked(_state.LastExpression);
            }
        }

        public void ShowError(string errorMessage)
        {
            _state.IsErrorState = true;

            _expressionInput.gameObject.SetActive(false);
            _contentContainer.gameObject.SetActive(false);
            _clearHistoryButton.gameObject.SetActive(false);
            _scrollView.SetActive(false);

            _errorText.text = errorMessage;
            _errorText.gameObject.SetActive(true);

            _titleText.gameObject.SetActive(false);

            _resultButton.GetComponentInChildren<TMP_Text>().text = "GOT IT";
        }

        private void ResetUI()
        {
            _state.IsErrorState = false;

            _expressionInput.gameObject.SetActive(true);
            _contentContainer.gameObject.SetActive(true);
            _clearHistoryButton.gameObject.SetActive(true);
            _scrollView.SetActive(true);

            _errorText.gameObject.SetActive(false);
            _titleText.gameObject.SetActive(true);
            _resultButton.GetComponentInChildren<TMP_Text>().text = "Calculate";

            _expressionInput.text = _state.LastExpression;
        }

        public void ShowHistory(List<CalculationRecord> records)
        {
            // Hide existing items
            for (int i = 0; i < _historyItemPool.Count; i++)
            {
                if (_historyItemPool[i] != null)
                {
                    _historyItemPool[i].gameObject.SetActive(false);
                }
            }

            if (records == null || records.Count == 0)
            {
                _errorText.text = "History is empty.";
                return;
            }

            for (int i = 0; i < records.Count; i++)
            {
                HistoryItemView itemView;

                if (i < _historyItemPool.Count)
                {
                    itemView = _historyItemPool[i];

                    if (itemView == null)
                    {
                        var itemObj = Instantiate(_historyItemPrefab, _contentContainer);
                        itemView = itemObj;
                        _historyItemPool[i] = itemView;
                    }
                }
                else
                {
                    var itemObj = Instantiate(_historyItemPrefab, _contentContainer);
                    itemView = itemObj;
                    _historyItemPool.Add(itemView);
                }

                itemView.SetData(records[i]);
                itemView.gameObject.SetActive(true);
            }

            for (int i = records.Count; i < _historyItemPool.Count; i++)
            {
                if (_historyItemPool[i] != null)
                {
                    _historyItemPool[i].gameObject.SetActive(false);
                }
            }
        }

        public void ClearInput()
        {
            _expressionInput.text = "";
        }

        private void OnApplicationQuit()
        {
            try
            {
                SaveState();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while saving state: {ex.Message}");
            }
        }

        private void SaveState()
        {
            PlayerPrefs.SetString("LastExpression", _state.LastExpression);
            PlayerPrefs.SetInt("IsErrorState", _state.IsErrorState ? 1 : 0);
            PlayerPrefs.Save();
            Debug.Log("State saved.");
        }

        private void LoadState()
        {
            _state.LastExpression = PlayerPrefs.GetString("LastExpression", "");
            _state.IsErrorState = PlayerPrefs.GetInt("IsErrorState", 0) == 1;

            _expressionInput.text = _state.LastExpression;

            if (_state.IsErrorState)
            {
                ShowError("Please check the expression you just entered");
            }

            Debug.Log("State loaded.");
        }

        private void SetUnderlineActive(bool isActive)
        {
            if (_underline != null)
            {
                _underline.color = isActive ? _activeColor : _inactiveColor;
            }
        }
    }
}