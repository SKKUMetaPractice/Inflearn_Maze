﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    internal class Player
    {
        public int PosY { get; private set; } // private를 붙여 Player class내부에서만 값을 고칠 수 있게 한다.
        public int PosX { get; private set; }
        Random _random = new Random();

        Board _board;

        public void Initialize(int posY, int posX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //여기에다가 0.1초마다 실행될 로직 넣어줌.
                int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0: // 상
                        if (PosY-1 >=0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY -= 1;
                        break;
                    case 1: //하
                        if (PosY + 1 <= _board.size && _board.Tile[PosY+1, PosX] == Board.TileType.Empty)
                            PosY += 1;
                        break;
                    case 2: // 좌
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX-1] == Board.TileType.Empty)
                            PosX -= 1;
                        break; 
                    case 3: // 우
                        if (PosX +1 <= _board.size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX += 1;
                        break;
                }
            }
        }
    }
}
