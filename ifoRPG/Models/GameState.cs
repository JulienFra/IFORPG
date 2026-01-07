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
        public (int x, int y) ExitDoorInRoom { get; set; }

        // Suivi de la progression
        public Dictionary<int, bool[]> FloorsProgress { get; set; } = new()
        {
            { 1, new bool[3] }, 
            { 2, new bool[3] }, 
            { 3, new bool[3] }
        };

        // Coordonnées fixes des portes dans le couloir
        private (int x, int y)[] _corridorDoors = { (2, 4), (5, 4), (8, 4) };

        public GameState()
        {
            // Initialisation au lancement
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            Player = new Player(0, 0);
        }

        // --- Logique de navigation ---

        public void EnterRoom(int roomIndex)
        {
            CurrentRoomIndex = roomIndex;
            InCorridor = false;
            
            // Création de la salle spécifique
            CurrentMap = new Map(10, 10, CurrentFloor, roomIndex + 1, false);
            
            // Placement de la porte de sortie et du joueur
            ExitDoorInRoom = (0, CurrentMap.Height / 2);
            Player = new Player(ExitDoorInRoom.x + 1, ExitDoorInRoom.y);
            
            // Génération du Boss
            CurrentBoss = BossFactory.CreateBoss(CurrentFloor, roomIndex + 1);
            ShowBoss = false;
        }

        public void ReturnToCorridor()
        {
            InCorridor = true;
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            
            // Le joueur réapparaît devant la porte qu'il vient de quitter
            Player = new Player(_corridorDoors[CurrentRoomIndex].x, _corridorDoors[CurrentRoomIndex].y + 1);
        }

        public void GoUp()
        {
            CurrentFloor++;
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            Player = new Player(1, 0); // Spawn près de l'escalier de descente
            InCorridor = true;
        }

        public void GoDown()
        {
            CurrentFloor--;
            CurrentMap = new Map(10, 10, CurrentFloor, -1, true);
            Player = new Player(CurrentMap.StairsX, CurrentMap.StairsY - 1); // Spawn près de l'escalier de montée
            InCorridor = true;
        }

        // --- Logique de combat ---

        public void HandleBossDefeat(bool defeated)
        {
            ShowBoss = false;
            
            if (defeated)
            {
                // Marquer la salle comme complétée
                FloorsProgress[CurrentFloor][CurrentRoomIndex] = true;
            }
            else
            {
                // Sanction : réinitialisation de la progression de l'étage actuel
                FloorsProgress[CurrentFloor] = new bool[3];
            }
        }
    }
}