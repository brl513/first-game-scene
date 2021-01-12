using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMotionArrayCreator : MonoBehaviour
{
    public static float FPS = 30;
    public static float duration = 12;
    public float speed;
    public Vector3 startCoordinates;
    public Vector3 endCoordinates;
    private static float[] instantTime = MathsFunctions.linspace(0, duration - (duration / (duration * FPS)), (int)(duration * FPS));
    private float[] instantX = new float[instantTime.Length];
    private float[] instantY = new float[instantTime.Length];
    private float[] instantZ = new float[instantTime.Length];

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        instantX = MathsFunctions.linspace(startCoordinates.x, endCoordinates.x, instantTime.Length);
        instantY = MathsFunctions.linspace(startCoordinates.y, endCoordinates.y, instantTime.Length);
        instantZ = MathsFunctions.linspace(startCoordinates.z, endCoordinates.z, instantTime.Length);
    }

    public Vector3 getPositionForTime(float time)
    {
        int index = 0;

        for (int i = 0; i < (int)(duration * FPS); i++)
        {
            if (time < instantTime[i])
            {
                Debug.Log("Index: " + index);
                break;
            }
            index++;
        }

        float x = instantX[index];
        float y = instantY[index];
        float z = instantZ[index];

        return new Vector3(x, y, z);
    }


    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        transform.position = getPositionForTime(timeCounter);

    }
}
