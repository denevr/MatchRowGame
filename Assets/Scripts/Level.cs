using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Level
    {
        public Level(int levelNumber, int gridWidth, int gridHeight, int moveCount, string[] gridSequence)
        {
            level_number = levelNumber;
            grid_width = gridWidth;
            grid_height = gridHeight;
            move_count = moveCount;
            grid = gridSequence;
        }
        public int level_number { get; set; }
        public int grid_width  { get; set; }
        public int grid_height  { get; set; }
        public int move_count  { get; set; }
        public string[] grid  { get; set; }
    }

