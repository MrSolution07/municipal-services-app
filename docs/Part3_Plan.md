# Part 3 plan — full functioning app (aim: top band on every criterion)

Work only in `MunicipalServicesApp` at the repository root. Leave `st10538419_AAPD7112_Part2` untouched. That folder is the Part 2 submission.

The app already has Report Issues, Local Events, the shared theme, and a disabled **Service Request Status** button. Part 3 turns that button on, adds the status page, and writes the three documents the brief asks for.

If the solution does not compile and run, the application marks are 0. The last step is a build in the Windows VM.

## What "done" means

The brief totals 100. The rubric's top band is the target for each row.

| Marks | Top band requires | How this plan meets it |
|---|---|---|
| 20 | Trees organise and retrieve service requests with no wasted structure | Five tree types, each with one job on the status form |
| 30 | Heap, graph, traversal, and MST manage relationships and the status display | Each one has a panel and changes when a status changes |
| 10 | Readme: compile, run, and use | README rewritten for the full app, with a click-by-click marker script |
| 10 | In-depth explanation of every structure, with an example | README section per structure: role, big-O, one example from the seed data |
| 20 | Whole project, challenges and fixes, learnings | Word report. Facts from this build. Personal learning lines filled by the student |
| 10 | Extra technologies, justified for this app | Three recommendations in the Word report. Not built into the app |
| Required file | Updates from the lecturer's feedback, specific | `docs/Feedback_Updates.md`: navigation, and the new recommendation score, with a before-and-after example |

The lecturer's Part 2 note is part of the mark, not a side task: complete the navigation, and refine the recommendation score.

## 1. Feedback, done first

### Navigation

- Enable `btnServiceStatus`. Label: `3.  Service Request Status`. Same active style as the other two buttons.
- Remove the "available later" tooltip.
- Main menu shows two session lines: issues reported, and open service requests.
- After a successful report, the confirmation asks **Track this request now?**
  - Yes: the report form closes, the main menu opens Service Request Status, and that reference is selected.
  - No: the report form clears, as it does now.
- The new issue is inserted into the service-request store before the dialog closes, so the tracker already contains it.
- Service Request Status has **Back to Main Menu**, same placement as the other forms.

### Recommendation score

Keep the current inputs (search queue, category counts, viewed stack, priority). Change the score so a recent search beats an old one.

- Walk the search queue from newest to oldest. Only the last five category searches count, with weights 5, 4, 3, 2, 1.
- The heaviest category gets +4. The second gets +2. One old favourite no longer locks the list.
- Date match is graduated: same day +3, within 3 days +2, within 7 days +1.
- An event (not an announcement) gets +1, so notices do not fill the list.
- Priority 3 or higher still gets +2.
- A viewed event still adds 1 to its category, capped at 3, so clicking does not drown out searches.
- Cards already on the viewed stack stay excluded.
- Each card shows `Score n` and the existing `Why:` line.

`docs/Feedback_Updates.md` records both changes in the lecturer's words, then the code change, then one worked example: search **Utilities**, search **Utilities** again, search **Recreation**, and state which events rise and why.

## 2. Service request

`Models/ServiceRequest.cs`

- `Reference` — `SR-001` style. Unique.
- `Department`, `Category`, `Location`, `Area`
- `Description`
- `Status` — Submitted, In review, Assigned, In progress, Resolved
- `Priority` — 1 to 5, 5 is the most urgent
- `SubmittedAt`
- `Source` — Seed or Resident report

`Data/ServiceRequestStore.cs` owns every structure below. Forms never touch the trees directly.

Seed about 12 requests across Bellville, Durbanville, CBD, Parow, and Brackenfell, plus a depot called Civic Centre. Include:

- a dependency chain of three (electricity outage, then street lights, then a follow-up inspection)
- a second chain of two (water main shutoff, then pipe repair)
- a mix of statuses, so the heap, the graph, and the route all change when one request is resolved
- references inserted in sorted order, so the plain binary search tree becomes a tall spine and the AVL tree stays short

`IssueStore` stays the Part 1 list. On submit, the store also builds a `ServiceRequest` with status Submitted, priority 3, and the report's category and location. The area is the part of the location the resident typed; if none of the known areas appears, the area is Bellville.

## 3. Structures and the job each one does

Implement these. Do not wrap `SortedSet` or `SortedDictionary` and call it a tree. `Dictionary` is allowed only as a helper index from reference to object.

### Trees (20)

All four ordered trees hold the same requests and use `string` comparison on the reference, except the red-black tree.

| Class | Kind | Job on the form |
|---|---|---|
| `Data/BinaryTree.cs` | Binary tree, left-child / right-sibling | Stores the department → category hierarchy. The left child is the first subcategory. The right child is the next sibling. The category `TreeView` is filled by walking this encoding |
| `Data/BinarySearchTree.cs` | BST | Reference search. The detail panel lists every node visited |
| `Data/AvlTree.cs` | AVL | The request list. In-order walk, so the list is in reference order. The panel shows the AVL height next to the BST height after the same inserts |
| `Data/RedBlackTree.cs` | Red-black | Open requests only, ordered by priority (high first), then oldest first, then reference. This is the browse order for "most urgent first". It is not destroyed by a pop |

AVL and red-black both support insert and delete, because a status change must update them. A `Validate` method checks the invariant after each mutation (AVL balance factor in -1..1, red-black black-height and no red-red edge). It throws if the seed is wrong. The user never sees it.

