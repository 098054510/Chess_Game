using System;
using System.Collections.Generic;
using System.Text;
using Chess_Game.BattleField;

namespace Chess
{
    class ChessParty
    {
        public BattleField Bat { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captureds;
        public bool CheckMate { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }
        
        public ChessParty()
        {
            Bat = new BattleField(8, 8);
            Round = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            CheckMate = false;
            VulnerableEnPassant = null;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            PuttPiece();
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captureds)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        private Color Enemy(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in PiecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool InCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BattlefieldlException("No have any " + color + " King on the BattleField.");
            }
            foreach (Piece x in PiecesInGame(Enemy(color)))
            {
                bool[,] mat = x.PossiblesMovements();
                if (mat[K.position.Line, K.position.Collum])
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckMateTest(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossiblesMovements();
                for (int i = 0; i < Bat.Line; i++)
                {
                    for (int j = 0; j < Bat.Collum; j++)
                    {
                        if (mat[i, j])
                        {
                            Position source = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(source, destiny);
                            bool CheckTest = InCheck(color);
                            UndoMovement(source, destiny, capturedPiece);
                            if (!CheckTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PuttNewPiece(char Collum, int Line, Piece piece)
        {
            Bat.PuttPiece(piece, new Chess_Position(Collum, Line).toPosition());
            pieces.Add(piece);
        }

        public Piece ExecuteMovement(Position source, Position destiny)
        {
            Piece piece = Bat.RemovePiece(source);
            piece.ToIncreaseMovement();
            Piece capturedPiece = Bat.RemovePiece(destiny);
            Bat.PuttPiece(piece, destiny);
            if (capturedPiece != null)
            {
                captureds.Add(capturedPiece);
            }

            //Especia play Little Roque
            if (piece is King && destiny.Collum == source.Collum + 2)
            {
                Position sourceT = new Position(source.Line, source.Collum + 3);
                Position destinyT = new Position(source.Line, source.Collum + 1);
                Piece T = Bat.RemovePiece(sourceT);
                T.ToIncreaseMovement();
                Bat.PuttPiece(T, destinyT);
            }

            //Especial Play Big Roque
            if (piece is King && destiny.Collum == source.Collum - 2)
            {
                Position sourceT = new Position(source.Line, source.Collum - 4);
                Position destinyT = new Position(source.Line, source.Collum - 1);
                Piece T = Bat.RemovePiece(sourceT);
                T.ToIncreaseMovement();
                Bat.PuttPiece(T, destinyT);
            }

            //Especial Play En Passant
            if (piece is Pawn)
            {
                if (source.Collum != destiny.Collum && capturedPiece == null)
                {
                    Position posP;
                    if (piece.color == Color.White)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Collum);
                    }
                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Collum);
                    }
                    capturedPiece = Bat.RemovePiece(posP);
                    captureds.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMovement(Position source, Position destiny, Piece capturedPiece)
        {
            Piece piece = Bat.RemovePiece(destiny);
            piece.ToDecryptMovement();
            if (capturedPiece != null)
            {
                Bat.PuttPiece(capturedPiece, destiny);
                captureds.Remove(capturedPiece);
                Bat.PuttPiece(piece, source);
            }

            //Especial Play Little Roque
            if (piece is King && destiny.Collum == source.Collum + 2)
            {
                Position sourceT = new Position(source.Line, source.Collum + 3);
                Position destinyT = new Position(source.Line, source.Collum + 1);
                Piece T = Bat.RemovePiece(destinyT);
                T.ToDecryptMovement();
                Bat.PuttPiece(T, sourceT);
            }

            //Especial Play Big Roque
            if (piece is King && destiny.Collum == source.Collum - 2)
            {
                Position sourceT = new Position(source.Line, source.Collum + 4);
                Position destinyT = new Position(source.Line, source.Collum + 1);
                Piece T = Bat.RemovePiece(destinyT);
                T.ToDecryptMovement();
                Bat.PuttPiece(T, sourceT);
            }

            //Especial Play En Passant
            if (piece is Pawn)
            {
                if (source.Collum != destiny.Collum && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Bat.RemovePiece(destiny);
                    Position posP;
                    if (piece.color == Color.White)
                    {
                        posP = new Position(3, destiny.Collum);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Collum);
                    }
                    Bat.PuttPiece(pawn, posP);
                }
            }
        }

        public void MakeAMove(Position source, Position destiny)
        {
            Piece capturedPiece = ExecuteMovement(source, destiny);

            if (InCheck(CurrentPlayer))
            {
                UndoMovement(source, destiny, capturedPiece);
                throw new BattlefieldlException("You can't putt in Check.");
            }
        }
    }
}