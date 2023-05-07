using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.AI
{
    public class BaseAI
    {
        protected List<Vector2> canWalkGrid = new List<Vector2>();

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



    }
}