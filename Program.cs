using System;

namespace Maze
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.CursorVisible = false; // 콘솔의 커서 안보이도록 설정
        //    int lastTick = 0;

        //    while (true)
        //    {
        //        #region 프레임관리 및 영역 설정
        //        int currentTick = System.Environment.TickCount;
        //        int elapsedTick = currentTick - lastTick;

        //        // 만약 경과한 시간이 1/30초보다 작다면
        //        if (elapsedTick < 1000 / 30)
        //            continue;
        //        #endregion


        //        // FPS 프레임 (60 프레임 ok, 30 프레임 이하로는 no)
        //        //입력 - 사용자의 모든 input
        //        //로직 - 서버에서 원격 연산되고 있음
        //        //렌더링 
        //        Console.SetCursorPosition(0, 0); // 내용이 0,0 포지션에서 출력되도록 설정
        //        Console.WriteLine("Hello World!");
        //    }
        //}

        static void Main(string[] args)
        {
            Board board = new Board(); //같은 namespace에 있어야됨. 패키지 느낌
            Player player = new Player();
            player.Initialize(1, 1, board);
            board.Initialize(25, player);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            int lastTick = 0;

            while (true)
            {
                #region 프레임관리
                // 만약 경과한 시간이 1/30초보다 작다면
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion
                
                // 입력

                //로직
                player.Update(deltaTick);

                //렌더링
                Console.SetCursorPosition(0, 0);
                board.Render();
            }

        }
    }
}