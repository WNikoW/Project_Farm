using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int Shield;
    public int shieldmax;
    public int ShieldReg;
    public float SchildProzent;
    public float LebenProzent;
    public int Health;
    public int maxHealth = 100;
    float newTime;
    void Start()
    {
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ShieldRegLogic();
        if(shieldmax > 0){SchildProzent = (Shield *100/shieldmax*100)/100;}
        LebenProzent = (Health *100/maxHealth*100)/100;
    }
    public void SetShield50(){
        Shield = 50;
        shieldmax = 50;
        ShieldReg = 0;
    }
    public void SetShield75(){
        Shield = 75;
        shieldmax = 75;
        ShieldReg = 1;
    }
    public void SetShield100(){
        Shield = 100;
        shieldmax = 100;
        ShieldReg = 5;
    }
    void ShieldRegLogic(){
        if(Time.time > newTime){
            newTime = Time.time + 5;
            if((Shield + ShieldReg) <= shieldmax){
                Shield += ShieldReg;
            }else if((Shield + ShieldReg) > shieldmax){
                Shield = shieldmax;
            }
        }
    }
    public void TakeDamage(int Schaden){
        if(Shield >= Schaden){
            Shield-= Schaden;
        }else if(Shield != 0 && Schaden > Shield){
            int merken = Schaden - Shield;
            Health -= merken;
        }else if(Shield == 0){
            Health-= Schaden;
        }
    }
}
