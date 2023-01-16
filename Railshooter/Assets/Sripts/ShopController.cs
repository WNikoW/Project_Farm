using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [Header ("Lock")]
    [SerializeField] GameObject MinigunLock;
    [SerializeField] GameObject SniperLock;
    [SerializeField] GameObject RaketenwerferLock;
    [Header ("Shop UI Waffen")]
    [SerializeField] GameObject UnlockMinigun;
    [SerializeField] GameObject UnlockSniper;
    [SerializeField] GameObject UnlockRaketenwerfer;
    [SerializeField] GameObject MinigunLockUI;
    [SerializeField] GameObject SniperLockUI;
    [SerializeField] GameObject RaketenwerferLockUI;
    [Header ("Shop UI Schild")]
    [SerializeField] GameObject UnlockSchild1;
    [SerializeField] GameObject UnlockSchild2;
    [SerializeField] GameObject UnlockSchild3;
    [SerializeField] GameObject Schild1LockUI;
    [SerializeField] GameObject Schild2LockUI;
    [SerializeField] GameObject Schild3LockUI;
    [SerializeField] ShipHealth shipHealth;
    [Header ("Audio")]
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            unlockMinigun();
            unlockSniper();
            unlockRaketenWerfer();
            SetShild1();
            SetShild2();
            SetShild3();
        }
    }
    public void unlockMinigun(){
        Destroy(MinigunLock);
        UnlockMinigun.SetActive(true);
        MinigunLockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void unlockSniper(){
        Destroy(SniperLock);
        UnlockSniper.SetActive(true);
        SniperLockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void unlockRaketenWerfer(){
        Destroy(RaketenwerferLock);
        UnlockRaketenwerfer.SetActive(true);
        RaketenwerferLockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void SetShild1(){
        shipHealth.SetShield50();
        UnlockSchild1.SetActive(true);
        Schild1LockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void SetShild2(){
        shipHealth.SetShield75();
        UnlockSchild2.SetActive(true);
        Schild2LockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void SetShild3(){
        shipHealth.SetShield100();
        UnlockSchild3.SetActive(true);
        Schild3LockUI.SetActive(false);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
