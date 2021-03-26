using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WriteFileNameJSON : MonoBehaviour
{
    private GameObject[] instruments;

    private Scene scene;
    private string sceneName;
    private int instrumentsLength;
    private int i;

    void Start()
    {

        instruments = GameObject.FindGameObjectsWithTag("Instrument");
        instrumentsLength = instruments.Length;

        if (instrumentsLength < 1)
        {
            Debug.Log("No instruments found");
        }
        else
        {
            Debug.Log(instrumentsLength + " instruments found");
        }

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        Debug.Log(sceneName);


        Track[] tracks = new Track[instruments.Length];

        i = 0;

        foreach (GameObject instrument in instruments)
        {
            Debug.Log(instrument.name);
            tracks[i] = new Track();
            tracks[i].wavFileName = sceneName + instrument.name + ".wav";
            tracks[i].JSONFileName = instrument.name + ".json";
            i += 1;
        }

        Debug.Log("Made " + tracks.Length + " tracks");


        //Convert to JSON
        string tracktoJSON = JsonHelperB.ToJson(tracks, true);

        string pathString = System.IO.Path.Combine(Application.dataPath, "JSONFiles");

        // You can extend the depth of your path if you want to.
        //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

        // Create the subfolder. You can verify in File Explorer that you have this
        // structure in the C: drive.
        //    Local Disk (C:)
        //        Top-Level Folder
        //            SubFolder
        System.IO.Directory.CreateDirectory(pathString);

        // Create a file name for the file you want to create.
        sceneName += ".json";

        // This example uses a random string for the name, but you also can specify
        // a particular name.
        //string fileName = "MyNewFile.txt";

        // Use Combine again to add the file name to the path.
        pathString = System.IO.Path.Combine(pathString, sceneName);

        // Verify the path that you have constructed.
        Console.WriteLine("Path to my file: {0}\n", pathString);

        // Check that the file doesn't already exist. If it doesn't exist, create
        // the file and write the JSON.
        // DANGER: System.IO.File.Create will overwrite the file if it already exists.
        // This could happen even with random file names, although it is unlikely.
        if (!System.IO.File.Exists(pathString))
        {
            StreamWriter sr = File.CreateText(pathString);
            sr.WriteLine(tracktoJSON);
            sr.Close();
        }
        else
        {
            Console.WriteLine("File \"{0}\" already exists.", sceneName);
            return;
        }
    }

}