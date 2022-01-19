using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ladron : MonoBehaviour
{
    public double Hp;
    public double Dinero;
    GameObject[] policias;
    GameObject[] guardias;
    bool evade = false;
    GameObject carcel;
    public TextMeshPro mText;
    public GameObject bunker;
    private double[] values= new double[9];  
    private double decision;


    // Start is called before the first frame update
    void Start()
    {
        
        carcel = GameObject.FindGameObjectWithTag("Carcel");
        policias = GameObject.FindGameObjectsWithTag("police");
        Hp = 50;
        Dinero = 0;
        decision = 0.0f;           



    }

    double vidaBaja(double x){
        double level;
        if (x >= 22)
        {
            return 0;}
        if (x<18) { return 1; }
        if (x>=18 && x<=22) { level = (22 - x) / (22 - 18);
            return level;
        }
        return 0;
    }

    double vidaNormal(double x)
    {
        double level;
        if (x < 18)
        {
            return 0;
        }
        if (x >= 18 && x< 22 ) { level = (x - 18) / (22 - 18); return level; }
        if(x>=22 && x < 38) { return 1; }
        if (x >= 38 && x <= 42) { level = (42 - x) / (42 - 38); return level; }
        return 0;
    }

    double vidaAlta(double x)
    {
        double level;
        if (x < 38)
        {
            return 0;
        }
        if (x > 42) { return 1; }
        if (x >= 38 && x <= 42)
        {
            level = (x-38) / (42 - 38);
            return level;
        }
        return 0;
    }

    double DineroBajo(double x)
    {
        double level;
        if (x > 22)
        {
            return 0;
        }
        if (x < 18) { return 1; }
        if (x >= 18 && x <= 22)
        {
            level = (22 - x) / (22 - 18);
            return level;
        }
        return 0;
    }

    double DineroMedio(double x)
    {
        double level;
        if (x < 18)
        {
            return 0;
        }
        if (x >= 18 && x < 22) { level = (x - 18) / (22 - 18); return level; }
        if (x >= 22 && x < 38) { return 1; }
        if (x >= 38 && x <= 42) { level = (42 - x) / (42 - 38);  return level; }
        return 0;
    }

    double DineroAlto(double x)
    {
        double level;
        if (x < 38)
        {
            return 0;
        }
        if (x > 42) { return 1; }
        if (x >= 38 && x <= 42)
        {
            level = (x - 38) / (42 - 38);
            return level;
        }
        return 0;
    }

    double GetLower(double x, double y) {
        if (x < y) { return x; }
        else return y;
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Pd")
        {
            this.transform.position = new Vector3(this.transform.position.x - 2.0f, 0, 0);
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

        if (col.gameObject.tag == "police")
        {
            Hp -= 7.5;
            if (Hp <= 0) { this.gameObject.tag = "carcel"; }
            else { this.transform.position = new Vector3(84.0f, 2.5f, -79.0f); }
            
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        policias = GameObject.FindGameObjectsWithTag("police");
        guardias = GameObject.FindGameObjectsWithTag("guardia");
        evade = false;
        if (this.gameObject.tag == "thief")
        {


            foreach (GameObject p in policias)
                if (Vector3.Distance(p.transform.position, this.transform.position) < 5.5f)
                {

                    //Debug.Log(p);
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(p);
                    evade = true;

                }

            foreach (GameObject p in guardias)
                if (Vector3.Distance(p.transform.position, this.transform.position) < 5.5f)
                {

                    //Debug.Log(p);
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(p);
                    evade = true;

                }

            if (evade)
            {

                this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Evade");
                mText.text = "Escapando de policia";
            }
            else
            {

                int depositar = 17;
                int path = 11;
                int wander = 1;
                

                values[0] = GetLower(DineroBajo(Dinero), vidaAlta(Hp));
                values[1] = GetLower(DineroMedio(Dinero), vidaAlta(Hp));
                values[2] = GetLower(DineroAlto(Dinero), vidaAlta(Hp));
                values[3] = GetLower(DineroBajo(Dinero), vidaNormal(Hp));
                values[4] = GetLower(DineroMedio(Dinero), vidaNormal(Hp));
                values[5] = GetLower(DineroAlto(Dinero), vidaNormal(Hp));
                values[6] = GetLower(DineroBajo(Dinero), vidaBaja(Hp));
                values[7] = GetLower(DineroMedio(Dinero), vidaBaja(Hp));
                values[8] = GetLower(DineroAlto(Dinero), vidaBaja(Hp));
                
                
                decision = ((values[0] * path) + (values[1] * path) + (values[2] * depositar) +
                           (values[3] * path) + (values[4] * path) + (values[5] * depositar) +
                           (values[6] * wander) + (values[7] * depositar) + (values[8] * depositar)) / 
                           (values[0]+ values[1] + values[2] + values[3] + values[4] + values[5] + values[6] + values[7] + values[8])  ;
                if (decision <= 4)
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Wander");
                    mText.text = "Recuperando vida";
                    Hp += Time.deltaTime;
                }

                if (decision >4  && decision <17)
                {
                    this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Path");
                    mText.text = "Buscando banco";
                }

            }

            if (decision >= 17)
            {
                this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
                this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(bunker);
                mText.text = "Depositando";
            }

        }
        if(this.gameObject.tag == "carcel") {
            this.gameObject.GetComponent<AgentBehaviors>().ChangeTarget(carcel);
            this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Seek");
            mText.text = "Llendo a la carcel";
            Dinero = 0;
            if (Vector3.Distance(carcel.transform.position, this.transform.position) < 3.5f) {
                this.gameObject.tag = "reformado";
            }
        }
        if (this.gameObject.tag == "reformado") {
            this.gameObject.GetComponent<AgentBehaviors>().ChangeBehaviors("Wander");
            mText.text = "Reformado";
        }

    }
}
