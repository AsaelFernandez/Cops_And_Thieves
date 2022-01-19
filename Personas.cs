using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Personas : MonoBehaviour
{
    GameObject player;
    GameObject hospital;
    GameObject[] ladrones;
    bool evade = false;
    public TextMeshPro mText;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Pd")
        {
            this.transform.position = new Vector3(this.transform.position.x - 2.0f, 0,0);
        }
        if (col.gameObject.tag == "Pi")
        {
            this.transform.position = new Vector3(this.transform.position.x + 2.0f, 0, 0);
        }
        if (col.gameObject.tag == "Pu")
        {
            this.transform.position = new Vector3(0, 0, this.transform.position.z - 2.0f);
        }
        if (col.gameObject.tag == "Pa")
        {
            this.transform.position = new Vector3(0, 0, this.transform.position.z + 2.0f);
        }

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "thief")
        {
            this.gameObject.tag = "herido";
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hospital = GameObject.FindGameObjectWithTag("Hospital");
        ladrones = GameObject.FindGameObjectsWithTag("thief");

    }

    // Update is called once per frame
    void Update()
    {
        ladrones = GameObject.FindGameObjectsWithTag("thief");
        evade = false;

        if (this.gameObject.tag == "civil") {
            if (Vector3.Distance(player.transform.position, this.transform.position) > 7.0f)
            {
                foreach (GameObject p in ladrones)
                    if (Vector3.Distance(p.transform.position, this.transform.position) < 6.0f)
                    {

                        this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(p);
                        evade = true;

                    }
                if (evade)
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Evade");
                    mText.text = "Esquivando ladron";
                }
                else {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Wander");
                    mText.text = "Paseando";
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(player);
                }

            }
            else {
                this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(player);
                this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Flee");
                mText.text = "Escapando de Jugador";

            }
       
        
        }

        if (this.gameObject.tag == "herido") {
            this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(hospital);
            this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
            mText.text = "Buscando Hospital";
            if (Vector3.Distance(hospital.transform.position, this.transform.position) < 3.5f)
            {
                this.gameObject.tag = "civil";
            }

        }

        
    }
}
