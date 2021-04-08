using System;

using System.Collections.Generic;
using System.Text;

namespace GreedySnake
{
    class StageManager
    {
        // public static Stage LoadStageFromFile(string fileName = "stage1.txt")
        // {
        //     File
        //     Stage stage = new Stage();
            
        //     return stage;
        // }
    }

    class Stage
    {
        public GameDef.GameObj[,] orignal_stage_map;
        public GameDef.GameObj[,] running_stage_map;
    }
}