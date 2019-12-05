using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Term
{
    public string Name;
    public int termID = 0;
}

[System.Serializable]
public class Definition
{
    public string def;
    public int termID = 0;
}

public class TermQuestionare : MonoBehaviour
{
    public List<Term> terms;
    public List<Definition> definitions;

    public Color CorrectColor = Color.green;
    public Color IncorrectColor = Color.red;

    public Text questionText;
    public Text option1Text;
    public Text option2Text;
    public Text option3Text;
    public Text option4Text;

    public Text correctNumberTF;
    public Text wrongNumberTF;

    private int correctID;
    private int curQuestion = 0;
    private int numCorrect = 0;
    private int numWrong = 0;


    // Start is called before the first frame update
    void Start()
    {

        curQuestion = 0;
        GenerateQuestion();

    }

    private void GenerateQuestion()
    {
        // get the definition and correct answer
        string def = definitions[curQuestion].def;
        string answer = terms.Find(x => x.termID == definitions[curQuestion].termID).Name;

        // add the options
        List<string> options = new List<string>();
        while (options.Count < 3)
        {
            int rnd = Random.Range(0, terms.Count - 1);
            // if it is not the correct answer and it is not already in the list, add it
            if (terms[rnd].Name != answer && !options.Contains(terms[rnd].Name))
            {
                options.Add(terms[rnd].Name);
            }

        }

        questionText.text = def;

        correctID = Random.Range(1, 4);
        switch (correctID)
        {
            case 1:
                option1Text.text = answer;
                option2Text.text = options[0];
                option3Text.text = options[1];
                option4Text.text = options[2];
                break;
            case 2:
                option1Text.text = options[0];
                option2Text.text = answer;
                option3Text.text = options[1];
                option4Text.text = options[2];
                break;
            case 3:
                option1Text.text = options[1];
                option2Text.text = options[0];
                option3Text.text = answer;
                option4Text.text = options[2];
                break;
            case 4:
                option2Text.text = options[0];
                option3Text.text = options[1];
                option1Text.text = options[2];
                option4Text.text = answer;
                break;
            default:
                option3Text.text = answer;
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
        if (curQuestion >= definitions.Count)
        {
            curQuestion = 0;
        }
        StartCoroutine(GoToNextQuestion());



    }

    private IEnumerator GoToNextQuestion()
    {
        yield return new WaitForSeconds(2f);
        GenerateQuestion();

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

    public void GoToMovieQuestions()
    {
        SceneManager.LoadScene("MovieQuestions");
    }
}
