using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 _originRotation;

    private void Start()
    {
        _originRotation = transform.rotation.eulerAngles;
    }

    public IEnumerator COShake(float shakeAmount, float shakeTime)
    {
        float timer = 0;
        while (timer <= shakeTime)
        {
            Camera.main.transform.rotation = Quaternion.Euler(_originRotation + (Vector3)UnityEngine.Random.insideUnitCircle * shakeAmount);
            timer += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.rotation = Quaternion.Euler(_originRotation);
    }
}
