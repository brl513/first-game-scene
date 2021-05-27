using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 mouseRotation = new Vector2(0, 0);
    public float lookSensitivity = 5f;
    private bool isButtonDown = false;
    private bool wasButtonDown = false;
    private bool isEnemyInSight = false;
    public GameObject canvas;
    public GameObject gun;
    private Color gunColour;
    public float gunFlashDuration = 0.1f;
    public Color gunFlashColour;
    bool flash;
    private MeshRenderer gunMesh;
    private float timeLeft;
    private float shootTime;
    private List<float> shootTimes;
    public ListToText listToText;
    private bool textFileWritten;

    // Start is called before the first frame update
    void Start()
    {
        gunMesh = gun.GetComponent<MeshRenderer>();
        gunColour = gunMesh.material.color;

        var canvasController = canvas.GetComponent<CanvasController>();

        canvasController.EnemyOutOfSight();

        shootTimes = new List<float>();
    }

    // Update is called once per frame.
    void Update()
    {
        isButtonDown = Input.GetAxis("Fire") > 0;
        mouseRotation.x += Input.GetAxis("Mouse X"); // Found these axis names in input manager.
        mouseRotation.y += Input.GetAxis("Mouse Y");

        if (timeLeft > 0)
        {
            var tempColor = gunFlashColour;
            gunMesh.material.color = tempColor;
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0) // Flash off
        {
            gunMesh.material.color = gunColour;
        }

    }

    // Update is called once per physics update;
    void FixedUpdate()
    {
        float yAngle = Mathf.Clamp(mouseRotation.y * lookSensitivity, -90, 90); // Ensures player cannot "backflip".
        transform.eulerAngles = new Vector2(yAngle, mouseRotation.x * lookSensitivity); // Assigns player rotation to mouse movement.

        var canvasController = canvas.GetComponent<CanvasController>();
        canvasController.EnemyOutOfSight();

        bool shouldFire = isButtonDown && !wasButtonDown; // fun boolean logic occurs here.
        wasButtonDown = isButtonDown;

        if (shouldFire)
        {
            GunFlash();
        }

            RaycastHit seek;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out seek))
        {
            var enemy = seek.collider.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                canvasController.EnemyWasSeeked();
            }
            if (enemy != null && shouldFire)
            {
                GunFlash();
                canvasController.HitMarkerOn();
                enemy.EnemyWasHit();

            }

        }
        if (Time.time >= Settings.duration && textFileWritten == false)
        {
            listToText.WriteListToFile(shootTimes, "ShootTimes");

            textFileWritten = true;
        }
    }

    public void GunFlash()
    {
        timeLeft = gunFlashDuration;
        shootTime = Time.time;
        shootTimes.Add(shootTime);
        Debug.Log("Shoot counter: " + shootTimes.Count);
        Debug.Log("Shoot time: " + shootTime.ToString("F3"));

    }

}
