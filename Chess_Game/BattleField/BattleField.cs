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
    }
}