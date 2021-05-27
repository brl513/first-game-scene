using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public GameObject reticleNormal;
    public GameObject reticleDetect;
    public GameObject reticleHit;
    private Image normalRenderer;
    private Image detectRenderer;
    private Image hitRenderer;
    public float hitMarkerDuration = 0.5f;
    private float timeLeft;


    // Start is called before the first frame update
    void Start()
    {

        normalRenderer = reticleNormal.GetComponent<Image>();
        detectRenderer = reticleDetect.GetComponent<Image>();
        hitRenderer = reticleHit.GetComponent<Image>();

        normalRenderer.enabled = true;
        detectRenderer.enabled = false;
        hitRenderer.enabled = false;
    }

    // Update is called once per physics update
    void Update()
    {
        if (timeLeft > 0) // Hit marker on
        {
            var tempColor = hitRenderer.color;
            var opacity = timeLeft / hitMarkerDuration;
            hitRenderer.enabled = true;
            tempColor.a = opacity;
            hitRenderer.color = tempColor;
            timeLeft -= Time.deltaTime;
        }
        if (timeLeft <= 0) // Hit marker off
        {
            hitRenderer.enabled = false;
        }
    }

    public void EnemyOutOfSight()
    {

        normalRenderer.enabled = true;
        detectRenderer.enabled = false;
    }

    public void EnemyWasSeeked()
    {
        normalRenderer.enabled = false;
        detectRenderer.enabled = true;
    }

    public void HitMarkerOn()
    {
        timeLeft = hitMarkerDuration; // Resets the timer
    }

   
}
