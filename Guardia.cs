using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Guardia : MonoBehaviour
{
    GameObject[] ladrones;
    GameObject[] policias;
    public GameObject banco;

    

    bool evade = false;
    bool seek = false;
    GameObject player;
    public TextMeshPro mText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //banco = GameObject.FindGameObjectWithTag("Hospital");
        ladrones = GameObject.FindGameObjectsWithTag("thief");
        policias = GameObject.FindGameObjectsWithTag("police");
    }
    void OnCollisionEnter(Collision col)
    {

        

        if (col.gameObject.tag == "thief" || col.gameObject.tag == "carcel")
        {
            Destroy(col.gameObject);
            this.gameObject.tag = "regresando";
        }
        if (col.gameObject.tag == "police" || col.gameObject.tag == "herido")
        {
            Destroy(col.gameObject);
           
        }


        if (col.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Menu");
        }

    }
    // Update is called once per frame
    void Update()
    {
        ladrones = GameObject.FindGameObjectsWithTag("thief");
        policias = GameObject.FindGameObjectsWithTag("police");
        evade = false;
        seek = false;
        if (this.gameObject.tag == "guardia")
        {//Si no detecta al jugador cerca si se pone a buscar a los demas
            if (Vector3.Distance(player.transform.position, this.transform.position) > 7.0f)
            {
                //checa las distancias de los ladrones
                foreach (GameObject p in ladrones)
                    if (Vector3.Distance(p.transform.position, this.transform.position) < 6.0f)
                    {

                        this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(p);
                        seek = true;

                    }
                foreach (GameObject p in policias)
                    if (Vector3.Distance(p.transform.position, this.transform.position) < 3.0f)
                    {
                        this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(p);
                        evade = true;
                    }

                if (seek)
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
                    mText.text = "Persiguiendo ladron";
                }
                else if (evade)
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
                    mText.text = "Persiguiendo policia";

                }
                else
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Path");
                    mText.text = "Cuidando bunker";
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(player);
                }

            }
            else
            {
                this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(player);
                this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Persuit");
                mText.text = "Persiguiendo Jugador";

            }
            
        }
        if (this.gameObject.tag == "regresando")
        {
            this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(banco);
            this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
            mText.text = "Regresando Dinero";
            if (Vector3.Distance(banco.transform.position, this.transform.position) < 3.5f)
            {
                this.gameObject.tag = "guardia";
            }
        }

    }
}
