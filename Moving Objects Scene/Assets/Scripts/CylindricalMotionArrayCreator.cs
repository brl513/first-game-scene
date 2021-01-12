using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylindricalMotionArrayCreator : MonoBehaviour
{
    public static float FPS = 30;
    public static float duration = 12;
    public bool movementIsClockwise = true;
    public Vector3 centrePoint;
    public float width;
    public float depth;
    public float height;
    public float radiusMultiplier;
    public float radialSpeed;
    private static float[] instantTime = MathsFunctions.linspace(0, duration - (duration / (duration * FPS)), (int)(duration * FPS));
    private float[] instantX = new float[instantTime.Length];
    private float[] instantY = new float[instantTime.Length];
    private float[] instantZ = new float[instantTime.Length];

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        instantY = MathsFunctions.linspace(-height/2, height/2, instantTime.Length);
        for (int i = 0; i < (int)(duration * FPS) ; i++)
        {
            if (movementIsClockwise == false)
            {
                instantX[i] = Mathf.Cos(instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(width) * radiusMultiplier;
                instantZ[i] = Mathf.Sin(instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(depth) * radiusMultiplier;
            }
            else
            {
                instantX[i] = -(Mathf.Cos(instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(width) * radiusMultiplier);
                instantZ[i] = Mathf.Sin(instantTime[i] * Mathf.Abs(radialSpeed)) * Mathf.Abs(depth) * radiusMultiplier;
            }

           
        }

    }

    public Vector3 getPositionForTime(float time)
    {
        int index = 0;

        for (int i = 0; i < (int)(duration * FPS) ; i++)
        {
            if (time < instantTime[i]) {
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
