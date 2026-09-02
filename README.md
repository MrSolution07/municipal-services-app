# Municipal Services Application for South Africa — Part 1 (Report Issues)


A C# .NET Framework Windows Forms application that lets residents report municipal issues
(location, category, description, optional photo or document) and shows a live progress bar
that encourages them to complete the report.

Part 1 implements **Report Issues** only. Local Events and Announcements (Part 2) and
Service Request Status (Part 3) appear on the main menu but are disabled, as required by the brief.

---

## 1. Requirements

| Item | Version |
|---|---|
| Operating system | Windows 10 or Windows 11 (a Windows virtual machine is fine) |
| IDE | Visual Studio 2022 (Community is enough) |
| Visual Studio workload | .NET desktop development |
| Target framework | .NET Framework 4.8 |
| Language version | C# 7.3 |

The .NET Framework 4.8 runtime ships with Windows 10 (May 2019 update) and Windows 11.
If Visual Studio asks for the 4.8 targeting pack, install it through the Visual Studio Installer
under **Individual components → .NET Framework 4.8 SDK / targeting pack**.

## 2. How to compile

1. Copy the project folder to the Windows machine.
2. Double-click `MunicipalServicesApp.sln` to open it in Visual Studio 2022.
3. Make sure the configuration is **Debug** and the platform is **Any CPU**.
4. Choose **Build → Build Solution** (`Ctrl+Shift+B`).
   The build output appears in `MunicipalServicesApp\bin\Debug\MunicipalServicesApp.exe`.

## 3. How to run

- From Visual Studio: press **F5** (with debugging) or **Ctrl+F5** (without debugging).
- Or run `MunicipalServicesApp\bin\Debug\MunicipalServicesApp.exe` directly after building.

The application starts on the **Main Menu**.

## 4. How to use the programme

### 4.1 Main menu

The main menu opens on startup and offers the three municipal tasks:

1. **Report Issues** — enabled.
2. **Local Events and Announcements** — disabled, added in Part 2.
3. **Service Request Status** — disabled, added in Part 3.

Hovering over a disabled button shows a tooltip explaining when the feature arrives.
The bottom of the menu shows **Issues reported this session**, which is the number of reports
currently held in the application's list.

### 4.2 Reporting an issue

Click **Report Issues**. The report window contains:

| Field | Control | Required |
|---|---|---|
| Location of the issue | TextBox | Yes |
| Category of the issue | Dropdown (sanitation, roads, water, electricity, stormwater, parks, other) | Yes |
| Description of the issue | RichTextBox | Yes |
| Photo or document | Attach button with a file dialog | No |

Steps:

1. Type the location, for example `14 Church Street, Bellville`.
2. Choose the category that matches the problem.
3. Describe what you can see in the description box.
4. Optionally click **Attach image or document**, choose a file, and confirm.
   Images (`.jpg`, `.jpeg`, `.png`, `.bmp`, `.gif`) show a small preview.
   Documents (`.pdf`, `.doc`, `.docx`, `.txt`) show the file name only.
   Cancelling the dialog changes nothing.
5. Click **Submit**. A message box confirms the submission and shows the reference number,
   for example `MSA-20260902-001`. The form then clears itself, ready for the next report.
6. Click **Back to Main Menu** at any time to return to the menu.

If a required field is empty, the application shows a warning message box and places the cursor
in the field that still needs attention. Nothing is saved until every required field is filled in.

### 4.3 The engagement feature (progress bar and message)

The progress bar and the message underneath it are the implemented user engagement strategy:
**progressive completion**. The resident always sees how complete the report is and what to do next.

| Part of the report | Contribution |
|---|---|
| Location entered | 25% |
| Category selected | 25% |
| Description entered (at least 20 characters gives the full weight, shorter gives half) | 30% |
| File attached | 20% |

The message under the bar changes with progress, for example
"Good start. Now choose the category so we send this to the right department."
When the report reaches 100% the message invites the resident to press Submit.
After a successful submission the bar shows 100%, then resets to 0% for the next report.

## 5. Data structure used

Reported issues are stored in a `List<IssueReport>` inside `Data/IssueStore.cs`.
Each `IssueReport` (see `Models/IssueReport.cs`) holds the location, category, description,
attachment path, reference number, and submission timestamp. The list preserves submission order,
new reports are appended with `List.Add`, and the count is shown on the main menu.
Storage is in memory for Part 1, so reports are cleared when the application closes.

## 6. Project structure

```
MunicipalServicesApp.sln
MunicipalServicesApp/
    App.config
    MunicipalServicesApp.csproj
    Program.cs                     entry point, STAThread, starts MainMenuForm
    Data/IssueStore.cs             List<IssueReport> storage
    Models/IssueReport.cs          one reported issue
    Forms/MainMenuForm.cs          main menu, two options disabled
    Forms/ReportIssuesForm.cs      capture form, validation, attachment, progress bar
    UI/AppTheme.cs                 shared colours and fonts
    Properties/AssemblyInfo.cs
```

## 7. Troubleshooting

| Problem | Fix |
|---|---|
| "The target framework 4.8 is not installed" | Install the .NET Framework 4.8 targeting pack from the Visual Studio Installer |
| The solution opens but nothing builds | Right-click the solution, choose **Restore**, then **Build Solution** |
| The attach button does nothing | The file dialog was cancelled; click the button again and choose a file |
| A chosen image shows no preview | The file is a document or is not a readable image; the file name is still attached |
