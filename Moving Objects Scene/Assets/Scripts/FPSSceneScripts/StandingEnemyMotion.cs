using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingEnemyMotion : MonoBehaviour
{
    Quaternion startRotation;
    public float waddleSpeedHz = 0.5f;
    public float maxWaddleAngle = 20f;
    private float currentWaddlePhase = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        Debug.Log("start rot = " + startRotation);

    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.AngleAxis(Mathf.Sin(currentWaddlePhase)*maxWaddleAngle, Vector3.forward);
        currentWaddlePhase += 2 * Mathf.PI * Time.deltaTime * waddleSpeedHz;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
