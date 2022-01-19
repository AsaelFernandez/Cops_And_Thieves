using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviors : MonoBehaviour
{   
    public enum Behaviors{Seek, Flee, Pursuit, Evade, Wander, Path};
    public Behaviors bDropDown;
    public Transform[] bankPath;
    public Transform[] policePath;
    public Transform[] carcel;
    private int currentPath;

    [Header("Agent")]
    [Range(0, 10)] public float agentSpeed = 1.0f;
    public Vector3 aMovement;
    private Vector3 aHeading;


    [Header("Target")]
    public GameObject Target;
    public GameObject Jugador;

    void Start()
    {
        currentPath = Random.Range(0, 20);

        aMovement = this.transform.forward*agentSpeed;
        aHeading = aMovement.normalized;
    }

    public void ChangeTarget(GameObject t) {

        Target = t;
    }
    public void ChangeBehaviors( string a)
    {
        if (a == "Seek") {
            bDropDown = Behaviors.Seek;
        }
        if (a == "Evade")
        {
            bDropDown = Behaviors.Evade;
        }
        if (a == "Flee")
        {
            bDropDown = Behaviors.Flee;
        }
        if (a == "Persuit")
        {
            bDropDown = Behaviors.Pursuit;
        }
        if (a == "Path")
        {
            bDropDown = Behaviors.Path;
        }
        if (a == "Wander")
        {
            bDropDown = Behaviors.Wander;
        }
    }

    void LateUpdate()
    {   
        float dTime = Time.deltaTime;
        Vector3 aDirection = Vector3.zero;
        if (!Target) {
            Target = Jugador;
        }
        Vector3 tPosition = Target.transform.position;

       
            switch(bDropDown){
                
                case Behaviors.Seek:
                    aDirection = Seek(tPosition);
                    break;
                case Behaviors.Flee:
                    aDirection = Flee(tPosition);
                    break;
                case Behaviors.Pursuit:
                    aDirection = Pursuit(tPosition);
                    break;
                case Behaviors.Evade:
                Vector3 aaa = Evade(tPosition);
                    aaa.y =0;
                    aDirection = Evade(tPosition);
                    break;
                case Behaviors.Wander:
                    aDirection = Wander();
                    break;
                case Behaviors.Path:
                    aDirection = Path();
                    break;
            }
        

        aDirection.Normalize();
        Vector3 aMoveD = aDirection - aHeading;
        Vector3 aMoveC = aHeading + (aMoveD * 0.1f);

        Debug.DrawRay(this.transform.position, aDirection*2.0f, Color.yellow);
        Debug.DrawRay(this.transform.position, this.transform.forward*1.0f, Color.red);

        aMoveC *= agentSpeed;
        aMoveC = Vector3.ClampMagnitude(aMoveC, agentSpeed);
        aHeading = aMoveC.normalized;

        Vector3 newPosition = this.transform.position + (aMoveC * dTime);
        transform.position = newPosition;

        float aSight = Vector3.SignedAngle(transform.forward, aMoveC, Vector3.up);
        this.transform.Rotate(0.0f, aSight, 0.0f, Space.World);

    }

   
    public Vector3 Seek(Vector3 bTarget){
        Vector3 direction;
        direction = bTarget - transform.position;
        direction.y = 0;

        //direction.Normalize();
        return direction;
    }

    public Vector3 Flee(Vector3 bTarget){
        Vector3 direction;
        direction = transform.position - bTarget;

        direction.y = 0;

        //direction.Normalize();
        return direction;
    }

    public Vector3 Pursuit(Vector3 bTarget){
        Vector3 direction;
        Vector3 toPrey = bTarget - transform.position;

        AgentBehaviors tScript = GetComponent<AgentBehaviors>();
        Vector3 tDirection = tScript.aMovement;
        float targetSpeed = tScript.agentSpeed;

        float LookAhead = Vector3.Magnitude(toPrey) / (agentSpeed + targetSpeed);
        Vector3 fPos = bTarget + (tDirection * LookAhead);        
        direction = Seek(fPos);
        direction.y = 0;
        return direction;
    }

    public Vector3 Evade(Vector3 bTarget){
        if(Vector3.Distance(bTarget, transform.position)<5.5f){
            return Flee(bTarget);

        }else{
            return Wander();
        }
        
    }

    public Vector3 Wander(){
        Vector3 direction;
        Vector3 wTarget;

        float randomVx = Random.Range(-0.5f,0.5f);
        float randomVz = Random.Range(-0.5f,0.5f);
        wTarget = transform.forward + new Vector3(transform.forward.x + randomVx*30000 , 0 , transform.forward.z + randomVz*30000);
       
        direction = Seek(wTarget);
        direction.y = 0;
        return direction;
    }

    public Vector3 Path(){
        Vector3 direction= new Vector3(0,0,0);
        if (gameObject.tag == "thief") {
            currentPath = (currentPath) % bankPath.Length;

            if (Vector3.Distance(transform.position, bankPath[currentPath].position) >0.8f)
            {

                direction = Seek(bankPath[currentPath].position);

            }
            else {

                currentPath = (currentPath + 1) %bankPath.Length;
            
            }

        
        }

        if (gameObject.tag == "police")
        {
            currentPath = (currentPath) % policePath.Length;
            if (Vector3.Distance(transform.position, policePath[currentPath].position) > 0.8f)
            {

                direction = Seek(policePath[currentPath].position);

            }
            else
            {

                currentPath = (currentPath + 1) % policePath.Length;

            }


        }
        if (gameObject.tag == "guardia")
        {
            currentPath = (currentPath) % policePath.Length;
            if (Vector3.Distance(transform.position, policePath[currentPath].position) > 0.8f)
            {

                direction = Seek(policePath[currentPath].position);

            }
            else
            {

                currentPath = (currentPath + 1) % policePath.Length;

            }


        }
        direction.y = 0;
        return direction;
        
       
        
        
    }
}
