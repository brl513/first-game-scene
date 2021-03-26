using System;
using System.IO;
using UnityEngine;

public class JSONWriter : MonoBehaviour
{
    MotionSetup motionSetup;

    private string objectName;

    Vector3 rotateAxis(Vector3 vec)
    {
        Quaternion rotation1 = Quaternion.Euler(0, 90, -90);
        Quaternion rotation2 = Quaternion.Euler(0, 0, 0);



        vec = rotation1 * rotation2 * vec;
        var inverser = new Vector3(1, 1, -1);
        return Vector3.Scale(inverser, vec);
    }


    void Start()
    {

        motionSetup = gameObject.GetComponent<MotionSetup>();
        InstantPosition[] instantPosition = new InstantPosition[(int)(Settings.duration * Settings.FPS)];

        for (int i = 0; i < (int)(Settings.duration * Settings.FPS); i++)
        {
            var vec = new Vector3(motionSetup.instantX[i], motionSetup.instantY[i], motionSetup.instantZ[i]);
            vec = rotateAxis(vec);

            instantPosition[i] = new InstantPosition();
            instantPosition[i].time = motionSetup.instantTime[i];
            instantPosition[i].x = vec.x;
            instantPosition[i].y = vec.y;
            instantPosition[i].z = vec.z;

            objectName = gameObject.name;

        }

        Debug.Log("JSONWriter works up to here");

        //Convert to JSON
        string instantPositionToJson = JsonHelper.ToJson(instantPosition, true);

        string pathString = System.IO.Path.Combine(Application.dataPath, "JSONFiles");

        Debug.Log(pathString);

        // You can extend the depth of your path if you want to.
        //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

        // Create the subfolder. You can verify in File Explorer that you have this
        // structure in the C: drive.
        //    Local Disk (C:)
        //        Top-Level Folder
        //            SubFolder
        System.IO.Directory.CreateDirectory(pathString);

        // Create a file name for the file you want to create.
        objectName += ".json";

        // This example uses a random string for the name, but you also can specify
        // a particular name.
        //string fileName = "MyNewFile.txt";

        // Use Combine again to add the file name to the path.
        pathString = System.IO.Path.Combine(pathString, objectName);

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
            Console.WriteLine("File \"{0}\" already exists.", objectName);
            return;
        }
    }

}