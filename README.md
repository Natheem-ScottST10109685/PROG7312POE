# ST10109685Prog7312POE

A Windows Forms application for managing local events and reporting issues, built with C# targeting .NET Framework 4.8.

## Features

- **Event Management**
  - Add, view, update, and remove local events.
  - Event data managed via `EventManager` and `LocalEvent` classes.
- **Issue Reporting**
  - Report, view, update, and remove issues.
  - Issues managed via `IssueManager` and `ReportedIssue` classes.
- **User Interface**
  - Main form (`Form1`) for navigation.
  - Dedicated forms for events (`LocalEventsForm`) and issue reporting.

## Project Structure

- `Form1.cs` / `Form1.Designer.cs`: Main application window.
- `LocalEventsForm.cs` / `LocalEventsForm.Designer.cs`: UI for managing local events.
- `EventManager.cs`: Handles event data operations.
- `LocalEvent.cs`: Data model for events.
- `IssueManager.cs`: Handles issue data operations.
- `ReportedIssue.cs`: Data model for reported issues.

## Getting Started

1. **Requirements**
   - Visual Studio 2022 or later
   - .NET Framework 4.8

2. **Build & Run**
   - Open the solution in Visual Studio.
   - Build the project (`Build > Build Solution`).
   - Run the application (`Debug > Start Debugging`).

## Usage

- Use the main form to navigate between event management and issue reporting.
- Add, edit, or remove events and issues using the respective forms.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is for educational purposes.
