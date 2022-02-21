![Logo](docs/logo.png)

# ItsMyConsole.Tools.Windows.Clipboard
Outil d'accès au presse papier Windows pour le Framework [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole)

## Sommaire

- [Pourquoi faire ?](#pourquoi-faire-)
- [Getting Started](#getting-started)
- [Comment se servir de l'outil ?](#comment-se-servir-de-loutil-)
- [Rechercher un personnage de Star Wars](#rechercher-un-personnage-de-star-wars)

## Pourquoi faire ?

Vous allez pouvoir étendre le Framework pour application Console .Net [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) avec un outil d'accès au presse papier Windows.

L'outil ```ItsMyConsole.Tools.Windows.Clipboard``` met à disposition :
 - La lecture un texte contenu dans le presse papier Windows
 - L'insertion d'un texte dans le presse papier Windows
 - Il permet de ne pas modifier l'état de cloisonnement du thread principal pour accéder au presse papier

## Getting Started

1. Créer un projet **"Application Console .Net"** avec le nom *"MyExampleConsole"*
2. Ajouter [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) au projet depuis le gestionnaire de package NuGet
3. Ajouter ```ItsMyConsole.Tools.Windows.Clipboard``` au projet depuis le gestionnaire de package NuGet
4. Dans le projet, modifier la méthode **"Main"** dans le fichier **"Program.cs"** par le code suivant :
```cs
using ItsMyConsole;
using ItsMyConsole.Tools.Windows.Clipboard;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
```

Voici le résultat attendu lors de l'utilisation de la Console :

![MyExampleProject](docs/MyExampleProject.png)

Dans cet exemple de code on a configuré avec ```Configure```, le prompt d’attente des commandes ```options.Prompt```, la présence d'un saut de ligne entre les saisies ```options.LineBreakBetweenCommands``` et l’en-tête affichée au lancement ```options.HeaderText```. 

Puis avec le premier ```AddCommand```, on a ajouté un pattern d’interprétation des lignes de commande ```^get$``` *(seulement "get")* qui est insensible à la casse ```RegexOptions.IgnoreCase```.

Et avec le deuximème ```AddCommand```, on a ajouté un pattern d’interprétation des lignes de commande ```^set (.+)$``` *(commence par "set" et suivi d'un texte)* qui est insensible à la casse ```RegexOptions.IgnoreCase```.

Lors de l'exécution de la Console, si on saisit une commande qui commence par **"set"** avec le texte à mettre dans le presse papier, il lancera l'implémentation de l'action associée (le deuxième ```AddCommand```). Dans cet exemple, il récupère le texte en utilisant ```tools.CommandMatch``` depuis les outils disponibles *(résultat du Match de l'expression régulière)*. Avec le texte récupéré, il l'insert dans le presse papier Windows en utilisant ```tools.Clipboard().SetText```.

Si on saisit la commande **"get"**, il lancera l'implémentation de l'action associée (le premier ```AddCommand```). Il lit le presse papier en utilisant ```tools.Clipboard().GetText``` et il affiche le texte obtenu.

Maintenant que l'on a configuré la Console et l'implémentation des actions, l'utilisation de ```RunAsync``` lance la mise en attente d'une saisie de commande par l'utilisateur.

## Comment se servir de l'outil ?

Vous pouvez accéder à l'outil "d'exemple" SWAPI lorsque vous ajoutez une interprétation de commande avec ```AddCommand```.

```cs
ConsoleCommandLineInterpreter ccli = new ConsoleCommandLineInterpreter();

// Add command
ccli.AddCommand("<PATERN>", tools => 
{
    List<People> peoples = await tools.Swapi().SearchPeople("<NAME>");
});
```

Vous devez ajouter ```using ItsMyConsole.Tools.Template.Example;``` pour avoir accès a l'outil "d'exemple" SWAPI depuis ```tools``` de ```AddCommand```.

## Rechercher un personnage de Star Wars

Vous pouvez rechercher un personnage par son nom en utilisant ```SearchPeople```.

| Propriété | Description |
| :-------- | :---------- |
| name | Le nom des personnages à chercher |

```cs
ccli.AddCommand("<PATERN>", tools => 
{
    List<People> peoples = await tools.Swapi().SearchPeople("<NAME>");
});
```

Vous avez en retour un objet de type ```People```.

| Nom de la propriété | Description |
| :------------------ | :---------- |
| Id | L'identifiant du personnage |
| Name | Le nom du personnage |
