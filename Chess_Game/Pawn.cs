using Chess_Game.BattleField;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessParty chessParty;

        public Pawn(BattleField Bat, Color color, ChessParty chessParty) : base(Bat, color)
        {
            this.chessParty = chessParty;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool EnemySpotted(Position position)
        {
            Piece piece = Bat.piece(position);
            return piece != null && piece.color != color;
        }

        private bool Free(Position position)
        {
            return Bat.piece(position) == null;
        }

        public override bool[,] PossiblesMovements()
        {
            bool[,] mat = new bool[Bat.Line, Bat.Collum];

            Position position = new Position(0, 0);

            if (color == Color.White)
            {
                position.SetValues(position.Line - 1, position.Collum);
                if (Bat.ValidPosition(position) && Free(position))
                {
                    mat[position.Line, position.Collum] = true;
                }

                position.SetValues(position.Line - 2, position.Collum - 1);
                Position p2 = new Position(position.Line - 1, position.Collum);
                if (Bat.ValidPosition(p2) && Free(p2) && Bat.ValidPosition(position) && Movement == 0)
                {

                }

                position.SetValues(position.Line - 1, position.Collum - 1);
                if (Bat.ValidPosition(position) && EnemySpotted(position))
                {
                    mat[position.Line, position.Collum] = true;
                }

                position.SetValues(position.Line - 1, position.Collum + 1);
                if (Bat.ValidPosition(position) && EnemySpotted(position))
                {
                    mat[position.Line, position.Collum] = true;
                }

                //Especial Play En Passant
                if (position.Line == 3)
                {
                    Position Left = new Position(position.Line, position.Collum - 1);
                    if (Bat.ValidPosition(Left) && EnemySpotted(Left) && Bat.piece(Left) == chessParty.VulnerableEnPassant)
                    {
                        mat[Left.Line - 1, Left.Collum] = true;
                    }

                    Position Right = new Position(position.Line, position.Collum + 1);
                    if (Bat.ValidPosition(Right) && EnemySpotted(Right) && Bat.piece(Right) == chessParty.VulnerableEnPassant)
                    {
                        mat[Right.Line - 1, Right.Collum] = true;
                    }
                }

                else
                {
                    position.SetValues(position.Line + 1, position.Collum);
                    if (Bat.ValidPosition(position) && Free(position))
                    {
                        mat[position.Line, position.Collum] = true;
                    }

                    position.SetValues(position.Line + 2, position.Collum);
                    Position pp2 = new Position(position.Line + 1, position.Collum);
                    if (Bat.ValidPosition(position) && Free(position))
                    {
                        mat[position.Line, position.Collum] = true;
                    }

                    position.SetValues(position.Line + 1, position.Collum);
                    if (Bat.ValidPosition(position) && Free(position))
                    {
                        mat[position.Line, position.Collum] = true;
                    }

                    position.SetValues(position.Line + 1, position.Collum);
                    if (Bat.ValidPosition(position) && Free(position))
                    {
                        mat[position.Line, position.Collum] = true;
                    }
                }

                //Especial Play En Passant
                if (position.Line == 4)
                {
                    Position Left = new Position(position.Line, position.Collum - 1);
                    if (Bat.ValidPosition(Left) && EnemySpotted(Left) && Bat.piece(Left) == chessParty.VulnerableEnPassant)
                    {
                        mat[Left.Line + 1, Left.Collum] = true;
                    }

                    Position Right = new Position(position.Line, position.Collum + 1);
                    if (Bat.ValidPosition(Right) && EnemySpotted(Right) && Bat.piece(Right) == chessParty.VulnerableEnPassant)
                    {
                        mat[Right.Line + 1, Left.Collum] = true;
                    } 
                }
            }

            return mat;
        }
    }
}
