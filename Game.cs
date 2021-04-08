using System;
using System.Collections.Generic;
using System.Text;

/*
 * orignal postion start from left-up coner
 * 
 */

namespace GreedySnake
{
	
	class Game
	{
		// store scence data

		Stage stage;
		private int SenceWidth, SenceHeigth;
		private Snake snake;
		private int gamePoint = 0;

		private bool isDeath = false;
		private bool isWin = false;

		public bool hasFailed() {return this.isDeath;}
		public bool hasWin() {return this.isWin;}

		GameDef.GameObj[,] running_map;//no sanke here
		char[,] draw_map;
		public int getPoint() {return this.gamePoint;}


		public Game(int heigth, int width, int stage_no = 1)
		{
			SenceWidth = width;
			SenceHeigth = heigth;
			snake = new Snake();
			this.running_map = new GameDef.GameObj[heigth, width];
			this.draw_map = new char[heigth, width];


			//TODO: tmp_test test finished remove it
			running_map[10, 10] = GameDef.GameObj.ALLPE;
			running_map[10, 11] = GameDef.GameObj.COIN;
			running_map[10, 12] = GameDef.GameObj.WALL;
		}
		

		//for print
		private readonly Dictionary<GameDef.GameObj, char> output = new Dictionary<GameDef.GameObj, char>()
		{
			{GameDef.GameObj.AIR, ' '},
			{GameDef.GameObj.ALLPE, 'O'},
			{GameDef.GameObj.COIN, 'C'},
			{GameDef.GameObj.SNAKEBODY, 'S'},
			{GameDef.GameObj.SNAKEHEAD, 'H'},
			{GameDef.GameObj.WALL, '#'},
		};

		private readonly Dictionary<ConsoleKey, GameDef.Action> ActionMap = new Dictionary<ConsoleKey, GameDef.Action>()
		{
			{ConsoleKey.UpArrow, GameDef.Action.UP},
			{ConsoleKey.DownArrow, GameDef.Action.DOWN},
			{ConsoleKey.LeftArrow, GameDef.Action.LEFT},
			{ConsoleKey.RightArrow, GameDef.Action.RIGHT},
			{ConsoleKey.R, GameDef.Action.RESTART}
		};

		public GameDef.Action GetActionUserAction()
		{
			if (Console.KeyAvailable)  
    		{  
        		ConsoleKeyInfo key = Console.ReadKey(true);  
				var k = key.Key;
				if (!ActionMap.ContainsKey(k))
					return GameDef.Action.NOACTION;
				return ActionMap[k];
			}
			return GameDef.Action.NOACTION;
		}

		public void Update(GameDef.Action action)
		{
			switch (action)
			{
				case GameDef.Action.LEFT:
				case GameDef.Action.RIGHT:
				case GameDef.Action.DOWN:
				case GameDef.Action.UP:
					snake.move(action);
					break;
				case GameDef.Action.RESTART:
					Restart();
					return;
				default:
					snake.move();
					break;
			}
			// move first, then check the new head position eating
			EatingLogic();
			System.Threading.Thread.Sleep(150);
		}

		public void Restart()
		{
			//TODO
		}
		public void draw()
		{
			//draw fix item
			// Console.Clear();
			for (int r = 0; r < this.SenceHeigth; r++)
			{
				for (int c = 0; c < this.SenceWidth; c++)
				{
					draw_map[r,c] = output[running_map[r, c]];
				}
			}
			//draw snake
			foreach (var s_node in this.snake._snakeList)
			{
				int c = s_node._h;
				int r = s_node._v;
				draw_map[r, c] = output[GameDef.GameObj.SNAKEBODY];
			}
			//redraw sanke head
			draw_map[this.snake._snakeList.First.Value._v, this.snake._snakeList.First.Value._h] = output[GameDef.GameObj.SNAKEHEAD];

			//real draw
			Console.SetCursorPosition(0, 0);
			for (int r = 0; r < this.SenceHeigth; r++)
			{
				for (int c = 0; c < this.SenceWidth; c++)
				{
					Console.SetCursorPosition(c, r);
					Console.Write("{0}", draw_map[r, c]);
				}
				Console.WriteLine();
			}
		} 

