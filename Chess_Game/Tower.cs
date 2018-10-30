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
            position.SetValues(position.Line - 1, position.Collum);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.Line = position.Line - 1;
            }

            //Below
            position.SetValues(position.Line + 1, position.Collum);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.Line = position.Line + 1;
            }

            //Right
            position.SetValues(position.Line, position.Collum + 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position). color != color)
                {
                    break;
                }
                position.Collum = position.Collum + 1;
            }

            //Left
            position.SetValues(position.Line, position.Collum - 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.Collum = position.Collum - 1;
            }

            return mat;
        }
    }
}
