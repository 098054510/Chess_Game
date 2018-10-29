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
            PuttPieces();
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
            if (piece is )
        }
    }
}