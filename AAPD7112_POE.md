# AAPD7112 / PROG7312 — Portfolio of Evidence (POE)

| Field | Value |
|---|---|
| Module names | Programming 3B / Advanced Application Development |
| Module codes | PROG7312 / AAPD7112/w |
| Assessment type | POE (paper and marking rubrics) |
| Total marks | 300 |
| Suggested hours | minimum 45 |
| Year | 2026, © The Independent Institute of Education (Pty) Ltd |

## Instructions

1. No material may be copied from original sources, even if referenced correctly, unless it is a direct quote indicated with quotation marks. No more than 10% of the assignment may consist of direct quotes.
2. Submit through Turnitin. Attach a similarity report to the POE if a hard copy is required.
3. Make a copy of the assignment before handing it in.
4. Assignments must be typed unless otherwise specified.
5. Begin each section on a new page.
6. Follow all instructions on the POE cover sheet.
7. This is an individual assignment.

## Referencing rubric (penalty, not marks)

- Inconsistent or incongruent referencing costs up to 10% of the overall percentage.
- Five or more minor errors: deduct 5%.
- Five or more major errors: deduct 10%.
- Both minor and major present: deduct 10% only (not 5% or 15%).
- Required style for ICT/Engineering: **IEEE**, used consistently in-text and in the reference list.
- Consistency: same style throughout; every quote and paraphrase referenced; in-text reference positioned at each quote/paraphrase.
- Congruency: every in-text source appears in the reference list and vice versa; list ordered per the style; quotes and paraphrases integrated with introductory phrases and correct grammar.
- Plagiarism, missing reference lists, or very poor referencing may trigger IIE023 action. Evidence of excessive AI usage may trigger IIE015 action.

## Background

Develop a C# .NET Framework software application to streamline municipal services in South Africa. The application provides an efficient, user-friendly platform for citizens to access and request municipal services.

### Scenario

A South African municipality wants to improve citizen engagement and service delivery through a comprehensive municipal services application. Residents should be able to:

- Report issues and request services.
- Access information about local events and announcements.
- Receive updates on the status of their service requests.

### Note to students

Ensure the application is fully functional and meets all outlined specifications. Aim for a user-centric experience that adds value to citizens.

---

# Part 1 — Municipal Services Application (Report Issues) — 100 marks

Learning Units: LU1 – LU2. Two tasks: Research (20) and Implementation (80).

## Task 1: Research (20 marks)

Conduct online research on user engagement strategies suitable for a municipal services application, especially in the South African context. Starting article:

> Hart, Tim G. B., et al. "Innovation for Development in South Africa: Experiences with Basic Service Technologies in Distressed Municipalities." Forum for Development Studies, vol. 47, no. 1, 20 Aug. 2019, pp. 23–47. [Accessed 20 February 2025]

In a Word document:

- List five user engagement strategies considered during the research.
- Provide a 500-word explanation of the chosen user engagement strategy and justify why it was selected.
- Line spacing 1.5. Font Times New Roman or Arial, 11 or 12 pt.
- Reference the sources used.

**Note:** If the explanation exceeds 500 words, any content beyond this point will not be marked.

## Task 2: Implementation (.NET Framework Windows Application) — 80 marks

### Requirements

1. On startup, present the user with three tasks:
   a. Report Issues (to be implemented).
   b. Local Events and Announcements (to be implemented later).
   c. Service Request Status (to be implemented later).
2. Only "Report Issues" is implemented initially; **disable the other two options**.
3. On selecting "Report Issues," prompt users for details about the issue, including location and category.
4. Users must be able to attach images or documents related to the issue.
5. Implement the chosen user engagement strategy to encourage active participation.

### Technical requirements

- Use appropriate data structures to store user-reported issues and relevant details.
- Create a readme file explaining how to compile, run, and use the programme.

### User interface specifications

**1. Main Menu (Form)** — presented on startup with the three options above.

**2. Report Issues Page (Windows Form)** — a new Windows Form containing:

| Element | Control |
|---|---|
| a. Location input | TextBox |
| b. Category selection | Dropdown or ListBox (e.g. sanitation, roads, utilities) |
| c. Description box | RichTextBox |
| d. Media attachment | Button with OpenFileDialog (images or documents) |
| e. Submit | Button clearly labelled "Submit" |
| f. Engagement feature | Label with encouraging messages or ProgressBar showing reporting progress |
| g. Navigation | Button(s), e.g. "Back to Main Menu" |

