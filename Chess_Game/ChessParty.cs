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
                   Console.WriteLine(x);
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

            Piece piece = Bat.piece(destiny);

            //Especial Play Promotion
            if (piece is Pawn)
            {
                if ((piece.color == Color.White && destiny.Line == 0) || (piece.color == Color.Black && destiny.Line == 7))
                {
                    piece = Bat.RemovePiece(destiny);
                    pieces.Remove(piece);
                    Piece HairsprayQueen = new HairSprayQueen(Bat, piece.color);
                    pieces.Add(HairsprayQueen);
                }
            }

            if (InCheck(Enemy(CurrentPlayer)))
            {
                CheckMate = true;
            }
            else
            {
                CheckMate = false;
            }

            if (CheckMateTest(Enemy(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Round++;
                ChangePlayer();
            }

            //Especial Play En Passant
            if (piece is Pawn && (destiny.Line == source.Line - 2 || destiny.Line == source.Line + 2))
            {
                VulnerableEnPassant = piece;
            }
            else
            {
                VulnerableEnPassant = null;
            }
        }

        public void ValidSourcePosition(Position position)
        {
            if (Bat.piece(position) == null)
            {
                throw new BattlefieldlException("There is no part in the chosen home position.");
            }

            if (CurrentPlayer != Bat.piece(position).color)
            {
                throw new BattlefieldlException("The chosen piece of origin is not yours.");
            }

            if (!Bat.piece(position).possibleMovementExists())
            {
                throw new BattlefieldlException("There are no possible moves for the chosen source part.");
            }
        }

        public void ValidDestinyPosition(Position source, Position destiny)
        {
            if (!Bat.piece(source).PossibleMovement(destiny))
            {
                throw new BattlefieldlException("Invalid Destiny Position.");
            }
        }

        private void PuttPieces()
        {
            PuttNewPiece('a', 1, new Tower(Bat, Color.White));
            PuttNewPiece('b', 1, new Horse(Bat, Color.White));
            PuttNewPiece('c', 1, new Bishop(Bat, Color.White));
            PuttNewPiece('d', 1, new HairSprayQueen(Bat, Color.White));
            PuttNewPiece('e', 1, new King(Bat, Color.White, this));
            PuttNewPiece('f', 1, new Bishop(Bat, Color.White));
            PuttNewPiece('g', 1, new Horse(Bat, Color.White));
            PuttNewPiece('h', 1, new Tower(Bat, Color.White));
            PuttNewPiece('a', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('b', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('c', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('d', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('e', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('f', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('g', 2, new Pawn(Bat, Color.White, this));
            PuttNewPiece('h', 2, new Pawn(Bat, Color.White, this));

            PuttNewPiece('a', 8, new Tower(Bat, Color.Black));
            PuttNewPiece('b', 8, new Horse(Bat, Color.Black));
            PuttNewPiece('c', 8, new Bishop(Bat, Color.Black));
            PuttNewPiece('d', 8, new HairSprayQueen(Bat, Color.Black));
            PuttNewPiece('e', 8, new King(Bat, Color.Black, this));
            PuttNewPiece('f', 8, new Bishop(Bat, Color.Black));
            PuttNewPiece('g', 8, new Horse(Bat, Color.Black));
            PuttNewPiece('h', 8, new Tower(Bat, Color.Black));
            PuttNewPiece('a', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('b', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('c', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('d', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('e', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('f', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('g', 7, new Pawn(Bat, Color.Black, this));
            PuttNewPiece('h', 7, new Pawn(Bat, Color.Black, this));
        }
    }
}