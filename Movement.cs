using UnityEngine;
using System.Collections;
using TMPro;

public class Movement : MonoBehaviour
{

    public TextMeshPro mText;

    public int Dinero=0;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
    }
    void Update()
    {
        mText.text = ""+ Dinero;

        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
        {
            transform.position+= new Vector3(0.0f,0.0f,0.05f);
            
        }

        if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
        {
            transform.position+= new Vector3(0.0f,0.0f,-0.05f);
           
        }

        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position+= new Vector3(-0.05f,0.0f,0.0f);
            
        }

        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
            transform.position+= new Vector3(0.05f,0.0f,0.0f);
           
        }
    }
}