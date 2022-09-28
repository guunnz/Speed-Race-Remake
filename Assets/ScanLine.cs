using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanLine : MonoBehaviour
{
    public float MaxY;
    public float MinX;
    public float Speed;

    void FixedUpdate()
    {
        transform.localPosition = new Vector2(this.transform.localPosition.x, this.transform.localPosition.y - (Time.deltaTime * Speed));

        if (transform.localPosition.y <= MinX)
        {

            MaxY += Random.Range(-1, 1f);
            if (MaxY < 9f)
            {
                MaxY = 9;
            }
            this.transform.localPosition = new Vector2(this.transform.position.x, MaxY);
        }
    }
}
