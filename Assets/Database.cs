using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person
{
    public string Name = "";
    public int PhotoID = 0;
    public bool isActor = false;
}

[System.Serializable]
public class Movie
{
    public string Name = "";
    public int PhotoID = 0;
}

[System.Serializable]
public class Photo
{
    public Sprite photo;
    public int PhotoID = 0;
    public bool isMovie = true;
    public bool hasActor = false;
}



public class Database : MonoBehaviour
{
    public List<Person> people;
    public List<Movie> movies;
    public List<Photo> photos;

    public static Database instance;
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
