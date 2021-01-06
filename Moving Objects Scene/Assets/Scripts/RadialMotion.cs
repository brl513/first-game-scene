using UnityEngine;

public class RadialMotion : MonoBehaviour
{
    public Vector3 offset;

    public JSONReader jsonReader;

    float timeCounter = 0;

    public float speed;
    public float width;
    public float height;
    public float depth;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;

        transform.position = jsonReader.getPositionForTime(timeCounter) + offset;

    }
}
