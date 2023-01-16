using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicatorscript : MonoBehaviour
{
    [SerializeField] GameObject LinksOben;
    [SerializeField] GameObject RechtsOben;
    [SerializeField] GameObject LinkUnten;
    [SerializeField] GameObject RechtsUnten;
    [SerializeField] Material m;
    [SerializeField] float Speed;
    public bool tra;
    Vector3 PosObenLinks;Vector3 PosObenRechts;Vector3 PosUntenLinks;Vector3 PosUntenRechts;

    void Update()
    {
        LinksOben.GetComponent<Renderer>().material = m;
        RechtsOben.GetComponent<Renderer>().material = m;
        LinkUnten.GetComponent<Renderer>().material = m;
        RechtsUnten.GetComponent<Renderer>().material = m;

        LinksOben.transform.position = new Vector3(Mathf.Lerp(LinksOben.transform.position.x,PosObenLinks.x,Speed),4,
                                                    Mathf.Lerp(LinksOben.transform.position.z,PosObenLinks.z,Speed));
        LinkUnten.transform.position = new Vector3(Mathf.Lerp(LinkUnten.transform.position.x,PosUntenLinks.x,Speed),4,
                                                    Mathf.Lerp(LinkUnten.transform.position.z,PosUntenLinks.z,Speed));

        RechtsOben.transform.position = new Vector3(Mathf.Lerp(RechtsOben.transform.position.x,PosObenRechts.x,Speed),4,
                                                    Mathf.Lerp(RechtsOben.transform.position.z,PosObenRechts.z,Speed));
        RechtsUnten.transform.position = new Vector3(Mathf.Lerp(RechtsUnten.transform.position.x,PosUntenRechts.x,Speed),4,
                                                    Mathf.Lerp(RechtsUnten.transform.position.z,PosUntenRechts.z,Speed));
    }
    public void GetPos(GameObject StartObjekt, GameObject EndObjekt){
        float Oben;
        float Unten;
        float Rechts;
        float Links;
        if(StartObjekt.transform.position.z > EndObjekt.transform.position.z){
            Oben = StartObjekt.transform.position.z;
            Unten = EndObjekt.transform.position.z;
        }else{
            Oben = EndObjekt.transform.position.z;
            Unten = StartObjekt.transform.position.z;
        }
        if(StartObjekt.transform.position.x > EndObjekt.transform.position.x){
            Rechts = StartObjekt.transform.position.x;
            Links = EndObjekt.transform.position.x;
        }else{
            Rechts = EndObjekt.transform.position.x;
            Links = StartObjekt.transform.position.x;
        }

        Speed = .08f;

        PosObenLinks = new Vector3(Links,0,Oben);
        PosObenRechts = new Vector3(Rechts,0,Oben);
        PosUntenLinks = new Vector3(Links,0,Unten);
        PosUntenRechts = new Vector3(Rechts,0,Unten);
    }
    public void GetPos(GameObject StartObjekt){
        float Oben;
        float Unten;
        float Rechts;
        float Links;
        Oben = StartObjekt.transform.position.z;
        Unten = StartObjekt.transform.position.z;
        Rechts = StartObjekt.transform.position.x;
        Links = StartObjekt.transform.position.x;

        Speed = .08f;

        PosObenLinks = new Vector3(Links,0,Oben);
        PosObenRechts = new Vector3(Rechts,0,Oben);
        PosUntenLinks = new Vector3(Links,0,Unten);
        PosUntenRechts = new Vector3(Rechts,0,Unten);
    }
    public void GetPos(Vector3 pos){
        float Oben;
        float Unten;
        float Rechts;
        float Links;
        Oben = pos.z;
        Unten = pos.z;
        Rechts = pos.x;
        Links = pos.x;

        Speed = .2f;

        PosObenLinks = new Vector3(Links,0,Oben);
        PosObenRechts = new Vector3(Rechts,0,Oben);
        PosUntenLinks = new Vector3(Links,0,Unten);
        PosUntenRechts = new Vector3(Rechts,0,Unten);
    }
    public void SetTransparent(bool transparent){
        if(transparent){
            Color c = m.color;
            c.a = .3f;
            m.color = c;
        }else{
            Color c = m.color;
            c.a = 1;
            m.color = c;
        }
    }
}
