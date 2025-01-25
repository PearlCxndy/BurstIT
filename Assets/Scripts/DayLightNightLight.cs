using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayLightNightLight : MonoBehaviour
{
    public Gradient sunLightGradient; // A Gradient to define the colours over time
    public float daytime = 60f; // The length of the day in seconds

    private Light2D sunLight; // Reference to the Global Light 2D component
    private float timeElapsed;


    // Start is called before the first frame update
    void Start()
    {   
        // Automatically get the Light2D component from the attached GameObject
        sunLight = GetComponent<Light2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        // Increment time, looping back after a full day
        float gameTime = (timeElapsed % daytime) / daytime;

        // Update the light colour based on the gradient
        sunLight.color = sunLightGradient.Evaluate(gameTime);
    }
}
