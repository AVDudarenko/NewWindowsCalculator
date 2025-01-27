# Calculator and Dialog System in Unity

## Overview

This project demonstrates a modular approach to building a calculator with a dialog-based UI in Unity. The solution adheres to the principles of Clean Architecture and the MVP (Model-View-Presenter) pattern, ensuring separation of concerns, scalability, and testability.

---

## Features

1. **Modular Architecture**: The project is split into two primary modules:
    - **CalculatorModule**: Handles the core logic for arithmetic calculations and history management.
    - **DialogModule**: Manages the user interface, including input handling, error display, and history visualization.

2. **Clean Architecture**:
    - **Data Layer**: Responsible for data storage and retrieval (e.g., history management using `PlayerPrefs`).
    - **Domain Layer**: Contains business logic (e.g., expression validation and calculation).
    - **Presentation Layer**: Manages the user interface, including presenters and views.

3. **History Management**:
    - User calculations are saved and can be retrieved even after restarting the application.
    - The history is displayed dynamically in a scrollable view.

4. **Error Handling**:
    - Displays user-friendly error messages when invalid expressions are entered.
    - Allows users to correct errors and retry calculations seamlessly.

---

## Explanation of Key Components

### CalculatorModule
This module focuses on the logic of arithmetic calculations and history management:
- **PlayerPrefsHistoryRepository**:
    - Implements `IHistoryRepository` to manage history persistence.
    - Uses Unity's `PlayerPrefs` to store and retrieve data.
- **CalculatorUseCase**:
    - Validates arithmetic expressions.
    - Performs addition operations on valid inputs.
- **CalculationRecord**:
    - Represents a single calculation (expression and result).

### DialogModule
This module handles the UI and user interactions:
- **CalculatorDialogView**:
    - Manages user input, error messages, and history display.
    - Handles button actions like calculating results and clearing history.
- **CalculatorDialogPresenter**:
    - Acts as the intermediary between the view and the domain layer.
    - Ensures separation of logic and presentation.

---

## Technical Details

1. **Persistence**:
    - Calculation history and the last entered expression are stored using `PlayerPrefs`, ensuring the application state is preserved between sessions.

2. **Dynamic UI**:
    - The history is dynamically generated using prefabs instantiated within a scroll view.

3. **Event-Driven Architecture**:
    - The project leverages Unity's UI events to respond to user actions.