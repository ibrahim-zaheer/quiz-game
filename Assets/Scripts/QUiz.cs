using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSo currentQuestion;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSo> questions = new List<QuestionSo>();

    [Header("Answers")]
    [SerializeField] TextMeshProUGUI answer1Text;
    [SerializeField] GameObject[] answerButtons;
    bool hasAnsweredEarly = true;

    [Header("ButtonColors")]
    int correctAnswerIndex;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;


    public bool isComplete;
    // Start is called before the first frame update


    //My Solution

    public bool outOfQuestion = false;
    void Start()
    {
        displayQuestion();
        timer = FindObjectOfType<Timer>();
        
        if (timer == null)
        {
            Debug.LogError("Timer not found! Make sure there is a Timer component in the scene.");
            return;
        }
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
        if (timer == null)
        {
            Debug.LogError("Timer is null in Update.");
            return;
        }

        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue) {
        isComplete = true;
        }
            GetNewQuestion();
            timer.loadNextQuestion = false;
            hasAnsweredEarly = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.cancelTimer();

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.getCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.color = Color.red;

            correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetANswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer is \n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNewQuestion()
    {
        if(questions.Count > 0)
        {

            SetButtonState(true);
            SetDefaultButtonSprites();

            GetRandomQuestion();
            displayQuestion();

            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
        
    }

    void GetRandomQuestion()
    {
        if (questions.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, questions.Count);
            currentQuestion = questions[index];
            questions.Remove(currentQuestion);
        }
        else
        {
            Debug.Log("No more questions available.");
            outOfQuestion = true;
        }
    }

    void displayQuestion()
    {
        if (currentQuestion == null)
        {
            Debug.LogError("Current question is null.");
            return;
        }

        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetANswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.color = Color.white;
            buttonImage.sprite = defaultAnswerSprite;
            
        }
    }
}
