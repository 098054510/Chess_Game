namespace Chess_Game.BattleField
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int Movement { get; protected set; }

    }
}
