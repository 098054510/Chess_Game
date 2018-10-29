using System;
using System.Collections.Generic;
using System.Text;
using Chess_Game.BattleField;

namespace Chess_Game
{
    class Horse : Piece
    {
        public Horse(BattleField Bat, Color color) : base(Bat, color)
        {
        }

        public override string ToString()
        {
            return "H";
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

            position.SetValues(position.Line - 1, position.Collum - 2);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line - 2, position.Collum - 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line - 2, position.Collum + 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line - 1, position.Collum + 2);
            if (Bat.ValidPosition(position) && CanMove(position){
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line + 1, position.Collum + 2);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line + 2, position.Collum + 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line + 2, position.Collum - 1);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            position.SetValues(position.Line + 1, position.Collum - 2);
            if (Bat.ValidPosition(position) && CanMove(position))
            {
                mat[position.Line, position.Collum] = true;
            }

            return mat;
        }
    }
}