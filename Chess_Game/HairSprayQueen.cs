using Chess_Game.BattleField;

namespace Chess
{
    class HairSprayQueen : Piece
    {
        public HairSprayQueen(BattleField Bat, Color color) : base(Bat, color)
        {
        }

        public override string ToString()
        {
            return "HQ";
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

            //Left
            position.SetValues(position.Line, position.Collum - 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
            }

            //Right
            position.SetValues(Bat.Line, position.Collum + 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line, position.Collum + 1);
            }

            //Above
            position.SetValues(position.Line - 1, position.Collum);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Collum);
            }

            //Bellow
            position.SetValues(position.Line + 1, position.Collum);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Collum);
            }

            //NO
            position.SetValues(position.Line - 1, position.Collum - 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Collum - 1);
            }

            //NE
            position.SetValues(position.Line - 1, position.Collum + 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line - 1, position.Collum + 1);
            }

            //SE
            position.SetValues(position.Line + 1, position.Collum + 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Collum + 1);
            }

            //SO
            position.SetValues(position.Line + 1, position.Collum - 1);
            while (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
                if (Bat.piece(position) != null && Bat.piece(position).color != color)
                {
                    break;
                }
                position.SetValues(position.Line + 1, position.Collum - 1);
            }

            return mat;
        }
    }
}