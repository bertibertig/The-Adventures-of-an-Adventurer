using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Fire : MonoBehaviour {

    public float minValue;
    public float maxValue;

    System.Random rnd;
    Light fire;
    bool lightMinus;

	void Start () {
        if(minValue <= 0)
            minValue = 0.3f;
        if (minValue >= maxValue)
            maxValue = 0.4f;
        lightMinus = false;
        fire = this.gameObject.GetComponent<Light>();
        rnd = new System.Random();
        StartCoroutine("Flame");
	}

    IEnumerator Flame()
    {
        while(true)
        {
            int intervall = rnd.Next(0, 1);
            for (int i = 0; i <= 10; i++)
            {
                FlameFlicker();
                yield return new WaitForSeconds(intervall);
            }
        }
    }

    private void FlameFlicker()
    {
        print(lightMinus);
        if (fire.intensity == maxValue)
            lightMinus = true;
        else if (fire.intensity == minValue)
            lightMinus = false;

        if (lightMinus)
            fire.intensity -= 0.01f;
        else
            fire.intensity += 0.01f;

    }
}
