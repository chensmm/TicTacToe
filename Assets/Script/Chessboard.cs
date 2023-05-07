using Assets.Script.AI;
using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Chessboard : MonoBehaviour
    {
        public int boardSize;
        public int[,] chessboard;
        public GameObject gridPrefab;

        int turnNum = 0;//目前回合数
        bool player1Trun = true;//目前是谁的回合，true=先手玩家回合，false=后手玩家回合
        bool playerChooseTurn;//玩家选择先后手，true=先手，false=后手
        bool fightAI = false;//是否开启AI
        int aiLevel = 1;//AI等级

        GameObject[,] buttons;

        private void Start()
        {
            Init(3, true, true, 1);

        }

        public void Init(int size, bool playerChoose, bool fightAI, int AILevel = 1)
        {
            this.boardSize = size;
            this.playerChooseTurn = playerChoose;
            this.fightAI = fightAI;
            this.aiLevel = AILevel;
            this.chessboard = new int[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    chessboard[i, j] = 0;
                }
            }
            CreatChessboard();
            //如果AI先手则直接动一步，不然则等玩家动
            if (fightAI && playerChooseTurn == false)
            {
                AIMove();
            }
        }

        public void CreatChessboard()
        {
            buttons = new GameObject[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    GameObject grid = GameObject.Instantiate(gridPrefab, this.gameObject.transform);
                    grid.GetComponent<Grid>().OnInit(i, j);
                    buttons[i, j] = grid;
                }
            }
        }

        public int SetChess(int x, int y, bool isPlayer)
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
                else
                {
                    if (fightAI && isPlayer)
                        AIMove();
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

        public void AIMove()
        {
            if (aiLevel == 1)
            {
                Vector2 aiMove = BaseAI.RandomAIMove(chessboard);
                int turn = SetChess((int)aiMove.x, (int)aiMove.y, false);
                buttons[(int)aiMove.x, (int)aiMove.y].GetComponent<Grid>().OnAIClick(turn);
            }

        }

    }
}