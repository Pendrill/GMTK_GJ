using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Shakes the camera on call
public class CameraShake : MonoBehaviour {

    //Used to see if we need to override a running routine
    bool isShaking = false;

    //Used to see if we need to override a running routine
    bool overrideRoutine = false;

    Vector3 originalPosition;

    //Save original position of camera
    void Start()
    {
        originalPosition = transform.position;
    }

    //Coroutine to shake the camera given duratio nand strength. Will exit if override is true.
    public IEnumerator ShakeCameraRoutine(float duration, float strength)
    {
        isShaking = true;
        float time = 0;
        while(time < duration)
        {
            float timeStep = time / (duration / 2);
            if(timeStep > 1)
            {
                timeStep = 1 - (timeStep - 1);
            }
            Debug.Log(timeStep);
            float multiplier = Mathf.Lerp(0 * strength, 1.0f * strength, timeStep);
            Vector2 random = Random.insideUnitCircle * strength;
            transform.position = new Vector3(
                originalPosition.x + random.x,
                originalPosition.y + random.y,
                -1);

            time += Time.deltaTime;
            if (overrideRoutine)
            {
                overrideRoutine = false;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.position = originalPosition;
        isShaking = false;
    }

    //Shakes the camera given duration and strength.
    //Overrides a previous camera shake if in process with the new one.
	public void ShakeCamera(float duration, float strength)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCameraRoutine(duration, strength));
        }
        else
        {
            overrideRoutine = true;
            StartCoroutine(ShakeCameraRoutine(duration, strength));
        }
      
    }
}
