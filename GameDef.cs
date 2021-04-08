using System;
using System.Collections.Generic;
using System.Text;

namespace GameDef
{
	enum GameObj
	{
		AIR = 0,
		WALL,
		ALLPE,
		COIN,
		SNAKEBODY,
		SNAKEHEAD
	}

	enum Action
	{
		LEFT = 0,
		RIGHT,
		UP,
		DOWN,
		RESTART,
		NOACTION
	}
}
