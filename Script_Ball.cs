using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ball : MonoBehaviour
{
    float speed = 20f;
    Rigidbody rigidbody_Reference;
    Vector3 velocity;
    Renderer renderer;
    void Start()
    {
        rigidbody_Reference = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        rigidbody_Reference.velocity = Vector3.up * speed;
    }
    void FixedUpdate()
    {
        rigidbody_Reference.velocity = rigidbody_Reference.velocity.normalized * speed;
        velocity = rigidbody_Reference.velocity;
        if(!renderer.isVisible)
        {
            Script_Game_Manager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rigidbody_Reference.velocity = Vector3.Reflect(velocity, collision.contacts[0].normal);
    }
}
