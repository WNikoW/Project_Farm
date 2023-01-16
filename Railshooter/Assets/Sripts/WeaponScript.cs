using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class WeaponScript : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] Transform Lauf;
    [SerializeField] Transform Shootpoint;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform MainCamera;
    [SerializeField] Transform Player;
    [SerializeField] Movement movement;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Camera normalCamera;
    [SerializeField] public Camera WeaponCamera;

    [Header("Aktivieren")]
    [SerializeField] bool Activated;
    [SerializeField] float radius;
    Collider[] hitcollider;
    [Header("Schiessen")]
    [SerializeField] float cooldown;
    [SerializeField] GameObject crosshair;
    [SerializeField] float xRotCap;
    [SerializeField] float YRotCap;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject[] AllBulletprefabs;
    public GameObject BulletPrefab;
    float y;
    float x;
    RaycastHit hit;
    float newtime;
    float Laufx;
    float Laufy;
    bool done;
    void Start()
    {
        
    }

    void Update()
    {   
        cooldown = BulletPrefab.GetComponent<BulletMovement>().Shootcooldown;
        lookatLogic();
        Shoot();
    }

    void Shoot(){

        if(Input.GetKey(KeyCode.Mouse0) && Activated && Time.time >= newtime){
            newtime = Time.time + cooldown;
            audioSource.pitch = Random.Range(2f,2.1f);
            audioSource.PlayOneShot(audioSource.clip);
            Debug.DrawLine(Shootpoint.transform.position,hit.point,Color.red,2f);
            particleSystem.Play();
            Instantiate(BulletPrefab,bulletSpawnPoint.transform.position,Shootpoint.rotation);
            if(BulletPrefab == AllBulletprefabs[0]){
                Debug.Log("Mini");
                CameraShaker.Instance.ShakeOnce(2f,4f,.1f,.4f);
            }else if(BulletPrefab  == AllBulletprefabs[1]){
                Debug.Log("snip");
                CameraShaker.Instance.ShakeOnce(10f,4f,.1f,.2f);
            }else if(BulletPrefab  == AllBulletprefabs[2]){
                Debug.Log("Rak");
                CameraShaker.Instance.ShakeOnce(15f,5f,.1f,.6f);
            }
        }
    }
    
    void lookatLogic(){
        y = (Player.transform.rotation.y *100) *-1;
        x = (MainCamera.transform.rotation.x*100)*-1;
        hit = movement.hit;
        crosshair.SetActive(Activated);
        movement.AnWaffe = Activated;

        Activated = CheckForPlayer();

        if(Activated){
            done = false;
            movement.Waffe = this.transform;
            movement.RotXCap = xRotCap;
            movement.RotYCap = YRotCap;

            normalCamera.gameObject.SetActive(false);
            WeaponCamera.gameObject.SetActive(true);


            if(movement.lookat){
                Lauf.LookAt(hit.point);
                Shootpoint.LookAt(hit.point);
                Debug.DrawLine(Shootpoint.transform.position,hit.point,Color.green,.01f);
            }else{
                Lauf.transform.rotation = Quaternion.Euler(x,y,transform.rotation.z);
                Debug.DrawLine(Shootpoint.transform.position,hit.point,Color.green,.01f);
            }
            
        }else{
            movement.RotXCap = 180;
            movement.RotYCap = 80;
            movement.PushDown();
            if(!done){
                WeaponCamera.gameObject.SetActive(false);
                normalCamera.gameObject.SetActive(true);
                movement.playerCamera = normalCamera.transform;
                done = true;
            }
        }
    }
    bool CheckForPlayer(){
        hitcollider = Physics.OverlapSphere(this.transform.position, radius);
        if(hitcollider.Length >0){
            for(int i = 0;i<hitcollider.Length;i++){
                if(hitcollider[i].transform.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !Activated){
                    return true;
                }else if(hitcollider[i].transform.tag == "Player"  && Input.GetKeyDown(KeyCode.E) && Activated){
                    return false;
                }else if(hitcollider[i].transform.tag == "Player"  && Activated){
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    public void SetWaffe1(){
        BulletPrefab = AllBulletprefabs[0];
    }
    public void SetWaffe2(){
        BulletPrefab = AllBulletprefabs[1];
    }
    public void SetWaffe3(){
        BulletPrefab = AllBulletprefabs[2];
    }
}
