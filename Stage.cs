using System;

using System.Collections.Generic;
using System.Text;

namespace GreedySnake
{
    class StageManager
    {
        public static Stage LoadStageFromFile(string fileName = "map1")
        {
            
            Stage stage = new Stage();
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            //get map size
            line = file.ReadLine();
            string[] size = line.Split(" ");

            int map_v_size = Int32.Parse(size[0]);
            int map_h_size = Int32.Parse(size[1]);

            stage.Initial(map_v_size, map_h_size);

            int v_idx = 0;
            int h_idx = 0;
            while ((line = file.ReadLine()) != null)
            {
                GameDef.GameObj obj = GameDef.GameObj.AIR;

                foreach (char c in line)
                {
                    if (GameDef.GlobalData.char2GameObjDict.ContainsKey(c))
                    {
                        obj = GameDef.GlobalData.char2GameObjDict[c];
                    }
                    stage.orignal_stage_map[v_idx, h_idx++] = obj;
                    stage.running_stage_map[v_idx, h_idx] = obj;
                }
                v_idx++;
            }

            return stage;
        }
    }

    class Stage
    {
        // for restrat
        public GameDef.GameObj[,] orignal_stage_map;
        public GameDef.GameObj[,] running_stage_map;

        public void Initial(int v_size, int h_size)
        {
            this.orignal_stage_map = new GameDef.GameObj[v_size, h_size];
            this.running_stage_map = new GameDef.GameObj[v_size, h_size];
        }
    }
}