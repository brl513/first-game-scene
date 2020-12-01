using UnityEngine;

public class RadialMotion : MonoBehaviour
{
    public Vector3 offset;

    float timeCounter = 0;

    float speed;
    float width;
    float depth;


    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        width = 3;
        depth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime*speed;

        float x = Mathf.Cos(timeCounter)*width;
        float y = 0;
        float z = Mathf.Sin(timeCounter)*depth;

        transform.position = new Vector3(x, y, z) + offset;

    }
}
