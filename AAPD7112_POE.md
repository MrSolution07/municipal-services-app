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
