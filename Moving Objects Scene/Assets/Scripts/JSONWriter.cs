using System;
using System.IO;
using UnityEngine;

public class JSONWriter : MonoBehaviour
{
    MotionSetup motionSetup;

    public string FILE_NAME;


    void Start()
    {

        motionSetup = gameObject.GetComponent<MotionSetup>();
        InstantPosition[] instantPosition = new InstantPosition[(int)(Settings.duration * Settings.FPS)];

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS); i++)
        {

            instantPosition[i] = new InstantPosition();
            instantPosition[i].time = motionSetup.instantTime[i];
            instantPosition[i].x = motionSetup.instantX[i];
            instantPosition[i].y = motionSetup.instantY[i];
            instantPosition[i].z = motionSetup.instantZ[i];

        }

        //Convert to JSON
        string instantPositionToJson = JsonHelper.ToJson(instantPosition, true);

        string pathString = @"/Users/brl513/Unity Projects/first-game-scene/Moving Objects Scene/Assets/JSONFiles";

        // You can extend the depth of your path if you want to.
        //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

        // Create the subfolder. You can verify in File Explorer that you have this
        // structure in the C: drive.
        //    Local Disk (C:)
        //        Top-Level Folder
        //            SubFolder
        System.IO.Directory.CreateDirectory(pathString);

        // Create a file name for the file you want to create.
        FILE_NAME += ".json";

        // This example uses a random string for the name, but you also can specify
        // a particular name.
        //string fileName = "MyNewFile.txt";

        // Use Combine again to add the file name to the path.
        pathString = System.IO.Path.Combine(pathString, FILE_NAME);

        // Verify the path that you have constructed.
        Console.WriteLine("Path to my file: {0}\n", pathString);

        // Check that the file doesn't already exist. If it doesn't exist, create
        // the file and write the JSON.
        // DANGER: System.IO.File.Create will overwrite the file if it already exists.
        // This could happen even with random file names, although it is unlikely.
        if (!System.IO.File.Exists(pathString))
        {
            StreamWriter sr = File.CreateText(pathString);
            sr.WriteLine(instantPositionToJson);
            sr.Close();
        }
        else
        {
            Console.WriteLine("File \"{0}\" already exists.", FILE_NAME);
            return;
        }
    }

}