using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : MonoBehaviour
{
    private CarMainBehaviour CarMain;
    private SpriteRenderer sr;
    private float moveTo;

    public float UpSpeed = 0.1f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CarMain = GameObject.FindObjectOfType<CarMainBehaviour>();
        moveTo = Random.Range(0,2) == 0 ? Random.Range(-1.5f, -1f) : Random.Range(1f, 1.5f);

        SetRandomColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            moveTo *= -1;
        }
    }

    public void Respawn()
    {
        SetRandomColor();
        moveTo = Random.Range(0, 2) == 0 ? Random.Range(-1.5f, -1f) : Random.Range(1f, 1.5f);
    }

    private void SetRandomColor()
    {
        int ColorR = Random.Range(0, 2);
        int ColorG = Random.Range(0, 2);
        int ColorB = Random.Range(0, 2);

        if (ColorG == 0 && ColorB == 0 && ColorR != 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                ColorG = 1;
            }
            else
            {
                ColorB = 1;
            }

            ColorR = 0;
        }
        else if (ColorG == 1 && ColorB == 1 && ColorR == 1)
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    ColorR = 0;
                    break;
                case 1:
                    ColorG = 0;
                    break;
                case 2:
                    ColorB = 0;
                    break;
                default:
                    break;
            }

        }
        else if (ColorG == 0 && ColorB == 0 && ColorR == 0)
        {
            ColorR = 1;

            if (Random.Range(0, 2) == 0)
            {
                ColorG = 1;
            }
            else
            {
                ColorB = 1;
            }
        }

        sr.color = new Color(ColorR, ColorG, ColorB, 1);
    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector2(this.transform.position.x - (moveTo * (Time.deltaTime / 1.5f)), this.transform.position.y + UpSpeed);


    }

    private void Update()
    {
        if (this.transform.position.x <= -2.9f || this.transform.position.x >= 2.9f)
        {
            moveTo *= -1;

            if (this.transform.position.x < 0)
            {
                this.transform.position = new Vector3(-2.89f, this.transform.position.y);
            }
            else
            {
                this.transform.position = new Vector3(2.89f, this.transform.position.y);
            }
        }
    }
}