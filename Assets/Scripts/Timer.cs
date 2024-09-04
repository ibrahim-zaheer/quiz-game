using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    

    public bool isAnsweringQuestion = false;

    public float fillFraction;

    public bool loadNextQuestion;

    float timerValue;

   

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }
    public void cancelTimer()
    {
        timerValue = 0;
    }
    
    void UpdateTime()
    {
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
               fillFraction = timerValue /  timeToCompleteQuestion;
            }

            else
            {

                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;

            }

        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }

            else { 
            isAnsweringQuestion  = true;
                timerValue = timeToCompleteQuestion;

                loadNextQuestion = true;   
            
            }
        }


        
        Debug.Log( isAnsweringQuestion+" : "+ timerValue +" = "+fillFraction);
    }
    public void SkipButton()
    {
        fillFraction = 0
;    }

}
