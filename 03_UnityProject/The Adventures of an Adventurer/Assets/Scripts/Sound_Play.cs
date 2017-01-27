using UnityEngine;
using System.Collections;

public class Sound_Play : MonoBehaviour {
  

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Audio_play(AudioSource audio)
    {
        if (audio != null)
        {
            audio.Play();
        }
    }
}
