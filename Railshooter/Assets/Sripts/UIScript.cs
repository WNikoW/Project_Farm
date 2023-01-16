using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public GameObject Hitmarker;
    public float stayTime;
    float newTime;
    public void SetHitmarkerTrue(){
        audioSource.PlayOneShot(audioSource.clip);
        newTime = Time.time + stayTime;
        Hitmarker.SetActive(true);
    }
    void Update(){
        if(Time.time >= newTime){
            Hitmarker.SetActive(false);
        }
    }
}
