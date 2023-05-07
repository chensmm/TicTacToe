using System.Collections;
using UnityEngine;

namespace Assets.Script
{
    public class Chessboard
    {
        public int boardSize { get; private set; }
        public int[,] chessboard { get; private set; }

        public int turn = 1;//player1=1;player2=2;空白=0


        public Chessboard(int size)
        {
            boardSize = size;
            ChessboardInit(boardSize);
        }

        public void ChessboardInit(int boardSize)
        {
            chessboard = new int[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    chessboard[i, j] = -1;
                }
            }
        }

        public void SetChess(int x, int y, int player)
        {
            chessboard[x, y] = player;

        }
        private int IsWin(int x, int y, int player)
        {
            bool isWin = true;
            //检测横排
            for (int i = 0; i < boardSize; i++)
            {
                if (chessboard[x, i] != player)
                {
                    isWin = false;
                }
            }
            //检测竖排
            for (int i = 0; i < boardSize; i++)
            {
                if (chessboard[i, y] != player)
                {
                    isWin = false;
                }
            }

            return 0;
        }

    }
}