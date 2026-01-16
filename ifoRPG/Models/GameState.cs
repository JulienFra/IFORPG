using System;
using System.Collections.Generic;
using System.Linq;

namespace ifoRPG.Models
{
    public class GameState
    {
        // État du jeu
        public int CurrentFloor { get; set; } = 1;
        public int CurrentRoomIndex { get; set; } = -1;
        public bool InCorridor { get; set; } = true;
        public Map CurrentMap { get; set; }
        public Player Player { get; set; }
        public Boss? CurrentBoss { get; set; }
        public bool ShowBoss { get; set; }
        public bool IntroductionRead { get; set; }
        public (int x, int y) ExitDoorInRoom { get; set; }
        public bool ChatGPTPicked { get; set; } = false;
        public (int x, int y) ChatGPTPosition => (8, 1);
        public bool DurumPicked { get; set; } = false;
        public (int x, int y) DurumPosition => (8, 1);

        public Dictionary<int, bool[]> FloorsProgress { get; set; } = new()
        {
            { 1, new bool[3] },
            { 2, new bool[3] }
        };

        private (int x, int y)[] _corridorDoors = { (2, 4), (5, 4), (8, 4) };

        public GameState()
        {
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            Player = new Player(0, 0);
        }

        public void EnterRoom(int roomIndex)
        {
            CurrentRoomIndex = roomIndex;
            InCorridor = false;
            IntroductionRead = false;

            CurrentMap = new Map(10, 10, CurrentFloor, roomIndex + 1, false);
            ExitDoorInRoom = (0, CurrentMap.Height / 2);
            Player.X = ExitDoorInRoom.x + 1;
            Player.Y = ExitDoorInRoom.y;

            if (CurrentFloor == 1 && roomIndex == 0) // Jeremy Kairis
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Qu'est-ce qu'une classe en POO ?", new List<string> { "Un modèle pour créer des objets", "Une fonction", "Une variable" }, 0),
                    new Question("Que signifie POO ?", new List<string> { "Programmation Orientée Objet", "Programmation Optimale Organisée", "Projet Objet Orienté" }, 0),
                    new Question("Comment crée-t-on un objet en C# ?", new List<string> { "new MaClasse()", "create MaClasse()", "MaClasse.new()" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Lequel de ces concepts permet de masquer les détails internes d'une classe ?", new List<string> { "L'encapsulation", "L'héritage", "Le polymorphisme" }, 0),
                    new Question("Qu'est-ce qu'une classe abstraite ?", new List<string> { "Une classe qui ne peut pas être instanciée", "Une classe sans méthodes", "Une classe qui n'a pas de nom" }, 0),
                    new Question("En POO, qu'est-ce que l'héritage ?", new List<string> { "Un mécanisme pour créer une nouvelle classe à partir d'une existante", "La suppression d'objets inutilisés", "La copie d'un fichier source" }, 0),
                    new Question("Quel mot-clé est souvent utilisé pour accéder aux membres de la classe parente ?", new List<string> { "base", "this", "super" }, 0),
                    new Question("Qu'est-ce que le polymorphisme ?", new List<string> { "La capacité d'un objet à prendre plusieurs formes", "La création de plusieurs classes dans un seul fichier", "Le fait de renommer une variable" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Quelle est la différence entre surcharge et redéfinition ?", new List<string> { "Surcharge = même nom différents paramètres, Redéfinition = override", "Aucune différence", "Surcharge utilise new, Redéfinition utilise base" }, 0),
                    new Question("Qu'est-ce que le principe SOLID en POO ?", new List<string> { "5 principes de conception orientée objet", "Un design pattern", "Un framework C#" }, 0),
                    new Question("Qu'est-ce qu'une interface en POO ?", new List<string> { "Un contrat que les classes doivent respecter", "Une classe sans constructeur", "Une méthode abstraite" }, 0),
                    new Question("Que permet le pattern Singleton ?", new List<string> { "Avoir une seule instance d'une classe", "Créer plusieurs objets identiques", "Hériter de plusieurs classes" }, 0),
                    new Question("Qu'est-ce que l'injection de dépendances ?", new List<string> { "Fournir les dépendances d'une classe de l'extérieur", "Créer des dépendances dans une classe", "Supprimer les dépendances" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "Developer")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "DataAnalyst")
                    questionsChoisies = questionsDifficiles;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Monsieur Kairis", questionsChoisies);
            }
            else if (CurrentFloor == 1 && roomIndex == 1) // Cédric
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Que signifie IP ?", new List<string> { "Internet Protocol", "Internal Program", "Information Process" }, 0),
                    new Question("Combien y a-t-il d'octets dans une adresse IPv4 ?", new List<string> { "4", "8", "16" }, 0),
                    new Question("Quel appareil connecte plusieurs réseaux ?", new List<string> { "Routeur", "Switch", "Hub" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Combien de couches compte le modèle OSI ?", new List<string> { "7", "5", "4" }, 0),
                    new Question("Quel protocole est utilisé pour attribuer dynamiquement des adresses IP ?", new List<string> { "DHCP", "DNS", "HTTP" }, 0),
                    new Question("Quelle est l'adresse IP de boucle locale (localhost) par défaut ?", new List<string> { "127.0.0.1", "192.168.1.1", "10.0.0.1" }, 0),
                    new Question("À quelle couche du modèle OSI appartient le protocole IP ?", new List<string> { "Couche 3 (Réseau)", "Couche 2 (Liaison)", "Couche 4 (Transport)" }, 0),
                    new Question("Quel port est utilisé par défaut pour le protocole sécurisé HTTPS ?", new List<string> { "443", "80", "22" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Qu'est-ce qu'un masque de sous-réseau CIDR /24 ?", new List<string> { "255.255.255.0", "255.255.0.0", "255.0.0.0" }, 0),
                    new Question("Quel protocole utilise TCP pour le transfert de fichiers sécurisé ?", new List<string> { "SFTP", "FTP", "TFTP" }, 0),
                    new Question("Que signifie NAT en réseau ?", new List<string> { "Network Address Translation", "New Access Technology", "Network Application Transfer" }, 0),
                    new Question("Quelle est la plage d'adresses IP privées de classe A ?", new List<string> { "10.0.0.0 à 10.255.255.255", "172.16.0.0 à 172.31.255.255", "192.168.0.0 à 192.168.255.255" }, 0),
                    new Question("Quel protocole permet la résolution inverse DNS ?", new List<string> { "PTR", "A", "CNAME" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "NetworkEngineer")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "Developer" || Player.Class == "DataAnalyst")
                    questionsChoisies = questionsNormales;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Cédric", questionsChoisies);
            }
            else if (CurrentFloor == 1 && roomIndex == 2) // Monsieur Bernair
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Que signifie UML ?", new List<string> { "Unified Modeling Language", "Universal Model Layout", "Unique Main Logic" }, 0),
                    new Question("À quoi sert un diagramme de classes ?", new List<string> { "Modéliser la structure des classes", "Afficher le flux de données", "Définir les interfaces" }, 0),
                    new Question("Qu'est-ce qu'un acteur dans un diagramme de cas d'utilisation ?", new List<string> { "Une personne ou système externe", "Une classe", "Une fonction" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Dans un diagramme de cas d'utilisation, que représente l'acteur ?", new List<string> { "Une entité externe qui interagit avec le système", "Une base de données", "Une fonction du code" }, 0),
                    new Question("Quel diagramme UML est le plus adapté pour modéliser le cycle de vie d'un objet ?", new List<string> { "Diagramme d'état-transition", "Diagramme de classes", "Diagramme de déploiement" }, 0),
                    new Question("Que signifie l'acronyme UML ?", new List<string> { "Unified Modeling Language", "Universal Model Layout", "Unique Main Logic" }, 0),
                    new Question("Dans un diagramme de classes, comment représente-t-on l'héritage ?", new List<string> { "Une flèche avec un triangle vide", "Un trait pointillé", "Une flèche pleine" }, 0),
                    new Question("Quel concept définit les besoins ce que le système doit faire ?", new List<string> { "Les besoins fonctionnels", "Les besoins techniques", "L'implémentation" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Quelle est la différence entre agrégation et composition ?", new List<string> { "La composition implique une dépendance forte", "Aucune différence", "L'agrégation est plus forte" }, 0),
                    new Question("Que représente une cardinalité 0..* dans un diagramme de classes ?", new List<string> { "Aucune ou plusieurs instances", "Exactement zéro instance", "Au moins une instance" }, 0),
                    new Question("Qu'est-ce qu'un diagramme de séquence ?", new List<string> { "Il montre les interactions temporelles entre objets", "Il affiche la structure des classes", "Il définit les états d'un objet" }, 0),
                    new Question("Dans quel diagramme UML trouve-t-on des swimlanes ?", new List<string> { "Diagramme d'activité", "Diagramme de classes", "Diagramme de composants" }, 0),
                    new Question("Que signifie une relation de dépendance en UML ?", new List<string> { "Une classe utilise temporairement une autre", "Une classe hérite d'une autre", "Une classe contient une autre" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "NetworkEngineer")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "Developer" || Player.Class == "DataAnalyst")
                    questionsChoisies = questionsNormales;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Monsieur Bernair", questionsChoisies);
            }
            else if (CurrentFloor == 2 && roomIndex == 0) // Monsieur Six
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Que signifie SGBD ?", new List<string> { "Système de Gestion de Base de Données", "Système de Gestion de Bureau", "Service de Gestion de Données" }, 0),
                    new Question("Quelle commande SQL permet d'ajouter des données ?", new List<string> { "INSERT", "ADD", "PUT" }, 0),
                    new Question("Que fait la commande SELECT ?", new List<string> { "Récupère des données", "Supprime des données", "Modifie des données" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Quelle commande SQL permet de supprimer tous les enregistrements d'une table sans supprimer la table elle-même ?", new List<string> { "TRUNCATE", "DELETE TABLE", "DROP" }, 0),
                    new Question("Quel type de jointure retourne toutes les lignes de la table de gauche, même s'il n'y a pas de correspondance à droite ?", new List<string> { "LEFT JOIN", "INNER JOIN", "RIGHT JOIN" }, 0),
                    new Question("Que signifie l'acronyme ACID pour une transaction ?", new List<string> { "Atomicity, Consistency, Isolation, Durability", "Access, Control, Index, Data", "Auto, Commit, Insert, Delete" }, 0),
                    new Question("Quelle clause est utilisée pour filtrer les résultats d'un GROUP BY ?", new List<string> { "HAVING", "WHERE", "ORDER BY" }, 0),
                    new Question("Quelle contrainte assure qu'une colonne ne peut pas avoir de valeurs dupliquées ?", new List<string> { "UNIQUE", "NOT NULL", "CHECK" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Quelle est la différence entre DELETE et TRUNCATE ?", new List<string> { "DELETE peut être rollback, TRUNCATE non", "Aucune différence", "TRUNCATE est plus lent" }, 0),
                    new Question("Qu'est-ce qu'un index clustered ?", new List<string> { "Un index qui définit l'ordre physique des données", "Un index secondaire", "Un index sur plusieurs colonnes" }, 0),
                    new Question("Que garantit le niveau d'isolation SERIALIZABLE ?", new List<string> { "Aucune anomalie de concurrence", "Lecture rapide", "Écriture rapide" }, 0),
                    new Question("Qu'est-ce qu'un deadlock en base de données ?", new List<string> { "Deux transactions se bloquent mutuellement", "Une transaction qui ne termine jamais", "Une erreur de syntaxe SQL" }, 0),
                    new Question("Que fait la commande EXPLAIN en SQL ?", new List<string> { "Affiche le plan d'exécution d'une requête", "Explique les erreurs", "Documente une table" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "DataAnalyst")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "Developer")
                    questionsChoisies = questionsDifficiles;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Monsieur Six", questionsChoisies);
            }
            else if (CurrentFloor == 2 && roomIndex == 1) // Monsieur Martin
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Que fait ORDER BY en SQL ?", new List<string> { "Trie les résultats", "Filtre les résultats", "Compte les résultats" }, 0),
                    new Question("Quelle fonction compte le nombre de lignes ?", new List<string> { "COUNT()", "SUM()", "TOTAL()" }, 0),
                    new Question("Comment éviter les doublons dans un SELECT ?", new List<string> { "DISTINCT", "UNIQUE", "NO_DUPLICATES" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Quelle clause est utilisée pour trier les résultats d'une requête ?", new List<string> { "ORDER BY", "SORT BY", "GROUP BY" }, 0),
                    new Question("Quelle fonction permet de compter le nombre de lignes d'un résultat ?", new List<string> { "COUNT()", "SUM()", "TOTAL()" }, 0),
                    new Question("Comment supprimer une table et toute sa structure de la base de données ?", new List<string> { "DROP TABLE", "REMOVE TABLE", "DELETE TABLE" }, 0),
                    new Question("Quel mot-clé permet d'éviter les doublons dans un SELECT ?", new List<string> { "DISTINCT", "UNIQUE", "SINGLE" }, 0),
                    new Question("Quel opérateur est utilisé pour rechercher un modèle spécifique dans une colonne ?", new List<string> { "LIKE", "SEARCH", "MATCH" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Quelle est la différence entre WHERE et HAVING ?", new List<string> { "WHERE filtre avant GROUP BY, HAVING après", "Aucune différence", "HAVING est plus rapide" }, 0),
                    new Question("Que fait la fonction COALESCE ?", new List<string> { "Retourne la première valeur non NULL", "Combine des chaînes", "Compte les valeurs NULL" }, 0),
                    new Question("Qu'est-ce qu'une sous-requête corrélée ?", new List<string> { "Une sous-requête qui référence la requête externe", "Une requête rapide", "Une requête dans un JOIN" }, 0),
                    new Question("Que fait l'opérateur UNION ALL ?", new List<string> { "Combine les résultats en gardant les doublons", "Supprime les doublons", "Fait une jointure" }, 0),
                    new Question("Qu'est-ce qu'une CTE (Common Table Expression) ?", new List<string> { "Une requête temporaire nommée", "Une table temporaire", "Un index" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "DataAnalyst")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "Developer" || Player.Class == "NetworkEngineer")
                    questionsChoisies = questionsDifficiles;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Monsieur Martin", questionsChoisies);
            }
            else if (CurrentFloor == 2 && roomIndex == 2) // Christophe
            {
                var questionsFaciles = new List<Question>
                {
                    new Question("Que signifie JS ?", new List<string> { "JavaScript", "JavaSource", "JustScript" }, 0),
                    new Question("Comment afficher un message dans la console ?", new List<string> { "console.log()", "print()", "echo()" }, 0),
                    new Question("Quel mot-clé déclare une variable ?", new List<string> { "var", "variable", "dim" }, 0)
                };

                var questionsNormales = new List<Question>
                {
                    new Question("Quelle méthode permet de sélectionner un élément HTML par son identifiant ?", new List<string> { "getElementById()", "querySelector()", "selectId()" }, 0),
                    new Question("Quel mot-clé permet de déclarer une variable constante ?", new List<string> { "const", "let", "var" }, 0),
                    new Question("Comment écrit-on une fonction fléchée ?", new List<string> { "() => {}", "function() => {}", "arrow()" }, 0),
                    new Question("Quelle méthode ajoute un écouteur d'événement ?", new List<string> { "addEventListener()", "onClick()", "attach()" }, 0),
                    new Question("Que retourne 'typeof []' ?", new List<string> { "'object'", "'array'", "'list'" }, 0)
                };

                var questionsDifficiles = new List<Question>
                {
                    new Question("Qu'est-ce qu'une closure en JavaScript ?", new List<string> { "Une fonction qui a accès aux variables de son scope parent", "Une fonction fermée", "Une boucle infinie" }, 0),
                    new Question("Que fait Promise.all() ?", new List<string> { "Attend que toutes les promises soient résolues", "Résout la première promise", "Annule toutes les promises" }, 0),
                    new Question("Quelle est la différence entre == et === ?", new List<string> { "=== vérifie le type, == non", "Aucune différence", "== est plus strict" }, 0),
                    new Question("Qu'est-ce que le hoisting en JavaScript ?", new List<string> { "Les déclarations sont remontées en haut du scope", "L'optimisation du code", "La compilation" }, 0),
                    new Question("Que fait async/await ?", new List<string> { "Simplifie l'utilisation des promises", "Crée des threads", "Accélère le code" }, 0)
                };

                List<Question> questionsChoisies;
                if (Player.Class == "Developer")
                    questionsChoisies = questionsFaciles;
                else if (Player.Class == "DataAnalyst" || Player.Class == "NetworkEngineer")
                    questionsChoisies = questionsDifficiles;
                else
                    questionsChoisies = questionsNormales;

                CurrentBoss = new Boss("Christophe", questionsChoisies);
            }

            ShowBoss = false;
        }

        public void ReturnToCorridor()
        {
            InCorridor = true;
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            Player.X = _corridorDoors[CurrentRoomIndex].x;
            Player.Y = _corridorDoors[CurrentRoomIndex].y + 1;
        }

        public void GoUp()
        {
            if (CurrentFloor < 2)
            {
                CurrentFloor++;
                InCorridor = true;
                CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
                Player.X = 0;
                Player.Y = 1;
            }
        }

        public void GoDown()
        {
            if (CurrentFloor > 1)
            {
                CurrentFloor--;
                InCorridor = true;
                CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
                Player.X = CurrentMap.StairsX;
                Player.Y = CurrentMap.StairsY - 1;
            }
        }

       public void HandleBossDefeat(bool defeated)
{
            if (defeated)
            {
                ShowBoss = false;
                FloorsProgress[CurrentFloor][CurrentRoomIndex] = true;
            }
            else
            {
                FloorsProgress[CurrentFloor] = new bool[3];
                CurrentBoss?.Reset();
                IntroductionRead = false;
            }
        }

        public void TryPickupChatGPT()
        {
            if (!ChatGPTPicked)
            {
                Player.Inventory.Add(new ChatGPTItem());
                ChatGPTPicked = true;
                Console.WriteLine("Item ajouté à l'inventaire !");
            }
        }

        public void UseChatGPTOnBoss()
        {
            if (CurrentBoss != null && Player.Inventory.HasChatGPT())
            {
                Player.Inventory.UseChatGPT();
                CurrentBoss.ForceDefeat();
            }
        }

        public void TryPickupDurum()
        {
            if (!DurumPicked)
            {
                Player.Inventory.Add(new DurumItem());
                DurumPicked = true;
            }
        }
    }
}