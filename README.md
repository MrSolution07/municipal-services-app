# Municipal Services Application for South Africa — Part 2

A C# .NET Framework Windows Forms application that helps residents interact with their municipality.

- **Part 1 (Report Issues)** — report an issue with location, category, description and optional media, guided by a progress bar.
- **Part 2 (Local Events and Announcements)** — search upcoming events and announcements by category and date, see featured and recommended items, and track recently viewed items.
- **Part 3 (Service Request Status)** — disabled on the menu; this part is implemented later in the POE.

Storage is in memory, so data is cleared when the application closes.

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

The main menu opens on startup and offers three municipal tasks:

1. **Report Issues** — enabled (Part 1).
2. **Local Events and Announcements** — enabled (Part 2).
3. **Service Request Status** — disabled; this feature is added in Part 3.

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

The progress bar and the message underneath it are the Part 1 user engagement strategy:
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

### 4.4 Local Events and Announcements

Click **Local Events and Announcements**. The events window contains:

- **Featured** — a strip of the three highest-priority upcoming events.
- **Search** — choose a category and/or a date, then press **Search**.
  Tick **Any date** to ignore the date filter.
  Press **Clear** to show all upcoming events again.
- **Upcoming events** — a scrollable list of matching events.
  Click a card to see its details, add it to "Recently viewed", and update recommendations.
- **Selected event** — shows the title, category, date, venue and full summary.
- **Recommended for you** — suggests related events based on your searches.
- **Recently viewed (stack)** — links to events you have clicked.
  Press **Remove last viewed** to pop the most recent item off the stack.
- **Latest notices (queue)** — short municipal announcements.
  Press **Next notice** to rotate the queue (dequeue the front, enqueue it at the back).

Search tip: search a category such as **Recreation** or **Utilities**, then watch the
recommendation panel update.

## 5. Data structures and their roles

### Part 1

Reported issues are stored in a `List<IssueReport>` inside `Data/IssueStore.cs`.
Each `IssueReport` (see `Models/IssueReport.cs`) holds the location, category, description,
attachment path, reference number, and submission timestamp. The list preserves submission order,
new reports are appended with `List.Add`, and the count is shown on the main menu.

### Part 2

The Local Events feature uses the structures listed below. Their roles are described in
`Data/EventCatalogue.cs` and `Data/SimplePriorityQueue.cs`.

| Structure | File | Role in the application | Where the user sees it |
|---|---|---|---|
| `Dictionary<string, LocalEvent>` | `Data/EventCatalogue.cs` | Hash table for fast lookup of an event by its unique id. | Clicking a card or recommendation shows the event details instantly. |
| `SortedDictionary<DateTime, List<LocalEvent>>` | `Data/EventCatalogue.cs` | Keeps events sorted by date for chronological listing and date filtering. | The "Upcoming events" list and date search results are ordered by date. |
| `HashSet<string>` and `HashSet<DateTime>` | `Data/EventCatalogue.cs` | Stores unique categories and unique dates used to build the filter controls. | The category drop-down and date search use these unique values. |
| `Queue<LocalEvent>` | `Data/EventCatalogue.cs` | Holds the latest announcements in FIFO order. | The "Latest notices" panel and "Next notice" button rotate the queue. |
| `Queue<SearchRecord>` | `Data/EventCatalogue.cs` | Records recent user searches to feed the recommendation engine. | Search patterns influence the "Recommended for you" panel. |
| `Dictionary<string, int>` | `Data/EventCatalogue.cs` | Counts how often each category is searched. | The most-searched category boosts recommendation scores. |
| `Stack<string>` | `Data/EventCatalogue.cs` | Tracks the ids of recently viewed events. | The "Recently viewed (stack)" panel and "Remove last viewed" button. |
| `SimplePriorityQueue` (binary heap) | `Data/SimplePriorityQueue.cs` | Orders events by priority then date. | The "Featured" strip shows the top three items from the queue. |

## 6. Recommendation feature

`EventCatalogue.GetRecommendations` analyses the user's search history and current results:

1. It finds the most-searched category from the `Dictionary<string, int>`.
2. It looks at the most recent search (from the `Queue<SearchRecord>`) for category and date.
3. It scores every upcoming event that is not already in the current results:
   - +4 if the event's category matches the most-searched category.
   - +2 if the event's category matches the last searched category.
   - +2 if the event's date is within seven days of the last searched date.
   - +2 if the event has a high priority.
4. It returns the top three, each with a short reason shown on the card.

## 7. Project structure

```
MunicipalServicesApp.sln
MunicipalServicesApp/
    App.config
    MunicipalServicesApp.csproj
    Program.cs                     entry point, STAThread, starts MainMenuForm
    Data/
        IssueStore.cs              List<IssueReport> storage for Part 1
        EventCatalogue.cs          advanced data structures for Part 2 events
        SimplePriorityQueue.cs     binary-heap priority queue
    Models/
        IssueReport.cs             one reported issue
        LocalEvent.cs              one event or announcement
    Forms/
        MainMenuForm.cs            main menu with three options
        ReportIssuesForm.cs        capture form, validation, attachment, progress bar
        LocalEventsForm.cs         events search, recommendations, stack, queue
    UI/
        AppTheme.cs                shared colours and fonts
    Properties/AssemblyInfo.cs
```

## 8. Troubleshooting

| Problem | Fix |
|---|---|
| "The target framework 4.8 is not installed" | Install the .NET Framework 4.8 targeting pack from the Visual Studio Installer |
| The solution opens but nothing builds | Right-click the solution, choose **Restore**, then **Build Solution** |
| The attach button does nothing | The file dialog was cancelled; click the button again and choose a file |
| A chosen image shows no preview | The file is a document or is not a readable image; the file name is still attached |
| Recommendations do not change | Perform a category or date search first; recommendations are based on search history |
