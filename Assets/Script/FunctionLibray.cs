using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class FunctionLibray
    {
        public static int IsWin(int[,] chessboard, int x, int y)
        {
            bool isWin = true;
            int boardSize = chessboard.GetLength(0);
            //检测竖排
            int value = 0;
            for (int i = 0; i < boardSize; i++)
            {
                value += chessboard[x, i];
            }
            if (Mathf.Abs(value) == boardSize)//player
            {
                return value / boardSize;
            }
            //检测横排
            value = 0;
            for (int i = 0; i < boardSize; i++)
            {
                value += chessboard[i, y];
            }
            if (Mathf.Abs(value) == boardSize)
            {
                return value / boardSize;
            }
            //检测45斜方向
            value = 0;
            if (x == y)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    value += chessboard[i, i];
                }
            }
            if (Mathf.Abs(value) == boardSize)
            {
                return value / boardSize;
            }
            //检测135斜方向
            value = 0;
            if (x + y == boardSize)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    value += chessboard[boardSize - i - 1, i];
                }
            }
            if (Mathf.Abs(value) == boardSize)
            {
                return value / boardSize;
            }

            return 0;
        }
    }
}