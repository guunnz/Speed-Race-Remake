using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
public enum TrackType
{
    Normal = 0,
    Ice = 1,
    Bridge = 2,
}

[System.Serializable]
public class Track
{
    public int Order;
    public Transform TrackTransform;
    public TrackType TrackType;
}

public class TrackManager : MonoBehaviour
{
    public List<Track> tracks = new List<Track>();
    public List<Transform> RoadLefts = new List<Transform>();
    public List<Transform> RoadRights = new List<Transform>();

    public CarMainBehaviour Player;

    public bool NightMode;

    public TrackType CurrentTrack;

    public Light2D globalLight;
    public Light2D playerLight;

    private float LastPos = 10.6f;

    private float NextPos = 10.6f;

    public Transform LightToDark;

    public Transform SpawnPointBomberos;
    public Transform Bomberos;

    public float SpeedToMoveTrackLeft = 0.1f;
    public float SpeedToMoveTrackRight = 0.1f;
    public float MinXValue = 5f;
    public float MaxXValue = 4.25f;

    private float MaxXAux = 6f;
    private float MinXAux = 4.3f;

    public float MaxXMinRandom = 5.5f;
    public float MinXMaxRandom = 4.75f;

    public bool GoOutPath = true;

    public bool BomberosOut = false;
    public GameObject BridgeWarning;

    private float XValuePathAux = 4.5f;

    private float MovingTrackAcceleration = 0;

    private float LastActivation;

    public void ToggleNightMode()
    {
        LastActivation = Player.Score;
        StartCoroutine(IToggleNightMode());
    }

    public IEnumerator WarningBridge()
    {
        BridgeWarning.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BridgeWarning.SetActive(false);

        CurrentTrack = TrackType.Bridge;
    }

    public IEnumerator IToggleNightMode()
    {
        if (!NightMode)
        {
            playerLight.enabled = true;
            LightToDark.transform.localPosition = new Vector3(0, 33, 5);
        }
        yield return new WaitForFixedUpdate();
        LightToDark.DOLocalMove(new Vector3(0, !NightMode ? 0 : -33f, 5), 1.5f);

        NightMode = !NightMode;

        yield return new WaitForSeconds(1.5f);

        if (!NightMode)
        {
            playerLight.enabled = NightMode;
        }

    }

    public void ChangeLevel(TrackType Type)
    {
        LastActivation = Player.Score;
        if (Type == TrackType.Bridge)
        {
            StartCoroutine(WarningBridge());
            return;
        }
        CurrentTrack = Type;
    }

    private void FixedUpdate()
    {
        if (Mathf.Round(Player.transform.position.y) > NextPos && Player.transform.position.y != 0)
        {
            MoveTracks();
        }
    }

