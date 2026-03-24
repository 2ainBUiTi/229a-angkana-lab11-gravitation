using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f; //Gravitialnal Constant 6.674

    //create a List of objects in the galaxy to attract
    public static List<Gravity> otherObjectsList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjectsList == null)
        {
            otherObjectsList = new List<Gravity>();
        }

        //add object (with gravity script) to attract to the list
        otherObjectsList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (Gravity obj in otherObjectsList) {
            if (obj != this) {
                Attract(obj);
            }
        }
    }

    void Attract(Gravity other)
    {
        Rigidbody otherRb = other.rb;

        //get direction between 2 objects
        Vector3 direction = rb.position - otherRb.position;

        //get only distance between 2 objs
        float distance = direction.magnitude;

        //if 2 objs are at the same position, just return = do nothing to avoid collision
        if (distance == 0f) { return; }

        //F = G*((m1*m2)/r*r
        float forceMagnitude = G*(rb.mass * otherRb.mass)/Mathf.Pow(distance, 2);

        //Combine force magnitude with its diretion (normalize)
        //to form final gravitianal force (Vector3)
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        //apply gravitation force to other obj
        otherRb.AddForce(gravityForce);
    }
}
