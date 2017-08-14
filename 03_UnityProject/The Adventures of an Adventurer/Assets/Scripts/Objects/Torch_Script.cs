using UnityEngine;
using System.Collections;
using System;

public class Torch_Script : MonoBehaviour {
	private bool transitionDone = true;
	private float goal = 0;
	private float current = 0;
    public bool fixedValues = false;
    public bool useIgnoredIntensity = false;
    public bool fadeRange = false;
    public bool fadeIntensity = false;
	public float step = 0.1f; //Value the intensity changes each frame  || maximum of 0.1!
    public float delay = 0.1f; //Delay for change of Value
	public float min_intensity = 4; //minimum intensity the torch can reach
	public float max_intensity = 6; //maximum intensity the torch can reach
	public float min_angle = 22f; //minimum angle the torch can reach
	public float max_angle = 25f; //maximum angle the torch can reach
	public float min_ignored_intensity = 5f;
	public float max_ignored_intensity = 5.5f;

	// Use this for initialization
	void Start () {
		current = GetComponent<Light> ().intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (transitionDone) {
			transitionDone = false;
            if (fixedValues)
            {
                if(goal == min_intensity || goal == 0)
                {
                    goal = max_intensity;
                }
                else
                {
                    goal = min_intensity;
                }
            }
            else
            {
                do
                {
                    goal = (float)Math.Round(RandomValueInRange(min_intensity, max_intensity), 2);
                } while ((goal < max_ignored_intensity && goal > min_ignored_intensity) && useIgnoredIntensity);
            }
			StartCoroutine ("approachIntensity");
		}
	}

	IEnumerator approachIntensity ()
	{
		while ((int)(current*10) != (int)(goal*10))
		{
			current = (float)Math.Round(current, 2);
            if (fadeIntensity)
            {
                GetComponent<Light>().intensity = current;
            }
            if (fadeRange)
            {
                GetComponent<Light>().spotAngle = ((max_angle - min_angle) * ((current - min_intensity) / (max_intensity - min_intensity))) + min_angle;
            }
			if ((int)(current*10) < (int)(goal*10)) {
				current += step;
			}
			if ((int)(current*10) > (int)(goal*10)) {
				current -= step;
			}
			yield return new WaitForSeconds(delay);

			if (current + 0.01f == goal || current - 0.01f == goal
				|| current + 0.02f == goal || current - 0.02f == goal
				|| current + 0.03f == goal || current - 0.03f == goal
				|| current + 0.04f == goal || current - 0.04f == goal
				|| current + 0.05f == goal || current - 0.05f == goal
				|| current + 0.06f == goal || current - 0.06f == goal
				|| current + 0.07f == goal || current - 0.07f == goal
				|| current + 0.08f == goal || current - 0.08f == goal
				|| current + 0.09f == goal || current - 0.09f == goal){
				current = goal;
			}
		}
		transitionDone = true;
	}

	private float RandomValueInRange(float a, float b)
	{
		float random = UnityEngine.Random.value;
		float diff = b - a;
		float r = random * diff;
		return a + r;
	}
}
