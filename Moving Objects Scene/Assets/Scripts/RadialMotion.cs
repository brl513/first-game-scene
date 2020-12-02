using UnityEngine;

public class RadialMotion : MonoBehaviour
{
    public Vector3 offset;

    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;
    public float depth;
    public bool rotationIsClockwise;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime*speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;
        float z = Mathf.Sin(timeCounter) * depth;

        if (rotationIsClockwise == true)
        {
            transform.position = new Vector3(-x, y, z) + offset;
        }
        else
        {
            transform.position = new Vector3(x, y, z) + offset;
        }

    }
}