    private void Start()
    {
        MinXAux = MinXValue;
        MaxXAux = MaxXValue;
        MinXValue = Random.Range(MinXValue, MinXMaxRandom);
        MaxXValue = Random.Range(MaxXMinRandom, MaxXValue);

        GoOutPath = Random.Range(0, 2) == 1;
    }

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            BomberosOut = true;
            Bomberos.transform.position = SpawnPointBomberos.transform.position;
            Bomberos.gameObject.SetActive(true);
        }

        if (Player.Speed > 0)
        {
            if (GoOutPath)
            {
                if (XValuePathAux <= MaxXValue)
                {

                    XValuePathAux += (Time.deltaTime / Random.Range(5, 7) * Player.Acceleration) * MovingTrackAcceleration;

                    if (Mathf.Abs(XValuePathAux - MaxXValue) < 0.1f)
                    {
                        MovingTrackAcceleration -= Time.deltaTime;
                    }
                    else
                    {
                        MovingTrackAcceleration += Time.deltaTime;
                    }
                    if (MovingTrackAcceleration >= 1)
                    {
                        MovingTrackAcceleration = 1;
                    }
                    else if (MovingTrackAcceleration <= 0.1f)
                    {
                        MovingTrackAcceleration = 0.1f;
                    }
                }
                else
                {
                    MovingTrackAcceleration = 0.1f;
                    MaxXValue = Random.Range(MaxXAux, MaxXMinRandom);
                    GoOutPath = false;
                }

            }
            else
            {
                if (XValuePathAux >= MinXValue)
                {
                    XValuePathAux -= (Time.deltaTime / Random.Range(5, 7) * Player.Acceleration) * MovingTrackAcceleration;
                    if (Mathf.Abs(XValuePathAux - MinXValue) < 0.1f)
                    {
                        MovingTrackAcceleration -= Time.deltaTime;
                    }
                    else
                    {
                        MovingTrackAcceleration += Time.deltaTime;
                    }
                    if (MovingTrackAcceleration >= 1)
                    {
                        MovingTrackAcceleration = 1;
                    }
                    else if (MovingTrackAcceleration <= 0.1f)
                    {
                        MovingTrackAcceleration = 0.1f;
                    }
                }
                else
                {
                    MovingTrackAcceleration = 0.1f;
                    MinXValue = Random.Range(MinXAux, MinXMaxRandom);
                    GoOutPath = true;
                }
            }
        }

        RoadLefts.ForEach(x =>
        {
            x.transform.localPosition = new Vector3(-XValuePathAux, x.transform.localPosition.y, x.transform.localPosition.z);
        });


        RoadRights.ForEach(x =>
        {
            x.transform.localPosition = new Vector3(XValuePathAux, x.transform.localPosition.y, x.transform.localPosition.z);
        });

        if (Bomberos.gameObject.activeSelf)
        {
            if (Vector2.Distance(Bomberos.transform.position, SpawnPointBomberos.transform.position) >= 19f)
            {
                BomberosOut = false;
                Bomberos.gameObject.SetActive(false);
            }
        }

        if (Player.Score >= 600 && CurrentTrack != TrackType.Ice && Player.Score <= 700)
        {
            ChangeLevel(TrackType.Ice);
        }
        else if (Player.Score >= 900 && CurrentTrack != TrackType.Normal && Player.Score <= 1000)
        {
            ChangeLevel(TrackType.Normal);
        }
        else if (Player.Score >= 1000 && CurrentTrack == TrackType.Normal && Player.Score <= 1050 && !BomberosOut)
        {
            LastActivation = Player.Score;
            BomberosOut = true;
            Bomberos.gameObject.SetActive(true);
            Bomberos.transform.position = SpawnPointBomberos.transform.position;
        }
        else if (Player.Score >= 1500 && LastActivation < 1500 && NightMode == false)
        {
            ToggleNightMode();
        }
        else if (Player.Score >= 1750 && LastActivation < 1750 && NightMode == true)
        {
            ToggleNightMode();
        }
        else if (Player.Score >= 2500 && LastActivation < 2500)
        {
            ChangeLevel(TrackType.Bridge);
        }
        else if (Player.Score >= 3000 && LastActivation < 3000 && CurrentTrack != TrackType.Normal)
        {

            ChangeLevel(TrackType.Normal);
        }
        else if (Player.Score >= 3100 && LastActivation < 3100 && CurrentTrack == TrackType.Normal && !BomberosOut)
        {
            LastActivation = Player.Score;
            BomberosOut = true;
            Bomberos.transform.position = SpawnPointBomberos.transform.position;
            Bomberos.gameObject.SetActive(true);
        }
        else if (Player.Score >= 3400 && LastActivation < 3500 && CurrentTrack != TrackType.Ice)
        {
            ToggleNightMode();
            ChangeLevel(TrackType.Ice);
        }
        else if (Player.Score >= 3750 && LastActivation < 3750 && CurrentTrack != TrackType.Normal)
        {
            ToggleNightMode();
            ChangeLevel(TrackType.Normal);
        }
        else if (Player.Score >= 4500 && LastActivation < 4500 && CurrentTrack != TrackType.Bridge)
        {
            ToggleNightMode();
            ChangeLevel(TrackType.Bridge);
        }
        else if (Player.Score >= 5000 && LastActivation < 5000 && CurrentTrack != TrackType.Normal)
        {
            ToggleNightMode();
            ChangeLevel(TrackType.Normal);
        }
    }

    public void MoveTracks()
    {
        LastPos += 10.6f;

        NextPos = LastPos + 10.6f;

        for (int i = 0; i <= (int)TrackType.Bridge; i++)
        {
            List<Track> currentTracks = tracks.Where(x => x.TrackType == (TrackType)i).ToList();

            Track firstTrack = currentTracks.Single(x => x.Order == 1);
            Track midTrack = currentTracks.Single(x => x.Order == 2);
            Track postMid = currentTracks.Single(x => x.Order == 3);
            Track lastTrack = currentTracks.Single(x => x.Order == 4);

            firstTrack.TrackTransform.gameObject.SetActive(firstTrack.TrackType == CurrentTrack);


            firstTrack.TrackTransform.position = new Vector3(0, lastTrack.TrackTransform.position.y + 10.6f, 0);

            midTrack.Order = 1;
            postMid.Order = 2;
            lastTrack.Order = 3;
            firstTrack.Order = 4;
        }
    }
}