using System;
using System.Collections.Generic;
using System.Text;
using Chess_Game.BattleField;

namespace Chess
{
    class Screen
    {
        public static void ShowTheGame(ChessParty party)
        {
            ShowTheBattleField(party.Bat);
            Console.WriteLine();
            ShowCapturedPieces(party);
            Console.WriteLine();
            Console.WriteLine("Round: " + party.Round);
            if (!party.Finished)
            {
                Console.WriteLine("Waiting a Movement: " + party.CurrentPlayer);
                if (party.Check)
                {
                    Console.WriteLine("Check.");
                }
            }
            else
            {
                Console.WriteLine("CheckMate.");
                Console.WriteLine("Winner: " + party.CurrentPlayer);
            }
        }

        public static void ShowCapturedPieces(ChessParty party)
        {
            Console.WriteLine("Captured Pieces: ");
            Console.WriteLine("Whites: ");
            ShowGroupofPieces(party.capturedPieces(Color.White));
            Console.WriteLine();
            Console.WriteLine("Blacks: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowGroupofPieces(party.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ShowGroupofPieces(HashSet<Piece> PGroup)
        {
            Console.Write("[");
            foreach (Piece x in PGroup)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void ShowTheBattleField(BattleField Bat)
        {
            for (int i = 0; i<Bat.Line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j<Bat.Collum; j++)
                {
                    ShowPiece(Bat.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine(" a b c d e f g h");
        }

        public static void ShowTheBattleField(BattleField Bat, bool[,] PossiblesPositions)
        {
            ConsoleColor StockBackground = Console.BackgroundColor;
            ConsoleColor ChangedBackground = ConsoleColor.DarkGray;

            for (int i = 0; i<Bat.Line; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j<Bat.Collum; j++)
                {
                    if (PossiblesPositions[i, j])
                    {
                        Console.BackgroundColor = ChangedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = StockBackground;
                    }
                    ShowPiece(Bat.piece(i, j));
                    Console.BackgroundColor = StockBackground;
                }
                Console.WriteLine(" a b c d e f g h");
                Console.BackgroundColor = StockBackground;
            }
        }

        public static Chess_Position ReadChessPosition()
        {
            string s = Console.ReadLine();
            char Collum = s[0];
            int Line = int.Parse(s[1] + "");
            return new Chess_Position(Collum, Line);
        }

        public static void ShowPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.WriteLine("- ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }
}
