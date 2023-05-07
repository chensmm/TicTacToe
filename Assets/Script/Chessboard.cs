using Assets.Script.AI;
using System.Collections;
using System.Drawing;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Chessboard : MonoBehaviour
    {
        public int[,] chessboard;
        public GameObject gridPrefab;

        int boardSize;
        int turnNum = 0;//目前回合数
        int nowTurn = 1;//当前轮到谁动1=先手，-1=后手
        int playerChooseTurn;//玩家选择先后手，1=先手，-1=后手
        bool fightAI = false;//是否开启AI
        int aiLevel = 1;//AI等级

        GameObject[,] buttons;

        //private void Start()
        //{
        //    Init(3, 1, true, 2);
        //}

        public void Init(int size, int playerChoose, bool fightAI, int AILevel = 1)
        {
            this.boardSize = size;
            this.playerChooseTurn = playerChoose;
            this.fightAI = fightAI;
            this.aiLevel = AILevel;
            this.chessboard = new int[boardSize, boardSize];

            GridLayoutGroup gridComponent = this.gameObject.GetComponent<GridLayoutGroup>();
            gridComponent.cellSize = new Vector2((450 / boardSize) - 10, (450 / boardSize) - 10);
            gridComponent.constraintCount = boardSize;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    chessboard[i, j] = 0;
                }
            }
            CreatChessboard();
            //如果AI先手则直接动一步，不然则等玩家动
            if (fightAI && playerChooseTurn == -1)
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
            chessboard[x, y] = nowTurn;
            turnNum++;
            nowTurn *= -1;
            int isWin = FunctionLibray.IsWin(chessboard, x, y);
            if (isWin == 0)
            {
                //检测是否平局
                if (turnNum == boardSize * boardSize)
                {
                    GameSet(isWin);
                }
                else//让AI动
                {
                    if (fightAI && isPlayer)
                    {

                        AIMove();
                    }

                }
            }
            else
            {
                GameSet(isWin);
            }
            return chessboard[x, y];
        }

        private void GameSet(int isWin)
        {
            foreach (var button in buttons)
            {
                button.GetComponent<Button>().enabled = false;
            }
            StartCoroutine(ShowEndUI(isWin));
        }
        IEnumerator ShowEndUI(int isWin)
        {
            yield return new WaitForSeconds(1.5f);
            GameManager.instance.endGameUI.SetActive(true);
            if (isWin == 0)
            {
                GameManager.instance.endGameUIText.text = "平局！";
                Debug.Log("Draw!");//平局结算
            }
            else
            {
                if (isWin == 1)
                    GameManager.instance.endGameUIText.text = "OO获胜！";
                else
                    GameManager.instance.endGameUIText.text = "XX获胜！";
                Debug.Log("Player" + isWin + " Win!!!");//获胜结算
            }
        }

        public void AIMove()
        {
            if (aiLevel == 1)
            {
                Vector2 aiMove = BaseAI.RandomAIMove(chessboard);
                int turn = SetChess((int)aiMove.x, (int)aiMove.y, false);
                buttons[(int)aiMove.x, (int)aiMove.y].GetComponent<Grid>().OnAIClick(turn);
            }
            else if (aiLevel == 2)
            {
                Vector2 aiMove = BaseAI.Minimax(chessboard, nowTurn);
                int turn = SetChess((int)aiMove.x, (int)aiMove.y, false);
                buttons[(int)aiMove.x, (int)aiMove.y].GetComponent<Grid>().OnAIClick(turn);
            }
        }

    }
}