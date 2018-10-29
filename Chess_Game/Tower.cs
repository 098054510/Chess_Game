using Chess_Game.BattleField;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(BattleField Bat, Color color) : base(Bat, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Bat.piece(position);
            return piece == null || piece.color != color;
        }

        public override bool[,] PossiblesMovements()
        {
            bool[,] mat = new bool[Bat.Line, Bat.Collum];

            Position position = new Position(0, 0);

            //Above
        }
    }
}
