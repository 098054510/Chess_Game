using Chess_Game.BattleField;

namespace Chess
{
    class Chess_Position
    {
        public char Collum { get; set; }
        public int Line { get; set; }

        public Chess_Position(char Collum, int Line)
        {
            this.Collum = Collum;
            this.Line = Line;
        }

        public Position toPosition()
        {
            return new Position(8 - Line, Collum - 'a');
        }

        public override string ToString()
        {
            return "" + Collum + Line;
        }
    }
}
