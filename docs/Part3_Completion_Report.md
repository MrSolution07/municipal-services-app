# Project completion report

Advanced Application Development (AAPD7112)

Municipal Services Application for South Africa

Student name: [Student name]

Student number: [Student number]

## Project overview

The application is one Windows Forms program. A resident can report an issue, look up a local event, and track the service request that follows from the report. All three tasks are on the main menu.

Part 1 stores each report in a list. The form asks for a location, a category, and a description, and it can attach a file. A progress bar shows how complete the report is. The reference number looks like MSA-20261111-001.

Part 2 lists upcoming events and announcements. The resident searches by category and date. Featured events come from a priority queue. Notices are a queue. Recently viewed events are a stack. Recommendations use the search history.

Part 3 is the status page. Twelve seeded requests, SR-01 to SR-12, are loaded with the program. A new report is added to that same catalogue and keeps the MSA reference from the confirmation. The page shows a category tree, a reference search, the request list, the status steps, the next urgent request, related work, the dependency chain, and a crew route. Nothing is written to disk. Closing the program clears the session.

## Challenges and fixes

Part 1. The progress bar had to move only when a required field was actually filled, and a short description could not count as a finished one. The form now gives location and category 25 percent each, the description 30 percent (half of that until it reaches 20 characters), and the attachment 20 percent. Empty required fields stop the submit and place the cursor in the field that is still empty.

Part 2. The first events screen packed the search, the list, the notices, and the stack into one scrolling pane, and card text was drawn in the wrong order. The screen was rebuilt as three columns, with cards that stretch to the column and a border so they can be told apart. The notice button used to rotate the queue, so every notice stayed on screen. It now dequeues the front item. The lecturer then asked for finished navigation and a sharper recommendation score. The third menu button was still disabled, and the score treated an old favourite the same as a search made a moment ago. Both are fixed. The feedback file records the detail.

Part 3. The hard part was keeping several structures in step when a status changed. A delete on a red-black tree has many cases, and a wrong delete would crash the demonstration. The store therefore rebuilds the AVL tree, the red-black tree, the heap, the graph, and the route by inserting the current requests again, then checks the AVL balance and the red-black black-height. The binary search tree is not rebuilt. It is filled once, in order from SR-01 to SR-12, so it stays a spine of height 12 and the AVL height of 4 means something. Assign next peeks the heap and then calls SetStatus. It does not also dequeue, which would have removed the request twice. The report form and the status form share one static catalogue, so the request created on submit is the request the tracker opens. The program is written on a Mac and compiled in a Windows virtual machine, because .NET Framework 4.8 WinForms does not build on macOS.

## Key learnings

I learned that a data structure has to do one visible job, or I cannot tell whether it works. On the status page, Find shows the binary search path, the height line shows why the AVL tree is used, By urgency shows the red-black order, Next urgent shows the heap, and the crew route shows Prim's algorithm. If one of those panels is wrong, I know which structure to fix.

I also learned to keep a status change in one method. SetStatus updates the request, then rebuilds the trees, the heap, the graph, and the route together. With only twelve requests, rebuilding is fast enough, and the screen cannot show a heap that still contains a request I have already assigned.

## Technology recommendations

SQLite. The catalogue is cleared when the program closes, so a resident cannot come back the next day and see the same status. A single SQLite file next to the executable would keep the requests. The application is already a desktop program for one machine, so it does not need a database server, and the existing reference number can stay the primary key.

A map on the crew-route panel. Prim already has the depot, the suburbs, and the kilometres. Drawing those edges would make the 33 km route easier to check than a list of names. A WinForms map control, or a small embedded browser pointed at a local page, fits the current desktop project. The distance table would not have to change.

Email or SMS when a status advances. The resident already receives an MSA reference on submit. A message that quotes that reference, sent when Advance status or Assign next runs, would close the loop without a new screen. A local outbound mail setting is enough for a municipal desktop app. The status values already exist, so the message is only a notification of a change the program already makes.
