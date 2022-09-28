using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class CarMainBehaviour : MonoBehaviour
{
    public float Movement;
    public float TurnSpeed = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public GameManager gm;

    internal float Acceleration = 0;
    public float AccelerationSpeed = 1;

    public float MaxSpeed;
    public float Speed;

    private bool Moving;

    public float Score;

    private bool dead = false;

    public bool IceMode;
    private bool IceLeft;
    private bool IceRight;
    private bool Invincible;

    public CinemachineVirtualCamera vCam;


    public float screenshakeAmplitude;
    public float screenshakeFrequency;
    public float shakeDuration = 0.5f;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighscoreText;
    public TextMeshProUGUI TimerText;


    public GameObject ExtraTimeObject;

    public float TimeGame = 90;
    public float ExtraTime = 20;
    private bool ExtraTimeActivated = false;

    private float lastSpawnChange = 0;

    private int HorsePowers = 15;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Score = (transform.position.y - 10.6f) * 2;
            rb.velocity = new Vector2(rb.velocity.x, Speed);
        }

    }

    private IEnumerator IScreenShake()
    {
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = screenshakeAmplitude;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = screenshakeFrequency;
        Time.timeScale = 0.5f;



        while (Time.timeScale < 1)
        {
            Time.timeScale += Time.deltaTime * 10;
            yield return null;
        }
        Time.timeScale = 1;
        yield return new WaitForSeconds(shakeDuration);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;


        yield return null;
    }

    public void ScreenShake()
    {
        StartCoroutine(IScreenShake());
    }

    private void Move()
    {
        if (dead)
            return;



        if (Input.GetJoystickNames().Any(x => x.Contains("Wheel")))
        {
            Acceleration = (Input.GetAxis("Vertical") + 0.5f) - ((Input.GetAxis("Breaks") + 0.5f) == 0.5f ? 0 : Input.GetAxis("Breaks") + 0.5f);

            if (Acceleration <= 0)
            {
                Acceleration = 0;
            }

            Moving = Acceleration > 0;

            if (Moving)
            {
                Speed += Time.deltaTime * Acceleration * HorsePowers;

                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
            }
            else
            {
                Speed -= Time.deltaTime * HorsePowers * ((Input.GetAxis("Breaks") * 2) + 1.5f);
                if (Speed <= 0)
                {
                    Speed = 0;
                }

            }
            if (Acceleration <= 0)
            {
                Acceleration = 0;
            }
        }
        else
        {
            Moving = Input.GetKey(KeyCode.Space);

            if (Moving)
            {
                Acceleration += Time.deltaTime * AccelerationSpeed;

                Speed += Time.deltaTime * Acceleration * HorsePowers;
                if (Speed >= MaxSpeed)
                {
                    Speed = MaxSpeed;
                }
            }
            else
            {
                Acceleration -= Time.deltaTime;
                Speed -= Time.deltaTime * HorsePowers;
                if (Speed <= 0)
                {
                    Speed = 0;
                }
                if (Acceleration <= 0)
                {
                    Acceleration = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeathTrigger") || collision.CompareTag("Wall"))
        {
            if (Invincible)
                return;

            Debug.Log("Dead");
            dead = true;
            StartCoroutine(DeadCoroutine());
        }

        if (collision.CompareTag("Ice"))
        {
            IceMode = true;
        }


        if (collision.CompareTag("Normal"))
        {
            IceMode = false;
        }
    }


    public IEnumerator DeadCoroutine()
    {
        ScreenShake();
        rb.velocity = Vector2.zero;
        Speed = 0;
        Acceleration = 0;
        IceLeft = false;
        IceRight = false;
        Invincible = true;
        //Screenshake
        //Sonido
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        rb.velocity = Vector2.zero;
        dead = false;
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        Invincible = false;
    }

    void TimeManage()
    {
        if (Score != 0)
        {
            TimeGame -= Time.deltaTime;
        }

        TimerText.text = "<mspace=mspace=18>" + (TimeGame <= 9 ? "0" : "") + TimeGame.ToString("N0") + "</mspace>";


        if (Score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", (int)Mathf.Round(Score));

            int cerosAhead = 4 - Mathf.Round(Score).ToString().Length;


            string ceros = "";


            for (int i = 0; i < cerosAhead; i++)
            {
                ceros += "0";
            }



            ScoreText.text = "<mspace=mspace=18>" + ceros + Mathf.Round(Score).ToString() + "</mspace>";

            HighscoreText.text = "<mspace=mspace=18>" + ceros + Mathf.Round(Score).ToString() + "</mspace>";
        }
        else
        {
            int cerosAhead = 4 - Mathf.Round(Score).ToString().Length;

            string ceros = "";

            for (int i = 0; i < cerosAhead; i++)
            {
                ceros += "0";
            }

            ScoreText.text = "<mspace=mspace=18>" + ceros + Mathf.Round(Score).ToString() + "</mspace>";
            int cerosAheadHS = 4 - PlayerPrefs.GetInt("Highscore").ToString().Length;
            string cerosHS = "";
            for (int a = 0; a < cerosAheadHS; a++)
            {
                cerosHS += "0";
            }

            HighscoreText.text = "<mspace=mspace=18>" + cerosHS + PlayerPrefs.GetInt("Highscore").ToString() + "</mspace>";
        }

        if (TimeGame <= 0)
        {
            if (ExtraTimeActivated)
            {
                TimeGame = ExtraTime;
                ExtraTimeActivated = false;
            }
            else
            {
                // Play Music

                MaxSpeed = 0;
                Speed = 0;
                TimeGame = 0;
                Invoke("ReloadScene", 1f);
            }
        }

        float RoundScore = Mathf.Round(Score);

        if (RoundScore % 2000 == 0 && Score > 100f && !ExtraTimeActivated)
        {
            ExtraTimeActivated = true;
            StartCoroutine(ExtendedTimeAnim());
        }

        if (RoundScore % 500 == 0 && Score > 100f && RoundScore != lastSpawnChange)
        {
            lastSpawnChange = RoundScore;
            gm.SpawnRandomDifference = new Vector2(gm.SpawnRandomDifference.x - 0.2f, gm.SpawnRandomDifference.y - 0.2f);
        }
    }

    IEnumerator ExtendedTimeAnim()
    {
        for (int i = 0; i < 6; i++)
        {
            ExtraTimeObject.SetActive(true);
            yield return new WaitForSeconds(0.175f);
            ExtraTimeObject.SetActive(false);
            yield return new WaitForSeconds(0.175f);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        TimeManage();
        if (dead || TimeGame <= 0)
            return;

        Move();

        if (Input.GetJoystickNames().Any(x => x.Contains("Wheel")))
        {
            float x = Input.GetAxis("Horizontal");

            if (Mathf.Abs(x) < 0.01f)
            {
                x = 0;
            }



            if (IceMode)
            {
                if (x < 0)
                {
                    IceLeft = true;
                    IceRight = false;

                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
                else if (x > 0)
                {
                    IceLeft = false;
                    IceRight = true;
                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }


                if (IceRight)
                {
                    Movement += Time.deltaTime * (TurnSpeed * (Mathf.Abs(x) + 0.5f));

                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }
                else if (IceLeft)
                {
                    Movement -= Time.deltaTime * (TurnSpeed * (Mathf.Abs(x) + 0.5f));

                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
            }
            else
            {
                Movement = x * 3.5f;
                if (x < 0)
                {
                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
                else if (x > 0)
                {
                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(Movement, this.transform.position.y, this.transform.position.z), 0.1f);
        }
        else
        {
            if (IceMode)
            {

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Movement -= Time.deltaTime * TurnSpeed;
                    IceLeft = true;
                    IceRight = false;

                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Movement += Time.deltaTime * TurnSpeed;
                    IceLeft = false;
                    IceRight = true;
                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }


                if (IceRight && !Input.GetKey(KeyCode.RightArrow))
                {
                    Movement += Time.deltaTime * (TurnSpeed / 2);

                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }
                else if (IceLeft && !Input.GetKey(KeyCode.LeftArrow))
                {
                    Movement -= Time.deltaTime * (TurnSpeed / 2);

                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Movement -= Time.deltaTime * TurnSpeed;

                    if (Movement <= -3.5f)
                    {
                        Movement = -3.5f;
                        return;
                    }
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Movement += Time.deltaTime * TurnSpeed;

                    if (Movement >= 3.5f)
                    {
                        Movement = 3.5f;
                        return;
                    }
                }
            }
            this.transform.position = new Vector3(Movement, this.transform.position.y, this.transform.position.z);
        }
    }
}