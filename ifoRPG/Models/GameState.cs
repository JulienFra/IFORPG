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
        // --- ChatGPT ---
        public bool ChatGPTPicked { get; set; } = false;
        public (int x, int y) ChatGPTPosition => (8, 1); // Couloir étage 1


        // Limité à 2 étages
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

            // --- ÉTAGE 1 ---
            if (CurrentFloor == 1 && roomIndex == 0) // Jeremy Kairis
            {
                var questionsKairis = new List<Question>
                {
                    new Question("Lequel de ces concepts permet de masquer les détails internes d'une classe ?", new List<string> { "L'encapsulation", "L'héritage", "Le polymorphisme" }, 0),
                    new Question("Qu'est-ce qu'une classe abstraite ?", new List<string> { "Une classe qui ne peut pas être instanciée", "Une classe sans méthodes", "Une classe qui n'a pas de nom" }, 0),
                    new Question("En POO, qu'est-ce que l'héritage ?", new List<string> { "Un mécanisme pour créer une nouvelle classe à partir d'une existante", "La suppression d'objets inutilisés", "La copie d'un fichier source" }, 0),
                    new Question("Quel mot-clé est souvent utilisé pour accéder aux membres de la classe parente ?", new List<string> { "base", "this", "super" }, 0),
                    new Question("Qu'est-ce que le polymorphisme ?", new List<string> { "La capacité d'un objet à prendre plusieurs formes", "La création de plusieurs classes dans un seul fichier", "Le fait de renommer une variable" }, 0)
                };
                CurrentBoss = new Boss("Monsieur Kairis", questionsKairis);
            }
            else if (CurrentFloor == 1 && roomIndex == 1) // Cédric (Réseaux) - REMPLACE NATHALIE
            {
                var questionsReseau = new List<Question>
                {
                    new Question("Combien de couches compte le modèle OSI ?", new List<string> { "7", "5", "4" }, 0),
                    new Question("Quel protocole est utilisé pour attribuer dynamiquement des adresses IP ?", new List<string> { "DHCP", "DNS", "HTTP" }, 0),
                    new Question("Quelle est l'adresse IP de boucle locale (localhost) par défaut ?", new List<string> { "127.0.0.1", "192.168.1.1", "10.0.0.1" }, 0),
                    new Question("À quelle couche du modèle OSI appartient le protocole IP ?", new List<string> { "Couche 3 (Réseau)", "Couche 2 (Liaison)", "Couche 4 (Transport)" }, 0),
                    new Question("Quel port est utilisé par défaut pour le protocole sécurisé HTTPS ?", new List<string> { "443", "80", "22" }, 0)
                };
                CurrentBoss = new Boss("Cédric", questionsReseau);
            }
            else if (CurrentFloor == 1 && roomIndex == 2) // Monsieur Bernair
            {
                var questionsAnalyse = new List<Question>
                {
                    new Question("Dans un diagramme de cas d'utilisation, que représente l'acteur ?", new List<string> { "Une entité externe qui interagit avec le système", "Une base de données", "Une fonction du code" }, 0),
                    new Question("Quel diagramme UML est le plus adapté pour modéliser le cycle de vie d'un objet ?", new List<string> { "Diagramme d'état-transition", "Diagramme de classes", "Diagramme de déploiement" }, 0),
                    new Question("Que signifie l'acronyme UML ?", new List<string> { "Unified Modeling Language", "Universal Model Layout", "Unique Main Logic" }, 0),
                    new Question("Dans un diagramme de classes, comment représente-t-on l'héritage ?", new List<string> { "Une flèche avec un triangle vide", "Un trait pointillé", "Une flèche pleine" }, 0),
                    new Question("Quel concept définit les besoins ce que le système doit faire ?", new List<string> { "Les besoins fonctionnels", "Les besoins techniques", "L'implémentation" }, 0)
                };
                CurrentBoss = new Boss("Monsieur Bernair", questionsAnalyse);
            }
            // --- ÉTAGE 2 ---
            else if (CurrentFloor == 2 && roomIndex == 0) // Monsieur Six
            {
                var questionsSGBD = new List<Question>
                {
                    new Question("Quelle commande SQL permet de supprimer tous les enregistrements d'une table sans supprimer la table elle-même ?", new List<string> { "TRUNCATE", "DELETE TABLE", "DROP" }, 0),
                    new Question("Quel type de jointure retourne toutes les lignes de la table de gauche, même s'il n'y a pas de correspondance à droite ?", new List<string> { "LEFT JOIN", "INNER JOIN", "RIGHT JOIN" }, 0),
                    new Question("Que signifie l'acronyme ACID pour une transaction ?", new List<string> { "Atomicity, Consistency, Isolation, Durability", "Access, Control, Index, Data", "Auto, Commit, Insert, Delete" }, 0),
                    new Question("Quelle clause est utilisée pour filtrer les résultats d'un GROUP BY ?", new List<string> { "HAVING", "WHERE", "ORDER BY" }, 0),
                    new Question("Quelle contrainte assure qu'une colonne ne peut pas avoir de valeurs dupliquées ?", new List<string> { "UNIQUE", "NOT NULL", "CHECK" }, 0)
                };
                CurrentBoss = new Boss("Monsieur Six", questionsSGBD);
            }
            else if (CurrentFloor == 2 && roomIndex == 1) // Monsieur Martin
            {
                var questionsSQL = new List<Question>
                {
                    new Question("Quelle clause est utilisée pour trier les résultats d'une requête ?", new List<string> { "ORDER BY", "SORT BY", "GROUP BY" }, 0),
                    new Question("Quelle fonction permet de compter le nombre de lignes d'un résultat ?", new List<string> { "COUNT()", "SUM()", "TOTAL()" }, 0),
                    new Question("Comment supprimer une table et toute sa structure de la base de données ?", new List<string> { "DROP TABLE", "REMOVE TABLE", "DELETE TABLE" }, 0),
                    new Question("Quel mot-clé permet d'éviter les doublons dans un SELECT ?", new List<string> { "DISTINCT", "UNIQUE", "SINGLE" }, 0),
                    new Question("Quel opérateur est utilisé pour rechercher un modèle spécifique dans une colonne ?", new List<string> { "LIKE", "SEARCH", "MATCH" }, 0)
                };
                CurrentBoss = new Boss("Monsieur Martin", questionsSQL);
            }
            else if (CurrentFloor == 2 && roomIndex == 2) // Christophe
            {
                var questionsJS = new List<Question>
                {
                    new Question("Quelle méthode permet de sélectionner un élément HTML par son identifiant ?", new List<string> { "getElementById()", "querySelector()", "selectId()" }, 0),
                    new Question("Quel mot-clé permet de déclarer une variable constante ?", new List<string> { "const", "let", "var" }, 0),
                    new Question("Comment écrit-on une fonction fléchée ?", new List<string> { "() => {}", "function() => {}", "arrow()" }, 0),
                    new Question("Quelle méthode ajoute un écouteur d'événement ?", new List<string> { "addEventListener()", "onClick()", "attach()" }, 0),
                    new Question("Que retourne 'typeof []' ?", new List<string> { "'object'", "'array'", "'list'" }, 0)
                };
                CurrentBoss = new Boss("Christophe", questionsJS);
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
            if (CurrentFloor < 2) // Sécurité : on ne monte pas au dessus du 2
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
            ShowBoss = false;
            if (defeated)
            {
                FloorsProgress[CurrentFloor][CurrentRoomIndex] = true;
            }
            else
            {
                FloorsProgress[CurrentFloor] = new bool[3];
            }
        }
        public void TryPickupChatGPT()
        {
            if (!ChatGPTPicked &&
                InCorridor &&
                CurrentFloor == 1 &&
                Player.X == ChatGPTPosition.x &&
                Player.Y == ChatGPTPosition.y)
            {
                Player.Inventory.Add(new ChatGPTItem());
                ChatGPTPicked = true;
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
    }
}