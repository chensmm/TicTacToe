using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Script
{
    public class Chessboard : MonoBehaviour
    {
        public int boardSize { get; private set; }
        public int[,] chessboard { get; private set; }

        private int turnNum = 0;
        private bool player1Trun = true;
        public GameObject gridPrefab;

        private void Start()
        {
            Init(3);
        }

        public void Init(int size)
        {
            boardSize = size;
            chessboard = new int[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    chessboard[i, j] = 0;
                }
            }
            CreatChessboard();
        }

        public void CreatChessboard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    GameObject grid = GameObject.Instantiate(gridPrefab, this.gameObject.transform);
                    grid.GetComponent<Grid>().OnInit(i, j);
                }

            }
        }

        public int SetChess(int x, int y)
        {
            if (player1Trun)
            {
                chessboard[x, y] = 1;
            }
            else
            {
                chessboard[x, y] = -1;
            }
            turnNum++;
            player1Trun = !player1Trun;
            int isWin = IsWin(x, y);
            if (isWin == 0)
            {
                //检测是否平局
                if (turnNum == boardSize * boardSize)
                {
                    Debug.Log("Draw!");//平局结算
                }
            }
            else
            {
                Debug.Log("Player" + isWin + " Win!!!");//获胜结算
            }
            return chessboard[x, y];
        }
        private int IsWin(int x, int y)
        {
            bool isWin = true;
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