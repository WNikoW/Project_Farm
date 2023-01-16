using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetWeapons : MonoBehaviour
{
    public GameObject currWeapon;
    [SerializeField] GameObject[] AllWeapons;
    [SerializeField] int currIndex;
    [SerializeField] GameObject Minigun;
    [SerializeField] GameObject Sniper;
    [SerializeField] GameObject Raketenwerfer;
    [Header ("UI Referenzen")]
    [SerializeField] Text NameText;
    [SerializeField] Text SchadenText;
    [SerializeField] Text FeuerrateText;
    [SerializeField] GameObject VisuellerIdikator;
    [Header ("Waffe Refernzen")]
    [SerializeField] WeaponScript weaponScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SwitchVisuellerIndikatorPos();
        weaponScript.BulletPrefab = currWeapon;
    }
    void SetUItext(string nameText){
        NameText.text = nameText;
        SchadenText.text = "Schaden: " + currWeapon.GetComponent<BulletMovement>().Schaden.ToString();
        FeuerrateText.text = "Feuerrate: " + (Mathf.Round(10/(currWeapon.GetComponent<BulletMovement>().Shootcooldown * 10))).ToString() + "/s";
    }
    void SwitchVisuellerIndikatorPos(){
        switch(currIndex){
            case 1:
            VisuellerIdikator.transform.localPosition = new Vector3(VisuellerIdikator.transform.localPosition.x,Mathf.Lerp(VisuellerIdikator.transform.localPosition.y,200,0.1f),VisuellerIdikator.transform.localPosition.z);
            break;
            case 2:
            VisuellerIdikator.transform.localPosition = new Vector3(VisuellerIdikator.transform.localPosition.x,Mathf.Lerp(VisuellerIdikator.transform.localPosition.y,0,0.1f),VisuellerIdikator.transform.localPosition.z);
            break;
            case 3:
            VisuellerIdikator.transform.localPosition = new Vector3(VisuellerIdikator.transform.localPosition.x,Mathf.Lerp(VisuellerIdikator.transform.localPosition.y,-200,0.1f),VisuellerIdikator.transform.localPosition.z);
            break;
        }
    }
    public void SetMinigun(){
        currWeapon = Minigun;
        currIndex = 1;
        SetUItext("Minigun");
    }
    public void SetSniper(){
        currWeapon = Sniper;
        currIndex = 2;
        SetUItext("Sniper");
    }
    public void SetRaktenwerfer(){
        currWeapon = Raketenwerfer;
        currIndex = 3;
        SetUItext("Raketenwerfer");
    }
}
