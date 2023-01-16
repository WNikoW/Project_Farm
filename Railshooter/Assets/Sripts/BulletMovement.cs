using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] public float Shootcooldown;
    [SerializeField] public float Schaden;
    [SerializeField] GameObject Explosion;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * Speed,ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collider){
        if(collider.gameObject.tag == "Gegner"){
            collider.gameObject.GetComponent<GegnerScript>().Schaden(Schaden);
        }
        Instantiate(Explosion,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
