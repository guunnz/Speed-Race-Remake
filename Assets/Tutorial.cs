using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI sr;
    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            sr.enabled = false;
            yield return new WaitForSeconds(0.5f);
            sr.enabled = true;
        }
    }


}
