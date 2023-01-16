using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaumschiffController : MonoBehaviour
{
    [SerializeField] Transform CheckPoint;
    [SerializeField] Transform CheckPoint2;
    [SerializeField] Transform CheckPoint3;
    Collider[] hitcollider;
    float radius = 1;
    [SerializeField] bool Activated;
    [SerializeField] bool Activated2;
    [SerializeField] bool Activated3;
    [SerializeField] GameObject WaffenShop;
    [SerializeField] GameObject Waffenauswahl;
    [SerializeField] GameObject SchildUI;
    [SerializeField] Movement movement;

    void Start()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        Activated = UpdateActivated();
        Activated2 = UpdateActivated2();
        Activated3 = UpdateActivated3();
        if(Activated || Activated2 || Activated3){
            movement.SchiffSteuern = true;
        }else{
            movement.SchiffSteuern = false;
        }
        if(Activated){
            WaffenShop.SetActive(true);
        }else{
            WaffenShop.SetActive(false);
        }
        if(Activated2){
            Waffenauswahl.SetActive(true);
        }else{
            Waffenauswahl.SetActive(false);
        }
        if(Activated3){
            SchildUI.SetActive(true);
        }else{
            SchildUI.SetActive(false);
        }
    }
    bool UpdateActivated(){
        hitcollider = Physics.OverlapSphere(CheckPoint.transform.position, radius);
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
    bool UpdateActivated2(){
        hitcollider = Physics.OverlapSphere(CheckPoint2.transform.position, radius);
        if(hitcollider.Length >0){
            for(int i = 0;i<hitcollider.Length;i++){
                if(hitcollider[i].transform.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !Activated2){
                    return true;
                }else if(hitcollider[i].transform.tag == "Player"  && Input.GetKeyDown(KeyCode.E) && Activated2){
                    return false;
                }else if(hitcollider[i].transform.tag == "Player"  && Activated2){
                    return true;
                }
            }
            return false;
        }
        return false;
    }
    bool UpdateActivated3(){
        hitcollider = Physics.OverlapSphere(CheckPoint3.transform.position, 4);
        if(hitcollider.Length >0){
            for(int i = 0;i<hitcollider.Length;i++){
                if(hitcollider[i].transform.tag == "Player" && Input.GetKeyDown(KeyCode.E) && !Activated3){
                    return true;
                }else if(hitcollider[i].transform.tag == "Player"  && Input.GetKeyDown(KeyCode.E) && Activated3){
                    return false;
                }else if(hitcollider[i].transform.tag == "Player"  && Activated3){
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(CheckPoint3.transform.position, 4);
    }
}
