using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private bool shake = false;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shake)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
        }
    }

    public void StartToShake(float shakeDur, float shakeAm, float decreaseFakt)
    {
        shakeDuration = shakeDur;
        shakeAmount = shakeAm;
        decreaseFactor = decreaseFakt;
        shake = true;
        StartCoroutine("DestroyShakeAfterFinished");
    }

    private IEnumerator DestroyShakeAfterFinished()
    {
        yield return new WaitForSeconds(shakeDuration);
        Destroy(this);
    }
}
