using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
	public Color[] colors;

	public Text[] texts;
	public Image[] images;
	public Image[] backgrounds;

	public int currentColorIndex;
	public Color currentColor;

	GameObject findPlayer;

	void Start()
	{
		currentColorIndex = PlayerPrefs.GetInt("CurrentUIColor", 0);
		findPlayer = GameObject.FindGameObjectWithTag("Player");
		ChangeUIColor();
	}

    void Update()
    {
        currentColor = colors[currentColorIndex];

        if (Input.GetKeyDown(KeyCode.C))
        {
        	if (findPlayer == null)
	    	{	
	        	currentColorIndex ++;
		    	if (currentColorIndex >= colors.Length)
		    	{
		    		currentColorIndex = 0;
		    	}
	        	ChangeUIColor();
	        }
	        else 
	        {
	        	Destroy(gameObject);
	        }	
        }
    }

    void ChangeUIColor()
    {
    	PlayerPrefs.SetInt("CurrentUIColor", currentColorIndex);

    	foreach (Text text in texts)
        {
        	text.color = colors[currentColorIndex];
        }
        foreach (Image image in images)
        {
        	image.color = colors[currentColorIndex];
        }
        foreach (Image bg in backgrounds)
        {
        	Color bgColor = colors[currentColorIndex];
        	bgColor.a = 0.6f;
        	bg.color = bgColor;
        }
    }
}
