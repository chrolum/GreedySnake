using System;

namespace GreedySnake
{
	class Program
	{
		static void Main(string[] args)
		{
			Game newGame = new Game(20 ,30, 1);
			Console.CursorVisible = false;
			while (true)
			{
				//Console.Clear();
				newGame.Update(newGame.GetActionUserAction());
				//Console.WriteLine("Point: {0}", newGame.getPoint());
				newGame.draw();
				if (newGame.hasFailed())
				{
					Console.WriteLine("Oops! You eat youself!");
					Console.WriteLine("Press any key to restart!");
					Console.ReadKey();
					newGame.Restart();
				}
				else if (newGame.hasWin())
				{
					//Console.WriteLine("Congratulated! You has win the game");
					//Console.WriteLine("Press any key to restart!");
					Console.ReadKey();
					newGame.Restart();
				}
			}
		}
	}
}
