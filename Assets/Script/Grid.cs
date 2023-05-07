using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script
{
    public class Grid : MonoBehaviour
    {
        TMP_Text text;
        Chessboard chessboard;
        int x;
        int y;
        private int belongTo = 0;//1=player1,-1=player2
        public void OnInit(int x, int y)
        {
            this.x = x;
            this.y = y;
            text = this.gameObject.GetComponentInChildren<TMP_Text>();
            text.text = " ";
            chessboard = GetComponentInParent<Chessboard>();
        }

        public void OnClick()
        {
            int turn = chessboard.SetChess(x, y);
            if (turn == 1)
            {
                text.text = "O";
            }
            else
            {
                text.text = "X";
            }
            Debug.Log("Press:" + x + "," + y);
        }

    }
}