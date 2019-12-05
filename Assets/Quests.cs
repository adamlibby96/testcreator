using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public List<myQuestion> questions;
    private List<myAnswer> answers;

    void Awake()
    {
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        answers = new List<myAnswer>();
        foreach (myQuestion q in questions)
        {
            q.GenerateUUID(); // generate id
            q.answer.AssignUUID(q.GetUUID()); // assign id to answer
            answers.Add(q.answer); // save answer to answer list
        }
        Save();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Save()
    {
        Debug.Log("Saving...");
        FileStream fs = new FileStream("questions.dat", FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, questions);
        fs.Close();
        Debug.Log("Saved!");
    }

    public void Load()
    {
        if (File.Exists("questions.dat"))
        {

            using (Stream stream = File.Open("questions.dat", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                questions = (List<myQuestion>)bf.Deserialize(stream);
            }
        }
        else
        {
            Debug.Log("no saved data yet");
        }
    }
}

public enum tags
{
    Movie,
    Person,
    Director,
    Actress,
    Actor,
    Male,
    Female
}

[System.Serializable]
public class myQuestion
{
    private string uuid = "xxxxxxxxxxxxxxxxxx"; // 16 digit id
    public string question;

    public string GetUUID() { return uuid; }

    public Sprite image;

    public tags[] tags;
    public myAnswer answer;

    public void GenerateUUID()
    {
        string temp = "";
        for (int i = 0; i < uuid.Length; i++)
        {
            temp += Random.Range(0, 9).ToString();
        }
        uuid = temp; // assign the generated id
    }
}

[System.Serializable]
public class myAnswer
{
    private string uuid;
    public string answer;
    public tags[] tags;

    public void AssignUUID(string id)
    {
        uuid = id;
    }
}
