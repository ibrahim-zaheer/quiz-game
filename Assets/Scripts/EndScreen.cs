using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI finalScoretext;

    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        
        
    }
    public void showFinalScore()
    {
        finalScoretext.text = "Congratulations \n your score is " + scoreKeeper.CalculateScore();
    }

      
    
}
