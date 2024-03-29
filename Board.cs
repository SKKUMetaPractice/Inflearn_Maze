﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Maze
{
    class Board
    {
        // public int[] _data = new int[25];
        public TileType[,] Tile { get; private set; }
        public int size { get; private set; }
        const char CIRCLE = '\u25cf';
        public int DestY { get; private set; }
        public int DestX { get; private set; }

        Player _player;

        // enum: 상수 숫자를 좀더 의미있게 사용하기 위함
        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;
            Tile = new TileType[size, size];
            this.size = size;
            _player = player;

            DestY = size - 2;
            DestX = size - 2;

            generateBinaryTree();
            //generateSideWinder();
        }

        void generateSideWinder() {  // sidewinder 미로 생성 알고리즘 참고.

            // 테두리 및 짝수번째 막아버리는 작업
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1 || i % 2 == 0 || j % 2 == 0)
                    {
                        Tile[i, j] = TileType.Wall;
                    }
                    else
                    {
                        Tile[i, j] = TileType.Empty;
                    }
                }
            }

            // 랜덤으로 길 뚫기. 점 중 아무거나 하나 선택해서 길 뚫기 진행.
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                int cnt = 1;
                for (int j = 0; j < size; j++)
                {
                    // 첫 행 열은 모두 벽이 되도록
                    if (i % 2 == 0 || j % 2 == 0)
                        continue;

                    // 랜덤으로 점 중 아무 점을 선택하여 뚫리도록 설정. & 테두리 뚫리지 않도록 설정
                    if (rnd.Next(0, 2) == 0)
                    {
                        if(j != size-1)
                            Tile[i, j + 1] = TileType.Empty;
                        cnt++;
                    }
                    else {
                        int rndIndex = rnd.Next(0, cnt);
                        if(i != size - 1)
                            Tile[i + 1, j-rndIndex*2] = TileType.Empty;
                        cnt = 1;
                    }
                }
            }
        }

        void generateBinaryTree()
        {

            // 테두리 및 짝수번째 막아버리는 작업
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0 || i == size - 1 || j == 0 || j == size - 1 || i % 2 == 0 || j % 2 == 0)
                    {
                        Tile[i, j] = TileType.Wall;
                    }
                    else
                    {
                        Tile[i, j] = TileType.Empty;
                    }
                }
            }

            // 랜덤으로 길 뚫기. 알고리즘이 단순할 수록 단점이 존재.
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // 첫 행 열은 모두 벽이 되도록
                    if (i % 2 == 0 || j % 2 == 0)
                        continue;

                    // 외곽에 있는 애들은 무조건 한 방향으로만 가도록 설정 => 마지막 -1 라인이 뚫릴 수 밖에.
                    if (j == size - 2)
                    {
                        Tile[i + 1, j] = TileType.Empty;
                        continue;
                    }

                    if (i == size - 2)
                    {
                        Tile[i, j + 1] = TileType.Empty;
                        continue;
                    }

                    // 랜덤으로 한 쪽 방향으로 뚫리도록 설정.
                    if (rnd.Next(0, 2) == 0)
                    {
                        Tile[i, j + 1] = TileType.Empty;
                    }
                    else { Tile[i + 1, j] = TileType.Empty; }
                }
            }
        }

        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // player의 좌표를 받아 조건문으로 처리한다.
                    // play의 좌표는 Player class에서 처리
                    if(i == _player.PosY && j == _player.PosX)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }else if(i == DestY && j == DestX )
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        Console.ForegroundColor = GetTileColor(Tile[i, j]);
                    }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
