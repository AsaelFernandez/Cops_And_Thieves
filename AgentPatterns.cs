using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPatterns : MonoBehaviour
{   
    public GameObject Agent;
    public int Quantity;
    public float Distance = 3.0f;

    public enum Patterns{Circle, Square};
    public Patterns pDropDown;

    // Start is called before the first frame update
    void Start()
    {
        if(pDropDown == Patterns.Circle){
            float agentAngle = 360f / (float)Quantity;
            for (int i=0; i<Quantity; i++){
                Quaternion rotation = Quaternion.AngleAxis(i * agentAngle, Vector3.up);
                Vector3 direction = rotation * Vector3.forward;
        
                Vector3 circlePosition = transform.position + (direction * Distance);
                Instantiate(Agent, circlePosition, Quaternion.identity);
            }
        }
        if(pDropDown == Patterns.Square){
            for(int i=0; i<Quantity; i++){
                for(int j=0; j<Quantity; j++){
                    Vector3 AgentPosition = transform.position + new Vector3(i*Distance, 0.0f, j*Distance);
                    Instantiate(Agent, AgentPosition, Quaternion.identity);
                }
            }
        }
    }
}
