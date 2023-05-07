using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Script.AI
{
    public class BaseAI
    {
        protected List<Vector2> canWalkGrid = new List<Vector2>();

        static int treeDepth = 0;
        static int treeMaxDepth = 100;//minimax最大遍历深度
        public static Vector2 RandomAIMove(int[,] chessboard)
        {
            List<Vector2> canWalkGrid = new List<Vector2>();
            for (int i = 0; i < chessboard.GetLength(0); i++)
            {
                for (int j = 0; j < chessboard.GetLength(1); j++)
                {
                    if (chessboard[i, j] == 0)
                    {
                        canWalkGrid.Add(new Vector2(i, j));
                    }
                }
            }
            int random = Random.Range(0, canWalkGrid.Count);
            return canWalkGrid[random];
        }

        public static Vector2 Minimax(int[,] chessboard, int aiPlayer)
        {
            int boardSize = chessboard.GetLength(0);
            int bestScore = int.MinValue;
            Vector2 bestMove = new Vector2(-1, -1);

            //遍历所有空格
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (chessboard[i, j] == 0)
                    {
                        chessboard[i, j] = aiPlayer;
                        treeDepth = 0;
                        int score = Minimax(chessboard, false, aiPlayer);
                        chessboard[i, j] = 0;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = new Vector2(i, j);
                        }
                    }
                }
            }
            return bestMove;
        }

        public static int Minimax(int[,] chessboard, bool isMaximizing, int aiPlayer)
        {
            treeDepth += 1;
            int boardSize = chessboard.GetLength(0);
            int humanPlayer = aiPlayer == 1 ? -1 : 1;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    int result = FunctionLibray.IsWin(chessboard, i, j); //调用已有的函数判断是否有胜负
                    if (result == aiPlayer) return 10; //如果AI赢了，返回10分
                    if (result == humanPlayer) return -10; //如果人类赢了，返回-10分
                }
            }
            bool isfull = true;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (chessboard[i, j] == 0)
                        isfull = false;
                }
            }
            if (isfull) return 0; //如果棋盘满了，返回0分

            if (treeDepth > treeMaxDepth)
            {
                return 0;
            }

            if (isMaximizing) //如果是AI的回合，寻找最大化得分
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (chessboard[i, j] == 0)
                        {
                            chessboard[i, j] = aiPlayer;
                            int score = Minimax(chessboard, false, aiPlayer);
                            chessboard[i, j] = 0;
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            else //如果是人类的回合，寻找最小化得分
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (chessboard[i, j] == 0)
                        {
                            chessboard[i, j] = humanPlayer;
                            int score = Minimax(chessboard, true, aiPlayer);
                            chessboard[i, j] = 0;
                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}