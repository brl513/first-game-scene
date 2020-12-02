using UnityEngine;

public class RadialMotion : MonoBehaviour
{
    public Vector3 offset;

    float timeCounter = 0;

    public float speed;
    public float width;
    public float depth;


    // Start is called before the first frame update
    void Start()
    {
        
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
