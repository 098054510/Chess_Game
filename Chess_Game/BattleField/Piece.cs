namespace Chess_Game.BattleField
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int Movement { get; protected set; }
        public BattleField Bat { get; set; }

        public Piece(BattleField Bat, Color color)
        {
            this.position = null;
            this.Bat = Bat;
            this.color = color;
            this.Movement = 0;
        }

        public void ToIncreaseMovement()
        {
            Movement++;
        }

        public void ToDecryptMovement()
        {
            Movement--;
        }

        public bool possibleMovementExists()
        {
            bool[,] mat = PossiblesMovements();
            for (int i=0; i<Bat.Line; i++)
            {
                for (int j=0; i<Bat.Collum; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovement(Position position)
        {
            return PossiblesMovements()[position.Line, position.Collum];
        }

        public abstract bool[,] PossiblesMovements();
    }
}