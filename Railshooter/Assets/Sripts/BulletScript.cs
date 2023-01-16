using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void Start(){
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>().SetHitmarkerTrue();
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(this.gameObject,.1f);
    }
}
