using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code taken and adapted from Youtube "Unity3D How To: Driving With Wheel Colliders" - Renaissance Coders

public class CarController : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_sprintInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    private float crashTime;
    private List<float> crashTimes;
    private float accelerateTime;
    private List<float> accelerateTimes;
    private float brakeTime;
    private List<float> brakeTimes;
    public ListToText listToText;
    private bool textFileWritten;

    private void Start()
    {
        crashTimes = new List<float>();
        accelerateTimes = new List<float>();
        brakeTimes = new List<float>();
        textFileWritten = false;
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_sprintInput = Input.GetAxis("Sprint");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
        accelerateTime = Time.time * 1000;
        accelerateTimes.Add(accelerateTime);
    }

    private void Brake()
    {
        frontDriverW.brakeTorque = m_sprintInput * motorForce;
        frontPassengerW.brakeTorque = m_sprintInput * motorForce;
        brakeTime = Time.time * 1000;
        brakeTimes.Add(brakeTime);
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        if (Time.time >= Settings.duration && textFileWritten == false)
        {
            listToText.WriteListToFile(crashTimes, "CrashTimes");
            listToText.WriteListToFile(accelerateTimes, "AccelerateTimes");
            listToText.WriteListToFile(brakeTimes, "BrakeTimes");

            textFileWritten = true;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name != "Track")
        {
            crashTime = Time.time*1000; // time in milliseconds
            crashTimes.Add(crashTime);

        }
    }
}
