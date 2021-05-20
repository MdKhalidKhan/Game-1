using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Strick : MonoBehaviour
{
    Rigidbody rigidbody_reference;
    void Start()
    {
        
        rigidbody_reference = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody_reference.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x, -32, 0));
    }
}
