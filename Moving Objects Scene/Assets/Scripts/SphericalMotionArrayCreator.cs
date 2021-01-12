using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalMotionArrayCreator : MonoBehaviour
{
    public static float FPS = 30;
    public static float duration = 12;
    public bool movementIsClockwise = true;
    public Vector3 centrePoint;
    public float height;
    public float radiusMultiplier;

    public float startAz;
    public float endAz;
    public float startEl;
    public float endEl;

    public Vector3 orientation;
    private static float[] instantTime = MathsFunctions.linspace(0, duration - (duration / (duration * FPS)), (int)(duration * FPS));
    private float[] instantX = new float[instantTime.Length];
    private float[] instantY = new float[instantTime.Length];
    private float[] instantZ = new float[instantTime.Length];

    private float timeCounter = 0;

    // Start is called before the first frame update
    void Start( )
    {
        

        for (int i = 0; i < (int)(duration * FPS) ; i++)
        {
            float theta = Mathf.PI / 180 * (((endEl - startEl) / instantTime[instantTime.Length - 1] * instantTime[i]) + startEl);
            float phi = Mathf.PI / 180 * (((endAz - startAz) / instantTime[instantTime.Length - 1] * instantTime[i]) + startAz);

            if (movementIsClockwise == false)
            {
                instantX[i] = Mathf.Sin(theta) * Mathf.Cos(phi) * radiusMultiplier;
                instantY[i] = Mathf.Cos(theta) * Mathf.Abs(height);
                instantZ[i] = Mathf.Sin(theta) * Mathf.Sin(phi) * radiusMultiplier;
            }
            else
            {
                instantX[i] = -(Mathf.Sin(theta) * Mathf.Cos(phi) * radiusMultiplier);
                instantY[i] = Mathf.Cos(theta) * Mathf.Abs(height);
                instantZ[i] = Mathf.Sin(theta) * Mathf.Sin(phi) * radiusMultiplier;
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
