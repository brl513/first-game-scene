using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float startingHealth = 10;
    float health;
    public float hitFlashDuration = 0.1f;
    private float timeLeft;
    public Color flashColour;
    private Material[] defaultMats;
    private Color[] defaultMatColours;
    private MeshRenderer mr;
    private Vector3 initialScale;
    private float hitTime;
    private List<float> hitTimes;
    public ListToText listToText;
    private bool textFileWritten;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        mr = gameObject.GetComponent<MeshRenderer>();

        if (mr != null)
        {
            Debug.Log("Mesh renderer found");
        }

        defaultMats = mr.materials;
        if (defaultMats != null)
        {
            Debug.Log("Default materials found");
        }

        defaultMatColours = new Color[defaultMats.Length];
        hitTimes = new List<float>();

        for (int a = 0; a < defaultMats.Length; a++)
        {
            defaultMatColours[a] = defaultMats[a].color;
            Debug.Log("Colour " + a + " " + defaultMatColours[a]);
        }

        Debug.Log("Array Length =  " + defaultMatColours.Length);

        health = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        for (int a = 0; a < defaultMats.Length; a++)
        {
            if (timeLeft > 0)
            {
                var tempColor = Color.Lerp(defaultMatColours[a],flashColour, Mathf.PingPong(timeLeft/hitFlashDuration, hitFlashDuration));
                mr.materials[a].color = tempColor;
                timeLeft -= Time.deltaTime;
            }
            if (timeLeft <= 0) // Flash off
            {
                mr.materials[a].color = defaultMatColours[a];
            }
            if (timeLeft > 0 && health <= 0)
            {
                var xScale = initialScale.x * (Mathf.Sin(1.5f * Mathf.PI * (-(timeLeft / hitFlashDuration) + 1))+1);
                var yScale = initialScale.y * (Mathf.Sin(1.5f * Mathf.PI * (-(timeLeft / hitFlashDuration) + 1))+1);
                var zScale = initialScale.z * (Mathf.Sin(1.5f * Mathf.PI * (-(timeLeft / hitFlashDuration) + 1))+1);
                Debug.Log(xScale + ", " + yScale + ", " + zScale);

                transform.localScale = new Vector3(xScale, yScale, zScale);
                timeLeft -= Time.deltaTime;
            }
            if (timeLeft <= 0 && health <= 0)
            {
                transform.localScale = new Vector3(0, 0, 0);
                var renderer = GetComponent<Renderer>();
                var collider = GetComponent<Collider>();

                renderer.enabled = false;
                collider.enabled = false;
            }
        }

        

    }

    private void FixedUpdate()
    {
        if (Time.time >= Settings.duration && textFileWritten == false)
        {
            listToText.WriteListToFile(hitTimes, "HitTimes");

            textFileWritten = true;
        }
    }

    public void EnemyWasHit()
    {
        hitTime = Time.time;
        health -= 1;
        Debug.Log(gameObject.name + " health: " + health);
        timeLeft = hitFlashDuration;
        hitTimes.Add(hitTime);
        Debug.Log("Hit counter: " + hitTimes.Count);
        Debug.Log("Hit time: " + hitTime.ToString("F3"));

    }
}
