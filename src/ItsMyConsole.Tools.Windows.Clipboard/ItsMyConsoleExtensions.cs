namespace ItsMyConsole.Tools.Windows.Clipboard
{
    /// <summary>
    /// Extension de ItsMyConsole pour inclure les outils d'accès au presse papier Windows 
    /// </summary>
    public static class ItsMyConsoleExtensions
    {
        /// <summary>
        /// L'accès au presse papier Windows
        /// </summary>
        /// <param name="commandTools">Les outils de commandes pour accéder au presse papier Windows</param>
        public static ClipboardTools Clipboard(this CommandTools commandTools) {
            return new ClipboardTools();
        }
    }
}
