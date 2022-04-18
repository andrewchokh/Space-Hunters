using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackSystem : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) & Input.GetKeyDown(KeyCode.H))
        {
        	PlayerPrefs.SetInt("Coins", 9999999);
        	PlayerPrefs.SetInt("BestScore", 9999999);
        }
    }
}