### Design considerations

1. **Consistency** — consistent colour scheme and layout throughout.
2. **Clarity** — labels, buttons, and instructions clear for a diverse user base.
3. **User feedback** — MessageBox success messages and error alerts.
4. **Responsiveness** — interface accommodates various screen sizes and resolutions.

### Additional requirements

1. **Form interactions (event handling)** — event handlers for button clicks and user interactions.
2. **Data handling (data structures)** — appropriate data structures (e.g. a list for storing issues) to manage and organise reported issues.

**Note:** If the code does not compile and run, no marks are awarded for any application functionality.

### Submit for Part 1

1. A Word document containing the research.
2. Source code for the application.
3. The readme file with instructions for how to compile, run, and use the software.

Keep a copy of the code — Part 2 and the POE build on this application.

---

# Part 2 — Municipal Services Application (Collaboration) — 100 marks

Learning Units: LU1 – LU4. Focus: stacks, queues, priority queues, hash tables, dictionaries, sorted dictionaries, sets, plus a recommendation feature based on user searches.

## Task 1: Implementation (100 marks)

**a. Main Menu (Form) — 30 marks.** Windows Form with an organised menu: Report Issues (Part 1), Local Events and Announcements (this part), Service Request Status (Task 3).

**b. Local Events and Announcements Page (Windows Form) — 70 marks.**

- Display upcoming local events and announcements attractively.
- Search functionality to find events by category and date.
- Advanced data structures, such as sorted dictionaries, to optimise event organisation.

### Technical requirements (40 of the 70)

| Requirement | Marks |
|---|---|
| Stacks, queues, priority queues — manage event-related data | 15 |
| Hash tables, dictionaries, sorted dictionaries — organise and retrieve event info | 15 |
| Sets — handle unique categories or dates | 10 |

### Additional recommendation feature (30 marks)

- Analyse user search patterns and preferences.
- Use an appropriate algorithm or data structure to suggest related or recommended events.
- Present recommendations in a user-friendly manner in the application.

**Note:** If the code does not compile and run, no marks are awarded for application functionality.

### Submit for Part 2

1. Source code.
2. Readme with compile, run, and usage instructions.

---

# POE Part 3 — Full Functioning App — 100 marks

Learning Units: all. Focus: basic trees, binary trees, binary search trees, AVL trees, red-black trees, heaps, graphs, graph traversal, minimum spanning trees.

## Implementation (100 marks)

**a.** Windows Form menu for Report Issues (Part 1), Local Events and Announcements (Part 2), Service Request Status (this task).

**b. Service Request Status Page (Windows Form) — 100 marks:**

- Display a well-organised list of submitted service requests including status.
- Allow users to track progress using unique identifiers.
- Use advanced data structures such as graphs, binary search trees, or heaps to manage and display service request information efficiently.

### Technical requirements (50 marks)

| Requirement | Marks |
|---|---|
| Basic trees, binary trees, BSTs, AVL trees, red-black trees | 20 |
| Heaps, graphs, graph traversal, minimum spanning tree | 30 |

### Implementation report (20 marks)

- Detailed readme explaining how to compile, run, and use the programme.
- For each implemented data structure, an in-depth explanation of its role and contribution to the efficiency of the "Service Request Status" feature, with relevant examples.

### Project completion report (20 marks)

- Comprehensive report on the completion of the entire project.
- Challenges faced during Task 3 and how they were overcome.
- Key learnings: new skills, problem-solving approaches, programming techniques.

### Technology recommendations (10 marks)

- Suggest additional technologies or tools that could enhance functionality or performance.
- Justify recommendations based on potential benefits and compatibility with the project.

### Submit for Part 3

1. A Word document containing the report.
2. Source code with the complete functioning application.
3. The readme file.
4. A file listing the updates made based on lecturer feedback.

---

# Appendix A — Marking rubrics

Rubric band labels throughout: *does not meet the required standard / meets the required standard / partially exceeds the required standard / greatly exceeds the required standard*.

## Part 1 — Task 1 (Research)

