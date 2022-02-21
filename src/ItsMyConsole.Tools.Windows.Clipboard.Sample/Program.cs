using ItsMyConsole;
using ItsMyConsole.Tools.Windows.Clipboard;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace MyExampleConsole
{
    class Program
    {
        static async Task Main() {
            ConsoleCommandLineInterpreter ccli = new ConsoleCommandLineInterpreter();

            // Console configuration
            ccli.Configure(options => {
                options.Prompt = ">> ";
                options.LineBreakBetweenCommands = true;
                options.HeaderText = "#######################\n#  Windows Clipboard  #\n#######################\n";
                options.TrimCommand = true;
            });

            // Display text from clipboard
            ccli.AddCommand("^get$", RegexOptions.IgnoreCase, tools => {
                string text = tools.Clipboard().GetText();
                Console.WriteLine($"Get Clipboard : {text}");
            });

            // Set text in clipboard
            // Example : set NEW_TEXT
            ccli.AddCommand("^set (.+)$", RegexOptions.IgnoreCase, tools => {
                string text = tools.CommandMatch.Groups[1].Value;
                tools.Clipboard().SetText(text);
            });

            await ccli.RunAsync();
        }
    }
}
