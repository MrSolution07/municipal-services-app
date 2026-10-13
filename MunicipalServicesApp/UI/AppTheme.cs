using System.Drawing;

namespace MunicipalServicesApp.UI
{
    /// <summary>
    /// Single source of the colour scheme and fonts so every form looks the same.
    /// Palette chosen for civic / government readability: high-contrast navy, blue accent,
    /// light grey background and white cards. All colours are used consistently by MainMenuForm,
    /// ReportIssuesForm and the new LocalEventsForm.
    /// </summary>
    internal static class AppTheme
    {
        public static readonly Color Header = Color.FromArgb(15, 23, 42);         // #0F172A
        public static readonly Color HeaderText = Color.White;
        public static readonly Color Background = Color.FromArgb(248, 250, 252);    // #F8FAFC
        public static readonly Color Card = Color.White;
        public static readonly Color Accent = Color.FromArgb(3, 105, 161);          // #0369A1
        public static readonly Color AccentText = Color.White;
        public static readonly Color BodyText = Color.FromArgb(2, 6, 23);          // #020617
        public static readonly Color MutedText = Color.FromArgb(71, 85, 105);      // #475569
        public static readonly Color Border = Color.FromArgb(226, 232, 240);       // #E2E8F0
        public static readonly Color Disabled = Color.FromArgb(222, 226, 232);
        public static readonly Color Danger = Color.FromArgb(220, 38, 38);         // #DC2626

        public static readonly Font TitleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font SectionFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font SmallFont = new Font("Segoe UI", 9F, FontStyle.Regular);
    }
}