| Criterion | Marks | Top band requires |
|---|---|---|
| Research: list of five user engagement strategies | 5 | Five well-defined strategies relevant to municipal services applications (0 / 1–2 / 3–4 / 5) |
| Research: explanation and justification of the chosen strategy | 10 | Comprehensive 500-word explanation, clear justification, deep understanding of benefits for municipal services (0–3 / 4–6 / 7–8 / 9–10) |
| Referencing and citations | 5 | Proper referencing and citations using the given article and other relevant sources (0 / 1–2 / 3–4 / 5) |

## Part 1 — Task 2 (Implementation)

| Criterion | Marks | Top band requires |
|---|---|---|
| App functionality: task presentation on startup | 10 | Main menu presented flawlessly on startup, all options working without errors (0–3 / 4–6 / 7–8 / 9–10) |
| App functionality: Report Issues task implementation | 10 | Fully implemented, meeting all requirements without errors (0–3 / 4–6 / 7–8 / 9–10) |
| App functionality: user input for issue details | 10 | Works perfectly without errors (0–3 / 4–6 / 7–8 / 9–10) |
| App functionality: media attachment | 10 | Works flawlessly without errors (0–3 / 4–6 / 7–8 / 9–10) |
| App functionality: implementation of user engagement strategy | 10 | Seamlessly integrated, positively influencing user participation (0–3 / 4–6 / 7–8 / 9–10) |
| App logic: use of appropriate data structures | 5 | A **list** used consistently and appropriately throughout to store user-reported issues (0 / 1–2 / 3–4 / 5) |
| Coding standards: readme file quality | 5 | Excellent readme with all information for compiling, running, and using the software (0 / 1–2 / 3–4 / 5) |
| Design considerations: consistency, clarity, user feedback, responsiveness | 10 | Consistent colour scheme and layout; clear labels, buttons, instructions; effective feedback; responsive across screen sizes (0–3 / 4–6 / 7–8 / 9–10) |
| Additional requirements: form interactions and data handling | 10 | Event handlers implemented seamlessly; appropriate data structures (e.g. List) used efficiently (0–3 / 4–6 / 7–8 / 9–10) |
| **Part 1 total** | **/100** | |

## Part 2 — Task 1

| Criterion | Marks | Bands |
|---|---|---|
| Main Menu (Form) | 30 | 0–8 / 9–16 / 17–20 / 21–30 |
| Technical: stacks, queues, priority queues | 15 | 0–4 / 5–10 / 11–14 / 15 |
| Technical: hash tables, dictionaries, sorted dictionaries | 15 | 0–4 / 5–10 / 11–14 / 15 |
| Technical: sets | 10 | 0–3 / 4–6 / 7–8 / 9–10 |
| Additional: search patterns, smart recommendations | 30 | 0–9 / 10–15 / 16–20 / 21–30 |
| **Part 2 total** | **/100** | |

## POE Part 3 — Task 1

| Criterion | Marks | Bands |
|---|---|---|
| Basic trees, binary trees, BSTs, AVL trees, red-black trees: implementation effectiveness | 20 | 0–5 / 6–10 / 11–15 / 16–20 |
| Heaps, graphs, graph traversal, minimum spanning tree: structures utilisation | 30 | 0–9 / 10–15 / 16–20 / 21–30 |
| Implementation report: readme file quality | 10 | 0–3 / 4–6 / 7–8 / 9–10 |
| Implementation report: data structure explanation | 10 | 0–3 / 4–6 / 7–8 / 9–10 |
| Project completion report: project overview | 10 (bands printed to 30) | 0–10 / 11–20 / 21–26 / 27–30 |
| Project completion report: key learnings | 5 | 0 / 1–2 / 3–4 / 5 |
| Technology recommendations: suggestions | 5 | 0 / 1–2 / 3–4 / 5 |
| Technology recommendations: justification | 5 | 0 / 1–2 / 3–4 / 5 |
| Updates based on feedback: incorporation of feedback | 5 (bands printed to 10) | 0–3 / 4–6 / 7–8 / 9–10 |
| **Total** | **/100** | |

Note: the printed rubric for Part 3 shows mark-band ranges that exceed some stated criterion weights (project overview and feedback rows). Bands are reproduced above as printed in the brief.
