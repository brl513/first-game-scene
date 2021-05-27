using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class ListToText : MonoBehaviour
{
    private string objectName;
    private string fileName;

    // Start is called before the first frame update
    void Start()
    {
        objectName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteListToFile(List<float> times, string listName)
    {
        string textString = objectName + " " + listName + ": "; 
        
        foreach(float time in times)
        {
            textString = textString + time.ToString() + "ms, ";

        }
        string pathString = System.IO.Path.Combine(Application.dataPath, "TextFiles");
        Directory.CreateDirectory(pathString);
        fileName = objectName + listName + ".txt";
        Debug.Log(fileName);
        pathString = System.IO.Path.Combine(pathString, fileName);
        if (!System.IO.File.Exists(pathString))
        {
            StreamWriter sr = File.CreateText(pathString);
            sr.WriteLine(textString);
            sr.Close();
        }
        else
        {
            Console.WriteLine("File \"{0}\" already exists.", fileName);
            return;
        }
    }
}
