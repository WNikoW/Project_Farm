using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GegnerScript : MonoBehaviour
{
    [SerializeField] Image Healthbar;
    [SerializeField] Image Healthbar2;
    [SerializeField] float maxLeben;
    [SerializeField] int SchrottOnKill;
    public float currLeben;
    float platzhalter1;
    float platzhalter2;
    float a;
    float newTime;
    float ZeitzumGucken = 0.4f;
    WährungsScript währungsScript;
    void Start()
    {
        currLeben = maxLeben;
        platzhalter1 = maxLeben;
        währungsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WährungsScript>();
        SchrottOnKill = Random.Range(5,10);
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = currLeben / maxLeben;
        Healthbar2Script();
        OnDeath();
    }
    public void Schaden(float schaden){
        if(Time.time > newTime){
            platzhalter1 = currLeben;

        }
        currLeben -= schaden;

        platzhalter2 = currLeben;
        newTime = Time.time + ZeitzumGucken;
    }
    public void Healthbar2Script(){
        if(Time.time > newTime){
            platzhalter2 = Mathf.LerpUnclamped(platzhalter2,platzhalter1,.1f);
            a = platzhalter1 - platzhalter2;
            Healthbar2.fillAmount =(currLeben + a) / maxLeben;
        }
    }
    void OnDeath(){
        if(currLeben <=0){
            währungsScript.Schrott = währungsScript.Schrott + SchrottOnKill;
            Destroy(this.gameObject);
        }
    }
}
