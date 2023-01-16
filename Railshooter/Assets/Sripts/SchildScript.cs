using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SchildScript : MonoBehaviour
{
    [Header ("Referenzen")]
    [SerializeField] Text MaxSchild;
    [SerializeField] Text SchildProzent;
    [SerializeField] Text currLeben;
    [SerializeField] Text FehlendesSchild;
    [SerializeField] Text Kosten;
    [SerializeField] ShipHealth shipHealth;
    [SerializeField] Image Leben;
    [SerializeField] Image Schild;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shipHealth.shieldmax > 0){
            MaxSchild.text = "max Schild: " +shipHealth.shieldmax.ToString();
            SchildProzent.text = "Schild: " + shipHealth.SchildProzent.ToString()+"%";

            FehlendesSchild.text = "Fehlendes Schild: "+(shipHealth.shieldmax - shipHealth.Shield).ToString();
            Kosten.text = "Kosten: "+(Mathf.Round((shipHealth.shieldmax - shipHealth.Shield) / 5)).ToString();
            float prozent = shipHealth.SchildProzent / 100;
            Schild.fillAmount = prozent;
        }else{
            Schild.fillAmount = 0;
        }

        currLeben.text = "Leben: " + shipHealth.Health.ToString();
        float proz = shipHealth.LebenProzent / 100;
        Leben.fillAmount = proz;
    }
}
