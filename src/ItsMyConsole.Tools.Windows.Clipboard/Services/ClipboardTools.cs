using System.Threading;

namespace ItsMyConsole.Tools.Windows.Clipboard
{
    /// <summary>
    /// Outils d'accès au presse papier Windows
    /// </summary>
    public class ClipboardTools
    {
        internal ClipboardTools() { }

        /// <summary>
        /// Récupérer le texte du presse papier
        /// </summary>
        public string GetText()
        {
            string text = null;
            Thread thread = new Thread(() =>
            {
                text = System.Windows.Forms.Clipboard.GetText();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            return text;
        }

        /// <summary>
        /// Mettre un texte dans le presse papier
        /// </summary>
        /// <param name="text">Le texte à mettre dans le presse papier</param>
        public void SetText(string text)
        {
            Thread thread = new Thread(() =>
            {
                System.Windows.Forms.Clipboard.SetText(text);
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}
