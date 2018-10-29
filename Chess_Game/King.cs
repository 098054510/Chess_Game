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
            return piece != null && piece is 
        }
    }
}
