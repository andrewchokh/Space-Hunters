using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BackgroundMovement : MonoBehaviour
{
    public float speed;
    public GameObject background;

    void Start()
    {
        speed = 9f;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x <= -28.75f) {
        	Instantiate(background, new Vector3(28.75f, 0, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        	Destroy(gameObject);
        }
    }
}
