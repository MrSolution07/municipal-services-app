# Municipal Services Application for South Africa — full app (Part 3)

A C# .NET Framework Windows Forms application that helps residents interact with their municipality.

- **Part 1 (Report Issues)** — report an issue with location, category, description and optional media, guided by a progress bar.
- **Part 2 (Local Events and Announcements)** — search upcoming events and announcements by category and date, see featured and recommended items, and track recently viewed items.
- **Part 3 (Service Request Status)** — track a request by reference, see its status, and follow the related work and the crew route.

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
3. **Service Request Status** — enabled (Part 3).

The bottom of the menu shows **Issues reported this session** and **Open service requests**.
Open means every request that is not Resolved. On a fresh start that number is 11.

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
   for example `MSA-20261111-001`. It then asks **Track this request now?**
   - **Yes** closes the report and opens Service Request Status on that same reference.
   - **No** clears the form so another report can be entered.
   The new report is stored as a service request with status Submitted and priority 3.
   A location that names Bellville, Durbanville, CBD, Parow, or Brackenfell uses that area.
   Any other location is filed under Bellville.
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

- **Featured (priority queue)** — the three highest-priority upcoming events, ranked #1 to #3.
- **Search** — choose a category and/or a date, then press **Search**.
  Tick **Any date** to ignore the date filter. Press **Clear** to show all upcoming events again.
- **Upcoming events / Search results** — scrollable cards with a count in the heading.
  Click a card to open it: details appear, the event is pushed on the stack, recommendations update.
- **Selected event** — title, category, date, venue and summary.
- **Recommended for you** — related events, each with a **Score** and a "Why:" line.
- **Latest notices (queue)** — the front item is marked "Next to read".
  Press **Read next notice** to dequeue it and read it. When the queue is empty the button becomes
  **Reload notices**.
- **Recently viewed (stack)** — newest event on top ("Top of stack").
  Press **Undo last view** to pop it. The button is disabled when the stack is empty.

Search tip: search **Utilities**, search **Utilities** again, then search **Recreation**.
Utilities stays the top category. Recreation becomes the second. A third category such as Roads
gets no category bonus from those three searches.

### 4.5 Service Request Status

Click **Service Request Status**. The window has three columns.

- **Left.** A category tree (departments, then categories). The line under it is the same hierarchy stored as a binary tree (left child = first subcategory, right child = next sibling). Type a reference and press **Find**. **By reference** lists every request in AVL order. **By urgency** lists open requests in red-black order (resolved requests are hidden in that mode). The status drop-down filters the list.
- **Middle.** The selected request, a five-step status line, **Advance status**, and the tree heights. The current step is shown in square brackets.
- **Right.** **Next urgent** peeks the heap. **Assign next** sets that request to Assigned. **Related work** is a breadth-first walk. **Dependency chain** is a depth-first post-order of "depends on" links, root cause first. **Crew route** is a minimum spanning tree from the Civic Centre depot across areas that still have an open request.

**Marker script** (fresh start, before any new report):

1. The menu shows **Open service requests: 11**.
2. Open Service Request Status. Press **Find** on `SR-07`. The height line reads: BST visited 7 nodes, height 12. AVL height 4. The path visited is `SR-01, SR-02, SR-03, SR-04, SR-05, SR-06, SR-07`.
3. Press **By urgency**. The first card is `SR-01` (priority 5, oldest).
4. Press **Assign next**. `SR-01` changes from In review to Assigned, and the heap's next card becomes `SR-02`.
5. Click `SR-03`. The dependency chain is `SR-01`, `SR-02`, `SR-03`.
6. Select `SR-09` and press **Advance status** four times (Submitted, In review, Assigned, In progress, Resolved). Brackenfell leaves the crew route on the fourth press. The route changes from 5 edges, 33 km, to 4 edges, 25 km.

On a fresh open the crew route is:

- Civic Centre to Bellville, 4 km
- Bellville to Parow, 5 km
- Bellville to CBD, 6 km
- Bellville to Durbanville, 10 km
- Durbanville to Brackenfell, 8 km

Related work for `SR-07` is `SR-08`, one step away. A request with no open neighbour shows "No related work in this area."

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
| `Queue<LocalEvent>` | `Data/EventCatalogue.cs` | Holds the latest announcements in FIFO order. | The "Latest notices" panel; "Read next notice" dequeues the front item. |
| `Queue<SearchRecord>` | `Data/EventCatalogue.cs` | Records recent user searches to feed the recommendation engine. | Search patterns influence the "Recommended for you" panel. |
| `Dictionary<string, int>` | `Data/EventCatalogue.cs` | View points per category, capped at 3. | Opening events in a category adds at most 3 to that category's score. |
| `Stack<string>` | `Data/EventCatalogue.cs` | Tracks the ids of recently viewed events. | The "Recently viewed (stack)" panel and "Undo last view" button. |
| `SimplePriorityQueue` (binary heap) | `Data/SimplePriorityQueue.cs` | Orders events by priority then date. | The "Featured" strip shows the top three items from the queue. |

## 6. Recommendation feature

`EventCatalogue.GetRecommendations` uses the search queue and the capped view dictionary. Events already on the Recently viewed stack are left out. Each card shows `Score n` and a `Why:` line.

