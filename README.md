# 🎮 ifoRPG

ifoRPG est un **jeu RPG en 2D** développé en **Blazor WebAssembly**, se déroulant dans une école fictive inspirée d’IFOSUP.  
Le joueur incarne un étudiant qui doit **réussir ses examens**, **vaincre des boss sous forme de QCM**, explorer les étages et gérer des objets pour terminer ses **2 années d’études**.

---

## 🏫 Univers du jeu

- Le jeu se déroule dans un bâtiment scolaire composé de **2 étages**
- Chaque étage représente **une année scolaire**
- Chaque étage contient :
  - Un **couloir central**
  - **3 salles de boss** (examens)
  - Un **escalier** vers l’étage suivant (débloqué après avoir réussi tous les boss)

🎯 **Objectif final** : réussir les 2 étages sans redoubler.

---

## 🧑‍🎓 Gameplay

### 🚶 Déplacements
- **Z Q S D** ou **flèches directionnelles**
- Déplacements libres (monde semi-ouvert)
- Les interactions se font simplement en **marchant sur les éléments**

### 🗺️ Exploration
- Les salles restent accessibles même après avoir vaincu un boss
- Les boss vaincus **disparaissent définitivement**
- Les escaliers permettent de :
  - Monter à l’étage suivant
  - Redescendre à l’étage précédent (sans reset)

---

## 👹 Boss & Examens (QCM)

- Chaque étage contient **3 boss**
- Chaque boss est un **QCM de 5 questions**
- Conditions de réussite :
  - ✅ **4 bonnes réponses sur 5** pour réussir
- En cas d’échec :
  - ❌ L’étage entier est réinitialisé (boss + progression)

---

## 🧳 Objets & Inventaire

- Des objets peuvent être ramassés dans les salles
- Exemple :
  - 🤖 **Licence ChatGPT**
    - Permet de **tricher sur un boss** (réussite instantanée)
    - Objet consommable (1 seule utilisation)

📦 L’inventaire est affiché à gauche de l’écran.

---

## 📖 Quest Book

- Affiché à droite de l’écran
- Indique l’objectif actuel :
  - Exemple : *Réussir l’étage 1*
- Une quête est validée automatiquement quand :
  - Les **3 boss de l’étage sont vaincus**

---

## 🖥️ Interface

- 🗺️ Map centrée
- 🎒 Inventaire à gauche
- 📖 Quest Book à droite
- 🧠 QCM affiché à droite de la map
- 🎨 Graphismes en **pixel art / chibi**

---

## ⚙️ Technique

- **Framework** : Blazor WebAssembly
- **Langage** : C#
- **Architecture** : Programmation Orientée Objet
  - Encapsulation
  - Héritage
  - Polymorphisme
- Séparation logique :
  - `Models/` → Player, Map, Boss, Inventory, Items…
  - `Pages/` → Game.razor

---

## 🚀 Lancer le projet

1. Ouvrir le projet avec **Visual Studio**
2. Vérifier que le projet est bien en **Blazor WebAssembly**
3. Lancer avec **F5**

## 🧩 Diagramme de classes – ifoRPG

Ce diagramme présente l’architecture orientée objet du jeu **ifoRPG**.  
Il illustre les relations entre le joueur, l’état du jeu, les cartes, les boss, les objets et la progression.

### 🧠 Description

- **GameState** : cœur du jeu, gère l’état global (étages, salles, cartes, boss, progression).
- **Player** : représente le joueur et sa position dans le monde.
- **Inventory / Item** : gestion des objets (ex : ChatGPT).
- **Map / Tile** : représentation de l’environnement de jeu sous forme de grille.
- **Boss / Question** : système d’examens sous forme de QCM.
- **Floor / Room** : organisation de la progression par étage et par salle.

### 📐 Diagramme UML

> Le diagramme ci-dessous est généré avec **Mermaid** et affiché automatiquement par GitHub.

```mermaid
classDiagram
direction LR

class GameState {
    +int CurrentFloor
    +int CurrentRoomIndex
    +bool InCorridor
    +Map CurrentMap
    +Player Player
    +Boss CurrentBoss
    +bool ShowBoss
    +bool IntroductionRead
    +Dictionary<int, bool[]> FloorsProgress
}

class Player {
    +int X
    +int Y
    +Inventory Inventory
}

class Inventory {
    +List~Item~ Items
    +Add(item)
    +HasChatGPT()
    +UseChatGPT()
}

class Item {
    <<abstract>>
    +string Name
}

class ChatGPTItem

Item <|-- ChatGPTItem
Player --> Inventory
Inventory --> Item

class Map {
    +int Width
    +int Height
    +Tile[,] Tiles
    +int FloorNumber
    +int RoomNumber
}

class Tile {
    +TileType Type
    +bool IsWalkable
}

class TileType {
    <<enum>>
    Ground
    Window
    Bench
    PC
    Wall
    Board
}

Map --> Tile
Tile --> TileType

class Boss {
    +string Name
    +List~Question~ Questions
    +int CorrectAnswers
    +bool Defeated
}

class Question {
    +string Text
    +List~string~ Answers
    +int CorrectIndex
}

Boss --> Question

class Floor {
    +int FloorNumber
    +List~Room~ Rooms
}

class Room {
    +int RoomNumber
    +Boss Boss
}

Floor --> Room
Room --> Boss

GameState --> Player
GameState --> Map
GameState --> Boss
GameState --> Floor