The height comparison is the efficiency point the marker can see: sorted inserts make the BST height equal to the number of requests, while the AVL height stays near log2(n). The readme uses the actual numbers from the seed.

### Heap, graph, traversal, MST (30)

| Class | Job on the form |
|---|---|
| `Data/MaxHeap.cs` | Max-heap of open requests (Submitted, In review, Assigned, In progress). Order: higher priority, then older, then reference. **Next urgent** peeks and does not remove. **Assign next** dequeues, sets that request to Assigned, and refreshes every panel |
| `Data/ServiceGraph.cs` | Directed graph. Edge kinds: `DependsOn`, `Related`. Adjacency lists. Seed the two dependency chains, plus `Related` edges between requests in the same area |
| BFS on that graph | **Related work** lists reachable requests with the step count. Breadth-first, so nearer work appears first |
| DFS on that graph | **Dependency chain** follows `DependsOn` links only, so the marker sees the order work must finish in |
| `Data/AreaGraph.cs` and Prim in `Data/MinimumSpanningTree.cs` | Undirected weighted graph of areas, distances in km, depot included. Prim runs on the depot plus areas that still have an open request. **Crew route** lists the edges and the total km. Resolving the last open request in an area drops that area from the route |

Heap removal of a specific request (when the user advances a status that is not "assign next") uses an index map so the remove is O(log n), not a scan. The readme says so.

MST edge count is always vertices − 1. The route panel states that.

## 4. Service Request Status form

Same theme as Local Events: header, readable type, bordered cards, ellipsis on long lines, no sideways clipping, **Back to Main Menu** at the bottom right.

Three columns:

1. **Find a request.** Category tree. Reference search box and **Find** button. Scrollable request list from the AVL in-order walk. Each row shows reference, status, area, and priority. A status filter (All, plus the five statuses) sits above the list.
2. **Selected request.** Title line is the reference and category. Body is location, area, description, source, and submitted date. A five-step status line shows how far it has moved. **Advance status** moves it one step forward. Under the detail, a short line: `BST visited n nodes, height h. AVL height k.`
3. **Relationships.** Next urgent (heap peek). Related work (BFS). Dependency chain (DFS). Crew route (Prim), with total kilometres.

Empty states use a full sentence, same as Local Events ("No open requests to route.").

Selecting a row, a search hit, a BFS row, or the heap card all open the same detail. Advancing a status refreshes the list, the heap, both traversals, and the route.

`MainMenuForm` opens the form with an optional reference. When that reference is set, the form selects it on load.

## 5. Documents

### README.md (implementation report)

Keep the Part 1 and Part 2 instructions. Add:

- Requirements line for the full app.
- Compile and run, unchanged in substance (Visual Studio 2022, .NET Framework 4.8, F5).
- How to use Service Request Status, including the track-from-report path.
- A marker script with the real seed references: search one reference, read the BST path, compare heights, press **Assign next**, resolve one request, watch the crew route lose an area.
- One subsection per structure: class name, what the screen uses it for, the operation's big-O, and one numeric example taken from the seed (heights, a BFS order, MST total km).

### Word report (project completion + technology)

`docs/Part3_Completion_Report.md`, then a `.docx` in the same formatting as Part 1: Arial 12 pt, 1.5 line spacing, a new page per section. The student replaces the name and student-number placeholders before upload.

Sections:

1. Cover. Module, student name, student number.
2. Project overview. Parts 1 to 3 as one app: report an issue, find an event, track the request.
3. Challenges and fixes. Real ones from this build: WinForms layout on the events page, the queue originally rotating instead of dequeuing, building on a Mac and compiling in the Windows VM, keeping five tree types correct when a status changes. Task 3 gets the most space. Parts 1 and 2 get a short honest paragraph each, because the rubric overview includes all three tasks.
4. Key learnings. Short and specific. Leave a clearly marked paragraph for the student's own learning.
5. Technology recommendations. Three, each with a benefit and a reason it fits this desktop app:
   - SQLite, so requests survive a restart. The app is already a single Windows program; a local file needs no server.
   - A map control on the crew-route panel, using the same areas and distances Prim already uses.
   - Email or SMS when a status advances, using the reference the resident already receives on submit.

These three are recommendations only. They are not built.

### Feedback file

`docs/Feedback_Updates.md`. Quote the lecturer's sentence. Then two sections, Navigation and Recommendation scoring. Each section: what was wrong, what changed, which files, and how to see it in the app. The scoring section includes the worked example from section 1.

## 6. Build order

1. Feedback file outline, then the menu button, the track-from-report flow, and the new event score.
2. `ServiceRequest`, the store, and the seed.
3. Binary tree and the category `TreeView`. BST and AVL, with heights on the detail panel. Red-black tree and the urgency order.
4. Heap, graph, BFS, DFS, Prim. Wire **Advance status** and **Assign next** so all four panels update together.
5. Layout pass: type size, spacing, empty states, long text.
6. README, Word report, feedback file completed against the finished seed numbers.
7. Build in the Windows VM. Walk the marker script once.

## 7. Out of scope

- No database, no map library, no mail. Those belong in the recommendation section of the report.
- No edits inside `st10538419_AAPD7112_Part2`.
- No new structures that the form never shows.
