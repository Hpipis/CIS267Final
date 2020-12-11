using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public Text txt_TtlScore;
    public Text txt_BonusScore;
    public Text txt_EnemiesKilled;



    //set it for the amount of points you get for completing one level
    public int LevelCompletePoints;

    //total enemies per level to be added onto total enemies
    public int totalEnemiesLevel1;
    public int totalEnemiesLevel2;
    public int totalEnemiesLevel3;

    //points you get for killing the boss
    public int bossPoints;

    //temp = 0 tba kill counter reference
    private int getEnemieskilled = 0;

    

    //totals
    private int totalScore;
    private int BonusScore;
    private int totalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        CalculateScore();

        setText();
    }

    void setText()
    {
        txt_EnemiesKilled.text += getEnemieskilled.ToString();
        txt_BonusScore.text += BonusScore.ToString();
        txt_TtlScore.text += totalScore.ToString();
    }
    void CalculateScore()
    {
        LevelCompletePoints *= 3;
        totalEnemies = totalEnemiesLevel1 + totalEnemiesLevel2 + totalEnemiesLevel3;


        BonusScore = (LevelCompletePoints) - ((LevelCompletePoints * getEnemieskilled - totalEnemies) / totalEnemies);


        totalScore += LevelCompletePoints;
        totalScore += bossPoints;
    }
    

}
