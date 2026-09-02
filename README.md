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
