using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public float duraction;
	public Image image;
	public int mode;

    // Update is called once per frame
    void Update()
    {
    	if (mode == 0)
    	{
        	image.fillAmount -= Time.deltaTime / duraction;
    	}
    	else if (mode == 1)
    	{
    		image.fillAmount += Time.deltaTime / duraction;
    	}

    }
}
