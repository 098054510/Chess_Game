using Chess_Game.BattleField;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(BattleField Bat, Color color) : base(Bat, color)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position position)
        {
            Piece piece = Bat.piece(position);
            return piece == null || piece.color != color;
        }

        public override bool[,] PossibleMovement()
        {
            bool[,] mat = new bool[Bat.Line, Bat.Collum];

            Position position = new Position(0, 0);

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