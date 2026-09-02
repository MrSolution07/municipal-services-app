# Windows VM test (Part 1)

This project is a .NET Framework 4.8 WinForms app. It will not F5 on macOS.

## Once per VM

1. Install Visual Studio 2022 Community.
2. Workload: **.NET desktop development**.
3. Individual component if prompted: **.NET Framework 4.8 targeting pack**.

## Each test

1. Copy the repo onto the VM, or clone it.
2. Open `MunicipalServicesApp.sln`.
3. Press F5.
4. Confirm the main menu shows three tasks and that the last two are disabled.
5. Open Report Issues.
6. Leave location empty and press Submit — a warning MessageBox must appear.
7. Fill location, category, and description. Watch the progress bar move.
8. Attach an image. Preview should appear.
9. Attach a PDF. File name should appear; preview may stay hidden.
10. Cancel the file dialog — the form must not crash.
11. Submit. Note the reference number. Main menu count must increase.
12. Back to Main Menu must return without an exception.

Do not upload until every step above works.
