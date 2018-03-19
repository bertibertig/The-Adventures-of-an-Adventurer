using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnlyOnSpecificDevice : MonoBehaviour {

    public RuntimePlatform[] devices;

	void Start () {
        if(devices != null && devices.Length != 0)
            StartCoroutine("CheckDevices");
	}

    IEnumerator CheckDevices()
    {
        bool needsToBeActive = false;
        foreach(RuntimePlatform device in devices)
        {
            if (device == Application.platform)
                needsToBeActive = true;
            yield return null;
        }
        this.gameObject.SetActive(needsToBeActive);
    }
}