1. Walk the search queue from newest to oldest. The last five searches that have a category weigh 5, 4, 3, 2, 1. An empty category is skipped, so it never becomes the favourite. The heaviest category scores +4. The second scores +2.
2. The newest search that has a date sets proximity: same day +3, within 3 days +2, within 7 days +1.
3. A category the resident has opened scores +1, +2, or +3. The fourth open does not add more.
4. Priority 3 or higher scores +2. An event (not an announcement) scores +1.
5. The top five are returned. Ties break toward the earlier date.

Example. Search Utilities, search Utilities again, then search Recreation, with no date. Utilities has weight 4 + 3 = 7. Recreation has weight 5. Utilities events gain +4. Recreation events gain +2. Roads gains nothing from these searches. Youth sports day (Recreation, priority 3, an event) scores 2 + 2 + 1 = 5 before any date or view points.

## 7. Service request structures

`ServiceRequestStore` keeps one master list. `SetStatus` is the only way a status changes. It rebuilds the derived structures. The binary search tree is the exception: references are inserted once, in seed order `SR-01` to `SR-12`, and a status change does not rebuild it. That is why the fresh tree is a right spine of height 12. Height counts nodes on the longest path. One node has height 1.

| Structure | File | Role | Where the user sees it | Cost |
|---|---|---|---|---|
| N-ary category tree | `Data/CategoryTree.cs` | Department, then category. | The tree on the left. "All services" clears the filter. | Build O(n log n) because the children are sorted. |
| Binary tree | `Data/BinaryTree.cs` | Left-child / right-sibling copy of that hierarchy. | The "Binary encoding" line. Preorder starts `All services, Community, Buildings, ...`. | Walk O(n). |
| Binary search tree | `Data/BinarySearchTree.cs` | Find a reference and record every node visited. | **Find**, and the height line. `SR-07` visits 7 nodes. Height 12. | Search O(h). On this spine h = n. |
| AVL tree | `Data/AvlTree.cs` | Balanced index of every reference. | **By reference**, and "AVL height 4" beside the spine of 12. | Insert O(log n). A status change rebuilds in O(n log n). |
| Red-black tree | `Data/RedBlackTree.cs` | Open requests ordered by priority, then age, then reference. | **By urgency**. `SR-01` is first. | Insert O(log n). Rebuild O(n log n). There is no delete. |
| Max-heap | `Data/MaxHeap.cs` | Submitted and In review only. | **Next urgent** peeks. **Assign next** calls `SetStatus`, which rebuilds the heap. It does not dequeue. | Peek O(1). Rebuild O(n). |
| Directed graph | `Data/ServiceGraph.cs` | `DependsOn` points at the request that must finish first. `Related` links requests in the same area, both ways. | Used by the two walks below. | Build O(n + e). |
| Breadth-first search | `Data/ServiceGraph.cs` | Nearest open neighbours, with a step count. | **Related work**. `SR-07` shows `SR-08` at 1 step. | O(n + e). |
| Depth-first search | `Data/ServiceGraph.cs` | Post-order along `DependsOn`. | **Dependency chain**. `SR-03` shows `SR-01, SR-02, SR-03`. | O(n + e). |
| Area graph and Prim | `Data/AreaGraph.cs`, `Data/MinimumSpanningTree.cs` | Complete distance matrix. Prim starts at Civic Centre and returns vertices − 1 edges. | **Crew route**. Fresh route: 5 edges, 33 km. | Prim O(v²) on this matrix. |

`Validate` runs after load and after every status change. It checks AVL balance factors and the red-black black-height. A failed check throws before the form is used.

## 8. Project structure

```
MunicipalServicesApp.sln
MunicipalServicesApp/
    App.config
    MunicipalServicesApp.csproj
    Program.cs                     entry point, STAThread, starts MainMenuForm
    Data/
        IssueStore.cs              List<IssueReport> storage for Part 1
        EventCatalogue.cs          events, queue, stack, recommendations
        SimplePriorityQueue.cs     binary heap for featured events
        ServiceRequestStore.cs     master list and rebuild for Part 3
        CategoryTree.cs            n-ary department and category tree
        BinaryTree.cs              left-child / right-sibling encoding
        BinarySearchTree.cs        reference search
        AvlTree.cs                 balanced reference index
        RedBlackTree.cs            urgency order
        MaxHeap.cs                 next request to assign
        ServiceGraph.cs            dependencies, BFS, and DFS
        AreaGraph.cs               distances between areas
        MinimumSpanningTree.cs     Prim crew route
    Models/
        IssueReport.cs             one reported issue
        LocalEvent.cs              one event or announcement
        ServiceRequest.cs          one tracked service request
    Forms/
        MainMenuForm.cs            main menu with three options
        ReportIssuesForm.cs        capture form, validation, attachment, progress bar
        LocalEventsForm.cs         events search, recommendations, stack, queue
        ServiceRequestForm.cs      status, trees, heap, graph, and route
    UI/
        AppTheme.cs                shared colours and fonts
    Properties/AssemblyInfo.cs
```

## 9. Troubleshooting

| Problem | Fix |
|---|---|
| "The target framework 4.8 is not installed" | Install the .NET Framework 4.8 targeting pack from the Visual Studio Installer |
| The solution opens but nothing builds | Right-click the solution, choose **Restore**, then **Build Solution** |
| The attach button does nothing | The file dialog was cancelled; click the button again and choose a file |
| A chosen image shows no preview | The file is a document or is not a readable image; the file name is still attached |
| Recommendations do not change | Search a category such as Utilities, then search a second category |
