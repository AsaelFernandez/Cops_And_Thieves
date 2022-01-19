using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Bunker : MonoBehaviour
{
    GameObject[] ladrones;
    GameObject player;
    public int money;
    public TextMeshPro mText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ladrones = GameObject.FindGameObjectsWithTag("thief");
    }

    // Update is called once per frame
    void Update()
    {
        ladrones = GameObject.FindGameObjectsWithTag("thief");

        foreach (GameObject p in ladrones)
            if (Vector3.Distance(p.transform.position, this.transform.position) < 0.8f )
            {
                Ladron scriptLadron = p.GetComponent<Ladron>();
                scriptLadron.Dinero = 0;
               
            }

        if (Vector3.Distance(player.transform.position, this.transform.position) < 1.5f )
        {
            Movement scriptMovement = player.GetComponent<Movement>();
            money += scriptMovement.Dinero;
            scriptMovement.Dinero = 0;
           
        }
        mText.text = "" + money;
        if (money >= 500)
        {
            SceneManager.LoadScene("Menu");
        }
       
    }
}