		private void EatingLogic()
		{
			// check curr snake head(has updated!)
			int head_h = snake._snakeList.First.Value._h;
			int head_v = snake._snakeList.First.Value._v;
			switch(running_map[head_v, head_h])
			{
				case GameDef.GameObj.ALLPE:
					GrewSnake();
					AddPoint(GameDef.GameObj.ALLPE);
					break;
				case GameDef.GameObj.COIN:
					AddPoint(GameDef.GameObj.COIN);
					break;
				case GameDef.GameObj.WALL:
					isDeath = true;
					break;
				default:
					EatingItselfCheck(); // eating itself only happend while the item is air
					break;
			}
		}

		private void GrewSnake()
		{	
			var coords = this.snake.GetNewSnakeNodeCoords();
			SnakeNode newTail = new SnakeNode(coords.Item1, coords.Item2);
			this.snake._snakeList.AddLast(newTail);
		}
		private void AddPoint(GameDef.GameObj obj)
		{
			// int p = 0;
			if (!GameDef.GlobalData.ItemPoint.ContainsKey(obj))
				return;
			
			this.gamePoint += GameDef.GlobalData.ItemPoint[obj];
		}

		private void EatingItselfCheck()
		{
			int head_h = this.snake._snakeList.First.Value._h;
			int head_v = this.snake._snakeList.First.Value._v;

			var curr = this.snake._snakeList.First;
			curr = curr.Next;

			while (curr != null)
			{
				// eat it-self
				if (head_h == curr.Value._h && head_v == curr.Value._v)
				{
					this.isDeath = true;
					return;
				}
				// // hit wall
				// if (running_map[head_v, head_h] == GameDef.GameObj.WALL)
				// {
				// 	this.isDeath = true;
				// 	return;
				// }
				curr = curr.Next;
			}
		}
	}

	class SnakeNode
	{
		public int _h { get; set; }
		public int _v { get; set; }

		public SnakeNode(int v, int h)
		{
			_h = h;
			_v = v;
		}
	}
	class Snake
	{
		public LinkedList<SnakeNode> _snakeList {get;}
		private GameDef.Action dir;

		public Snake(int head_h = 7, int head_v = 7, int len = 4, GameDef.Action dir = GameDef.Action.RIGHT)
		{
			_snakeList = new LinkedList<SnakeNode>();
			_snakeList.AddFirst(new SnakeNode(head_v, head_h));
			//default put snake herizion from head to left
			for (int i = 1; i < len; i++)
			{
				_snakeList.AddLast(new SnakeNode(head_v, head_h-i));
			}
			this.dir = dir;
		}

		/*
		* Update snake linked list position record
		* No vaild move check!
		* No tail adding logic
		*/
		public void move(GameDef.Action d)
		{
			int dh = 0, dv = 0;
			
			// if (d == GameDef.Action.LEFT && thi)
			//TODO: 反方向禁止移动

			switch (d)
			{
				case GameDef.Action.LEFT:
					dh = -1;
					break;
				case GameDef.Action.RIGHT:
					dh = 1;
					break;
				case GameDef.Action.UP:
					dv = -1;
					break;
				case GameDef.Action.DOWN:
					dv = 1;
					break;
				default:
					dv = 0;
					dh = 0;
					break;
			}
			int prev_h = _snakeList.First.Value._h;
			int prev_v = _snakeList.First.Value._v;

			int curr_h = prev_h + dh;
			int curr_v = prev_v + dv;

			_snakeList.First.Value = new SnakeNode(curr_v, curr_h);

			var curr = _snakeList.First.Next;
			while (curr != null)
			{
				curr_h = curr.Value._h;
				curr_v = curr.Value._v;
				curr.Value = new SnakeNode(prev_v, prev_h);
				prev_h = curr_h;
				prev_v = curr_v;
				curr = curr.Next;
			}
			this.dir = d;

			// TODO 更新屏幕范围
		}

		public (int, int) GetNewSnakeNodeCoords()
		{
			var LastSnakeNode = this._snakeList.Last;
			var LastPrevSnakeNode = LastSnakeNode.Previous;

			int dh = LastSnakeNode.Value._h - LastPrevSnakeNode.Value._h;
			int dv = LastSnakeNode.Value._v - LastPrevSnakeNode.Value._v;

			int LastNodeH = LastSnakeNode.Value._h;
			int LastNodeV = LastSnakeNode.Value._v;
		
			return (LastNodeV + dv, LastNodeH + dh);
		}
		public void move()
		{
			move(this.dir);
		}
	}
}
