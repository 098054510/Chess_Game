using Chess_Game.BattleField;

namespace Chess
{
    class King : Piece
    {
        private ChessParty chessParty;

        public King(BattleField Bat, Color color, ChessParty chessParty) : base(Bat, color)
        {
            this.chessParty = chessParty;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Bat.piece(position);
            return piece == null || piece.color != color;
        }

        private bool TowertoRoquetest(Position position)
        {
            Piece piece = Bat.piece(position);
            return piece != null && piece is Tower && piece.color == color && piece.Movement == 0;
        }

        public override bool[,] PossiblesMovements()
        {
            bool[,] mat = new bool[Bat.Line, Bat.Collum];

            Position position = new Position(0, 0);

            //Above
            position.SetValues(position.Line - 1, position.Collum);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //NE
            position.SetValues(position.Line - 1, position.Collum + 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //Right
            position.SetValues(position.Line, position.Collum + 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //SE
            position.SetValues(position.Line + 1, position.Collum + 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //Below
            position.SetValues(position.Line + 1, position.Collum);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //SO
            position.SetValues(position.Line + 1, position.Collum - 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //Left
            position.SetValues(position.Line, position.Collum - 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //NO
            position.SetValues(position.Line - 1, position.Collum - 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            //Especial Play Roque
            if (Movement==0 && !chessParty.CheckMate)
            {
                //Especial Play Little Roque
                Position position1 = new Position(position.Line, position.Collum + 3);
                if (TowertoRoquetest(position1))
                {
                    Position p1 = new Position(position.Line, position.Collum + 1);
                    Position p2 = new Position(position.Line, position.Collum + 2);
                    if (Bat.piece(p1)==null && Bat.piece(p2) == null)
                    {
                        mat[position.Line, position.Collum + 2] = true;
                    }
                }

                //Especial Play Big Roque
                Position position2 = new Position(position.Line, position.Collum - 4);
                if (TowertoRoquetest(position2))
                {
                    Position p1 = new Position(position.Line, position.Collum - 1);
                    Position p2 = new Position(position.Line, position.Collum - 2);
                    Position p3 = new Position(position.Line, position.Collum - 3);
                    if (Bat.piece(p1) == null && Bat.piece(p2) == null && Bat.piece(p3) == null)
                    {
                        mat[position.Line, position.Collum - 2] = true;
                    }
                }
            }

            return mat;
        }
    }
}