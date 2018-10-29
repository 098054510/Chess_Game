using System;

namespace Chess_Game.BattleField
{
    class BattleField
    {
        public int Line { get; set; }
        public int Collum { get; set; }
        private Piece[,] pieces;

        public BattleField(int Line, int Collum)
        {
            this.Line = Line;
            this.Collum = Collum;
            pieces = new Piece[Line, Collum];
        }

        public Piece piece(int Line, int Collum)
        {
            return pieces[Line, Collum];
        }

        public Piece piece(Position position)
        {
            return pieces[position.Line, position.Collum];
        }

        public bool PieceExists(Position position)
        {
            ValidPosition(position);
            return piece(position) != null;
        }

        public void PutPiece(Piece p, Position position)
        {
            if (PieceExists(position))
            {
                throw new BattlefieldlException("There is already a piece there.");
            }
            pieces[position.Line, position.Collum] = p;
            p.position = position;
        }

        public Piece RemovePiece(Position position)
        {
            if (piece(position) == null)
            {
                return null;
            }
            Piece aux = piece(position);
            aux.position = null;
            pieces[position.Line, position.Collum] = null;
            return aux;
        }

        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Line || position.Collum < 0 || position.Collum >= Collum)
            {
                return false;
            }
            return true;
        }

        public void PositionValidation(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BattlefieldlException("Invalid Position.");
            }
        }
    }
}