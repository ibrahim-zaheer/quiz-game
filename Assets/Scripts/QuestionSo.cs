using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu( menuName = "Quiz Question", fileName ="New Question")]
public class QuestionSo : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";

    [SerializeField] string[] answers = new string[4];

    [SerializeField] int correctAnswer;

    public string GetQuestion() { 
    return question;
    }

    public int getCorrectAnswerIndex()
    {
        return correctAnswer;
    }
    public string GetANswer(int index)
    {
        return answers[index];
    }
    public string GetAnswer1() { 
    return answers[0];
    }

}
