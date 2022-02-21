![Logo](docs/logo.png)

# ItsMyConsole.Tools.Windows.Clipboard
Outil d'accès au presse papier Windows pour le Framework [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole)

## Sommaire

- [Pourquoi faire ?](#pourquoi-faire-)
- [Getting Started](#getting-started)
- [Comment se servir de l'outil ?](#comment-se-servir-de-loutil-)
- [Rechercher un personnage de Star Wars](#rechercher-un-personnage-de-star-wars)

## Pourquoi faire ?

Vous allez pouvoir étendre le Framework pour application Console .Net [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) avec un outil "d'exemple" qui permet de rechercher des personnages de Star Wars avec The Star Wars API (SWAPI).

L'outil "d'exemple" ```ItsMyConsole.Tools.Template.Example``` met à disposition :
 - Un exemple de structure de projet pour aider à démarrager un outil pour [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole)
 - Exemple d'implementation d'un outil avec la gestion des tests unitaires
 - Exemple de paramètres pour configurer l'outil
 - Exemple d'implementation d'un readme
 - Exemple sur la recherche des personnages de l'univers de Star Wars

## Getting Started

1. Créer un projet **"Application Console .Net"** avec le nom *"MyExampleConsole"*
2. Ajouter [```ItsMyConsole```](https://github.com/dtarroz/ItsMyConsole) au projet depuis le gestionnaire de package NuGet
3. Ajouter ```ItsMyConsole.Tools.Template.Example``` au projet depuis le gestionnaire de package NuGet *[C'est pour l'exemple, ce NuGet n'existe pas]*
4. Dans le projet, modifier la méthode **"Main"** dans le fichier **"Program.cs"** par le code suivant :
```cs
using ItsMyConsole;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ItsMyConsole.Tools.Template.Example;

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
                options.HeaderText = "###########\n#  SWAPI  #\n###########\n";
                options.TrimCommand = true;
            });

            // SWAPI configuration
            ccli.ConfigureSwapi(new SwapiOption { MaxResultSearchPeople = 10 });

            // Display the name of peoples
            // Example : people sky
            ccli.AddCommand("^people (.+)$", RegexOptions.IgnoreCase, async tools => {
                string name = tools.CommandMatch.Groups[1].Value;
                List<People> peoples = await tools.Swapi().SearchPeople(name);
                if (peoples.Count > 0)
                    foreach (People people in peoples)
                        Console.WriteLine(people.Name);
                else
                    Console.WriteLine("Aucun personnage trouvé");
            });

            await ccli.RunAsync();
        }
    }
}
```

Voici le résultat attendu lors de l'utilisation de la Console :

![MyExampleProject](docs/MyExampleProject.png)

Dans cet exemple de code on a configuré avec ```Configure```, le prompt d’attente des commandes ```options.Prompt```, la présence d'un saut de ligne entre les saisies ```options.LineBreakBetweenCommands``` et l’en-tête affichée au lancement ```options.HeaderText```. 

On ajoute la configuration de SWAPI avec ```ConfigureSwapi``` et on lui renseigne le nombre de résultat maximum retourné ```MaxResultSearchPeople```.

Puis avec ```AddCommand```, on a ajouté un pattern d’interprétation des lignes de commande ```^people (.+)$``` *(commence par **"people"** et suivi du texte pour la recherche)* qui est insensible à la casse ```RegexOptions.IgnoreCase```.

Lors de l'exécution de la Console, si on saisit une commande qui commence par **"people"** avec un texte à la suite, il lancera l'implémentation de l'action associée. Dans cet exemple, il récupère le texte de recherche en utilisant ```tools.CommandMatch``` depuis les outils disponibles *(résultat du Match de l'expression régulière)* pour lui permet de récupérer les informations sur les personnages de Star Wars associés avec ```tools.Swapi().SearchPeople```. Avec les informations récupérées, il affiche leur nom dans la Console.

Maintenant que l'on a configuré la Console et l'implémentation de l'action associée au pattern ```^people (.+)$```, l'utilisation de ```RunAsync``` lance la mise en attente d'une saisie de commande par l'utilisateur.

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
