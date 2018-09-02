using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryCheck : MonoBehaviour {

    public PlayerInfo p1;
    public PlayerInfo p2;

    public Text p1Txt;
    public Text p2Txt;

    private bool gameOver = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            p2.HP = 0;
        }

        if (gameOver)
            return;
        if(p1.HP <=0 && p2.HP <= 0)
        {
            p1Txt.text = p2Txt.text = "noone wins.";
            gameOver = true;
        }
        else if(p1.HP <= 0 )
        {
            p1Txt.text =  p2Txt.text = "Player 2 wins";
            gameOver = true;
        }
        else if(p2.HP <= 0)
        {
            p1Txt.text = p2Txt.text = "Player 1 wins";
            gameOver = true;
        }
        if(gameOver)
             StartCoroutine("resetGame");
    }
    IEnumerator resetGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
