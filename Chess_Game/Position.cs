namespace Chess_Game.BattleField
{
    class Position
    {
        public int Line { get; set; }
        public int Collum { get; set; }

        public Position(int Line, int Collum)
        {
            this.Line = Line;
            this.Collum = Collum;
        }

        public void SetValues(int Line, int Collum)
        {
            this.Line = Line;
            this.Collum = Collum;
        }

        public override string ToString()
        {
            return "Line: "
                + Line
                + ", Collum: "
                + Collum;
        }
    }
}