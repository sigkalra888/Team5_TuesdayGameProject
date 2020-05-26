using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{
    public float radius = 5;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            UseElement(Element.hot);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UseElement(Element.cold);
        }
        
    }
    //for debug
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void UseElement(Element e)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //use sphere raycast for manipulate adiacent objects
        foreach(var c in colliders)
        {
            Imanipulate manipulate = c.GetComponent<Imanipulate>();
            if(manipulate!=null)
            {
                manipulate.Change(e);
            }
        }
    }
   
}
