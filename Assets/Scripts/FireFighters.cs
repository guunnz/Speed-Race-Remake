using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Experimental.Rendering.Universal;

public class FireFighters : MonoBehaviour
{
    public float Speed = 0.5f;

    public Light2D lightFirefighter;

    private void OnEnable()
    {
        StartCoroutine(Lights());
    }

    IEnumerator Lights()
    {
        while(true)
        {
            lightFirefighter.enabled = false;
            yield return new WaitForSeconds(0.5f);
            lightFirefighter.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(this.transform.position.x, this.transform.position.y + Speed);
    }



}