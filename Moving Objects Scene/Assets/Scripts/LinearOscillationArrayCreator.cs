using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearOscillationArrayCreator : MonoBehaviour
{
    public static float FPS = 30;
    public static float duration = 12;
    public bool oscillationInXAxis = true;
    public bool oscillationInYAxis = true;
    public bool oscillationInZAxis = true;
    public Vector3 centrePoint;
    public float xDisplacement;
    public float yDisplacement;
    public float zDisplacement;
    public float speed;
    private static float[] instantTime = MathsFunctions.linspace(0, duration - (duration / (duration * FPS)), (int)(duration * FPS));
    private float[] instantX = new float[instantTime.Length];
    private float[] instantY = new float[instantTime.Length];
    private float[] instantZ = new float[instantTime.Length];

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)(duration * FPS); i++)
        {
            if (oscillationInXAxis == true)
            {
                instantX[i] = Mathf.Sin(instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(xDisplacement);
            }
            if(oscillationInYAxis == true)
            {
                instantY[i] = Mathf.Sin(instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(yDisplacement);
            }
            if(oscillationInZAxis == true)
            {
                instantZ[i] = Mathf.Sin(instantTime[i] * Mathf.Abs(speed)) * Mathf.Abs(zDisplacement);
            }
            if (oscillationInXAxis == false)
            {
                instantX[i] = 0;
            }
            if (oscillationInYAxis == false)
            {
                instantY[i] = 0;
            }
            if (oscillationInZAxis == false)
            {
                instantZ[i] = 0;
            }

        }

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

        transform.position = getPositionForTime(timeCounter) + centrePoint;

    }
}
