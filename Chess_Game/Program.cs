using System;
using Chess_Game.BattleField;
using Chess;

namespace Chess_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessParty chessParty = new ChessParty();

                while (!chessParty.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.ShowTheGame(chessParty);

                        Console.WriteLine();
                        Console.Write("Source: ");
                        Position source = Screen.ReadChessPosition().toPosition();
                        chessParty.ValidSourcePosition(source);

                        bool[,] possiblesPositions = chessParty.Bat.piece(source).PossiblesMovements();

                        Console.Clear();
                        Screen.ShowTheBattleField(chessParty.Bat, possiblesPositions);


                        Console.Clear();
                        Screen.ShowTheBattleField(chessParty.Bat, possiblesPositions);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadChessPosition().toPosition();
                        chessParty.ValidDestinyPosition(source, destiny);

                        chessParty.MakeAMove(source, destiny);
                    }
                    catch (BattlefieldlException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.ShowTheGame(chessParty);
            }
            catch(BattlefieldlException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
