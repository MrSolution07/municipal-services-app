using System.Drawing;

namespace MunicipalServicesApp.UI
{
    /// <summary>
    /// Single source of the colour scheme and fonts so every form looks the same.
    /// </summary>
    internal static class AppTheme
    {
        public static readonly Color Header = Color.FromArgb(17, 45, 78);
        public static readonly Color HeaderText = Color.White;
        public static readonly Color Background = Color.FromArgb(244, 246, 249);
        public static readonly Color Accent = Color.FromArgb(0, 122, 94);
        public static readonly Color AccentText = Color.White;
        public static readonly Color BodyText = Color.FromArgb(33, 37, 41);
        public static readonly Color MutedText = Color.FromArgb(90, 98, 108);

        public static readonly Font TitleFont = new Font("Segoe UI", 16F, FontStyle.Bold);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        public static readonly Font SectionFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        public static readonly Font BodyFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Bold);
    }
}
