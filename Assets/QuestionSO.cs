using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Question", menuName = "Test Data/Question")]
public class QuestionSo : ScriptableObject
{
    public string Name;
    public bool hasImage;
    public Sprite image;
    
    public AnswerSO answer;

    [Header("Seperate by , (no spaces)")]
    public string tags;

    
}
