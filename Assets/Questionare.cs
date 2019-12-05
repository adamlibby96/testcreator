using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum QType { Person, Movie }


public class Questionare : MonoBehaviour
{
    internal class Question
    {
        public string ques = "";
        public Photo photo;
        public bool isPerson = false;
        public bool isActor = false;
    }

    List<Question> questions;
    
    public Color CorrectColor = Color.green;
    public Color IncorrectColor = Color.red;

    public Image image;
    public Text questionText;
    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    public Text option4Text;

    public Text correctNumberTF;
    public Text wrongNumberTF;
    private int numCorrect = 0;
    private int numWrong = 0;

    private int correctID;
    private int curQuestion = 0;


    // Start is called before the first frame update
    void Start()
    {
        questions = new List<Question>();
        foreach (Photo p in Database.instance.photos)
        {
            if (p.isMovie)
            {
                Question who = new Question();
                Question movie = new Question();

                who.photo = p;
                who.ques = "Director?";
                who.isPerson = true;

                movie.photo = p;
                movie.ques = "Movie?";

                questions.Add(who);
                questions.Add(movie);
                if (p.hasActor)
                {
                    Question actor = new Question();
                    actor.photo = p;
                    actor.ques = "Actor/Actress?";
                    actor.isPerson = true;
                    actor.isActor = true;
                    questions.Add(actor);
                }
            }
            else
            {
                Question who = new Question();
                who.photo = p;
                who.ques = "Who?";
                who.isPerson = true;
                
                questions.Add(who);
            }
        }

        

        GenerateQuestion(0); // generate the first question

        

    }

    private void GenerateQuestion(int id)
    {
        
        image.sprite = questions[id].photo.photo; // set the photo 
        questionText.text = questions[id].ques; // set the question

        string correctAnswer;
        List<string> options = new List<string>();

        // get 3 random people or movie names
        if (questions[id].isPerson)
        {
                // get the correct answer
            if (questions[id].isActor)
            {
                correctAnswer = Database.instance.people.Find(x => x.PhotoID == questions[id].photo.PhotoID && x.isActor).Name;
            }
            else
            {
                correctAnswer = Database.instance.people.Find(x => x.PhotoID == questions[id].photo.PhotoID && !x.isActor).Name;
            }

            while (options.Count < 3)
            {
                int rnd = Random.Range(0, Database.instance.people.Count - 1);
                // only add to the options array if it is not the correct answer AND it is not already in the list of options, and it's not the correct answer name (there are duplicates in the people array)
                if (Database.instance.people[rnd].PhotoID != questions[id].photo.PhotoID && !options.Contains(Database.instance.people[rnd].Name) && Database.instance.people[rnd].Name != correctAnswer)
                {
                    options.Add(Database.instance.people[rnd].Name);
                }
            }

        } 
        else
        {
            // get the correct answer
            correctAnswer = Database.instance.movies.Find(x => x.PhotoID == questions[id].photo.PhotoID).Name;

            while (options.Count < 3)
            {
                int rnd = Random.Range(0, Database.instance.movies.Count - 1);
                // only add to the options array if it is not the correct answer AND it is not already in the list of options
                if (Database.instance.movies[rnd].PhotoID != questions[id].photo.PhotoID && !options.Contains(Database.instance.movies[rnd].Name))
                {
                    options.Add(Database.instance.movies[rnd].Name);
                }
            }
        }


        correctID = (int)Random.Range(1,4); // get the correct button ID
        
        switch (correctID)
        {
            case 1:
                option1Text.text = correctAnswer;
                option2Text.text = options[0];
                option3Text.text = options[1];
                option4Text.text = options[2];
                break;
            case 2:
                option1Text.text = options[0];
                option2Text.text = correctAnswer;
                option3Text.text = options[1];
                option4Text.text = options[2];
                break;
            case 3:
                option1Text.text = options[1];
                option2Text.text = options[0];
                option3Text.text = correctAnswer;
                option4Text.text = options[2];
                break;
            case 4:
                option2Text.text = options[0];
                option3Text.text = options[1];
                option1Text.text = options[2];
                option4Text.text = correctAnswer;
                break;
            default:
                option3Text.text = correctAnswer;
                option2Text.text = options[0];
                option1Text.text = options[1];
                option4Text.text = options[2];
                break;
        }

    }

    
    // button id is passed to validate the current answerS
    public void Validate(int buttonID)
    {
        if (buttonID == correctID)
        {
            numCorrect++;
            Debug.Log("CORRECT!");
            if (buttonID == 1)
            {
                option1Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
            else if (buttonID == 2)
            {
                option2Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
            else if (buttonID == 3)
            {
                option3Text.transform.GetComponentInParent<Image>().color = CorrectColor;

            }
            else if (buttonID == 4)
            {
                option4Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
        }
        else
        {
            numWrong++;
            Debug.Log("INCORRECT....");
            if (buttonID == 1)
            {
                option1Text.transform.GetComponentInParent<Image>().color = IncorrectColor;
            }
            else if (buttonID == 2)
            {
                option2Text.transform.GetComponentInParent<Image>().color = IncorrectColor;
            }
            else if (buttonID == 3)
            {
                option3Text.transform.GetComponentInParent<Image>().color = IncorrectColor;

            }
            else if (buttonID == 4)
            {
                option4Text.transform.GetComponentInParent<Image>().color = IncorrectColor;
            }

            // color the correct one green
            if (correctID == 1)
            {
                option1Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
            else if (correctID == 2)
            {
                option2Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
            else if (correctID == 3)
            {
                option3Text.transform.GetComponentInParent<Image>().color = CorrectColor;

            }
            else if (correctID == 4)
            {
                option4Text.transform.GetComponentInParent<Image>().color = CorrectColor;
            }
        }

        // update ui numbers
        correctNumberTF.text = numCorrect.ToString();
        wrongNumberTF.text = numWrong.ToString();

        // disable the buttons

        option1Text.transform.GetComponentInParent<Button>().interactable = false;
        option2Text.transform.GetComponentInParent<Button>().interactable = false;
        option3Text.transform.GetComponentInParent<Button>().interactable = false;
        option4Text.transform.GetComponentInParent<Button>().interactable = false;


        curQuestion++;

        // if we were on the last question, restart
        if (curQuestion >= questions.Count)
        {
            curQuestion = 0;
        }
        StartCoroutine(GoToNextQuestion());
        


    }

    private IEnumerator GoToNextQuestion()
    {
        yield return new WaitForSeconds(1.5f);
        GenerateQuestion(curQuestion);

        // enable the buttons
        option1Text.transform.GetComponentInParent<Button>().interactable = true;
        option2Text.transform.GetComponentInParent<Button>().interactable = true;
        option3Text.transform.GetComponentInParent<Button>().interactable = true;
        option4Text.transform.GetComponentInParent<Button>().interactable = true;

        // reset the colors
        option1Text.transform.GetComponentInParent<Image>().color = Color.white;
        option2Text.transform.GetComponentInParent<Image>().color = Color.white;
        option3Text.transform.GetComponentInParent<Image>().color = Color.white;
        option4Text.transform.GetComponentInParent<Image>().color = Color.white;

    }

    public void GoToTermScene()
    {
        SceneManager.LoadScene("TermQuestions");
    }
}
