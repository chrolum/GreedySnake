using System;
using System.Collections.Generic;
using System.Collections;
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

	class GlobalData
	{
		public static Dictionary<char, GameDef.GameObj> char2GameObjDict = new Dictionary<char, GameObj>(){
			{'#', GameDef.GameObj.WALL},
			{' ', GameDef.GameObj.AIR},
			{'.', GameDef.GameObj.COIN},
		};

		public static Dictionary<GameDef.GameObj, int> ItemPoint = new Dictionary<GameDef.GameObj, int>()
		{
			{GameDef.GameObj.ALLPE, 300},
			{GameDef.GameObj.COIN, 100}
		};
	}
	
}
