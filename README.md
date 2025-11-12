ST10109685Prog7312POE
======================

Short description
-----------------
A Windows Forms application (student project) that demonstrates service-request and event management data structures and algorithms. The project includes implementations of multiple tree types (AVL, Red-Black, Binary Search Tree), a min-heap, a graph for service requests, and manager classes / forms for reporting and tracking issues and local events.

Key features
------------
- Data structure implementations: `AVLTree`, `RedBlackTree`, `BinarySearchTree`, `MinHeap`, and `ServiceRequestGraph`.
- Domain classes for issue and event handling: `ReportedIssue`, `LocalEvent`, `IssueManager`, `EventManager`, `ReportedIssue` and `ServiceRequestStatusForm`.
- WinForms front-end with forms for reporting issues, listing local events, and viewing service request status: `Form1`, `ReportIssuesForm`, `LocalEventsForm`, `ServiceRequestStatusForm`.
- Sample data loader (`SampleDataLoader`) to populate demo data.

Project layout
--------------
Files of interest (top-level):
- `ST10109685Prog7312POE.sln` - Visual Studio solution file.
- `ST10109685Prog7312POE.csproj` - Project file (C# WinForms project).
- `Program.cs` - Program entry point that starts the WinForms application.
- `Form1.cs` / `Form1.Designer.cs` - Main application form.
- `ReportIssuesForm.cs` / `ReportIssuesForm.Designer.cs` - UI for reporting issues.
- `LocalEventsForm.cs` / `LocalEventsForm.Designer.cs` - UI for listing local events.
- `ServiceRequestStatusForm.cs` / `ServiceRequestStatusForm.Designer.cs` - UI for tracking requests.
- `IssueManager.cs`, `EventManager.cs` - Manager classes coordinating domain operations.
- `ReportedIssue.cs`, `LocalEvent.cs` - Domain model classes.
- `AVLTree.cs`, `BinarySearchTree.cs`, `RedBlackTree.cs` - Tree implementations.
- `MinHeap.cs` - Min-heap implementation.
- `ServiceRequestGraph.cs` - Graph structure for service request routing/relations.
- `SampleDataLoader.cs` - Helper to create sample data at startup.

Build & run
-----------
This is a Windows Forms (desktop) C# project. The recommended way to build and run is via Visual Studio (Community/Professional/Enterprise) on Windows.

From Visual Studio
- Open `ST10109685Prog7312POE.sln`.
- Set the configuration to `Debug` or `Release` as desired.
- Press F5 (Start Debugging) or Ctrl+F5 (Start Without Debugging).

From PowerShell / command line (if you have the appropriate .NET SDK and MSBuild tooling installed)
- Build the solution:

```powershell
# from the repository root (where the .sln file is located)
dotnet build "ST10109685Prog7312POE.sln"
```

- Run the built executable directly (Debug build):

```powershell
& "bin\Debug\ST10109685Prog7312POE.exe"
```

Note: If the project targets the .NET Framework (classic WinForms) rather than .NET Core/.NET, use Visual Studio to build and run. The `dotnet` CLI may not support building older .NET Framework WinForms projects without the appropriate SDK/workload.

Usage
-----
- Launch the application to explore the UI forms for reporting issues and viewing local events.
- Use `SampleDataLoader` (if enabled) to populate demo entries.
- The managers (`IssueManager`, `EventManager`) handle storage and retrieval; data structures provide the underlying storage/ordering where applicable.

Testing
-------
No automated unit tests are included in the repository. To test manually:
- Run the application and exercise the UI flows (report an issue, list events, view status).
- Inspect the `SampleDataLoader` to see how sample items are created.

Extending the project
---------------------
- Add unit tests using a test framework (NUnit/xUnit/MSTest) to validate the data structures and managers.
- Persist data to a file or database instead of in-memory collections if you want long-lived storage.
- Add input validation and improved UI/UX for production readiness.

Assumptions & notes
-------------------
- This README is written from the source files present in the repository. The project appears to be a student assignment demonstrating data structures and a WinForms UI.
- If you plan to build with the `dotnet` CLI and encounter errors, open the solution in Visual Studio and check the target framework in `ST10109685Prog7312POE.csproj`.

License
-------
- This repository does not include a formal license file. If you want to apply a license, consider adding a `LICENSE` file (for example MIT) and update this section.

Author / Contact
----------------
- Student project. For questions or updates, open an issue or contact the project owner.


Enjoy exploring the codebase!
