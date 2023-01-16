using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BlockController : MonoBehaviour
{
    [Header ("Blöcke")]
    [SerializeField] GameObject GrassBlock;
    [SerializeField] GameObject ErdeBlock;
    [SerializeField] GameObject ErdeBlockBearbeitet;
    [SerializeField] GameObject[] AlleBlöcke;
    [SerializeField] GameObject[] AusgewähleBlöcke;
    [SerializeField] GameObject ÄndernZu;
    [Header ("Spawn")]
    [SerializeField] GameObject BlockSpace;
    [SerializeField] Transform SpawnOrdner;
    [SerializeField] int SpawnAnzahl;
    [Header ("Visuell")]
    [SerializeField] GameObject VisuellIndicator;
    RaycastHit RayHit;
    Ray ray;
    GameObject ObjectHit;
    GameObject clickedObj = null;
    Vector3 newPosition;
    Vector3 Startposition;
    GameObject StartObjekt;
    Vector3 Endposition;
    GameObject EndObjekt;
    bool Auswählen = false;
    int anzahl;
    int zahl;
    bool check = false;
    int forxS;
    int forxE;
    int forzS;
    int forzE;
    [Header ("PlayerTool")]
    [SerializeField] PlayerTool playerTool;
    enum Tools{
        Nichts,
        Schaufel,
        Hacke,
        Saatkörner,
        Gießkanne,
        Korb
    }
    [SerializeField] Tools currtool;
    
    void Start()
    {
        Spawn();
    }

    void Update()
    {
        AlleBlöcke = GameObject.FindGameObjectsWithTag("BlockSpace");
        SetBlöcke();
        AuswahlInputs();

    }
    void SetTool(){
        currtool = GetTool();
        switch(currtool){
            case Tools.Schaufel: 
            ÄndernZu = ErdeBlock; 
            break;
            case Tools.Hacke: 
            ÄndernZu = ErdeBlockBearbeitet; 
            break;
            case Tools.Gießkanne: 
            ÄndernZu = GrassBlock; 
            break;
            case Tools.Saatkörner: 
            ÄndernZu = GrassBlock; 
            break;
            case Tools.Korb: 
            ÄndernZu = GrassBlock; 
            break;
        }
    }
    void Auswahl(){ 
        check = false;
        anzahl = 0;

        if(StartObjekt == EndObjekt )// Nur Ein Feld Ausgewählt
        {
            VisuellIndicator.GetComponent<Indicatorscript>().GetPos(StartObjekt);
            SetTool();
            ChangeBlock(StartObjekt,ÄndernZu);
        }
        else if(StartObjekt != EndObjekt)// Mehrere Felder Ausgewählt
        {
            VisuellIndicator.GetComponent<Indicatorscript>().GetPos(StartObjekt,EndObjekt);
            SetTool();
            GetSelected(ÄndernZu);
        }
        AusgewähleBlöcke = new GameObject[0];
    }
    void AuswahlInputs(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && !Auswählen){
            Auswählen = true;
            EndObjekt=null;
            GetStartblock();
        }

        //Visuelle Darstellung Start
        if(Input.GetKey(KeyCode.Mouse0) && Auswählen){
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RayHit);
            VisuellIndicator.GetComponent<Indicatorscript>().GetPos(StartObjekt,RayHit.transform.gameObject);
        }

        if(!Auswählen){
            VisuellIndicator.GetComponent<Indicatorscript>().SetTransparent(true);
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out RayHit);
            VisuellIndicator.GetComponent<Indicatorscript>().GetPos(new Vector3(RayHit.point.x,RayHit.point.y,RayHit.point.z));
            
        }else{
            VisuellIndicator.GetComponent<Indicatorscript>().SetTransparent(false);
        }
        //Visuelle Darstellung Ende

        if(Input.GetKeyUp(KeyCode.Mouse0) && Auswählen){
            Auswählen = false;
            GetEndBlock();
            check = true;
        }

        if(!Auswählen && StartObjekt != null && EndObjekt != null && check){
            Auswahl();
        }
    }
    void SetBlöcke(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            FillSpace();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            FillSpace2();
        }
    }

    void Spawn(){
        for(int x = 0; x < SpawnAnzahl; x++){
            for(int z = 0; z < SpawnAnzahl; z++){
                GameObject newObj = Instantiate(BlockSpace,new Vector3(x*4,0,z*4),Quaternion.identity,SpawnOrdner);
                newObj.name = "BlockSpace_ " + zahl.ToString();
                zahl++;
            }
        }
    }
    Tools GetTool(){
        switch (playerTool.currTool.name){
            case "Schaufel": return Tools.Schaufel;
            case "Hacke": return Tools.Hacke;
            case "Saatkörner": return Tools.Saatkörner;
            case "Gießkanne": return Tools.Gießkanne;
            case "Korb": return Tools.Korb;
        }
        return Tools.Nichts;
    }
    
    void FillSpace(){
        for (int i = 0; i < AlleBlöcke.Length;i++){
            AlleBlöcke[i].GetComponent<BlockSpace>().SetBlock(GrassBlock);
        }
    }
    void FillSpace2(){
        for (int i = 0; i < AlleBlöcke.Length;i++){
            AlleBlöcke[i].GetComponent<BlockSpace>().SetBlock(ErdeBlock);
        }
    }
    void GetStartblock(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);               
        if (Physics.Raycast(ray, out RayHit))
        {
            ObjectHit = RayHit.transform.gameObject;
            StartObjekt = ObjectHit;
            if(ObjectHit.tag == "Block"){
                StartObjekt = ObjectHit.transform.parent.gameObject;
                Startposition = ObjectHit.transform.parent.gameObject.transform.position;
            }
        }
    }
    void GetEndBlock(){
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);               
        if (Physics.Raycast(ray, out RayHit))
        {
            ObjectHit = RayHit.transform.gameObject;
            if(ObjectHit.tag == "Block"){
                EndObjekt = ObjectHit.transform.parent.gameObject;
                Endposition = ObjectHit.transform.parent.gameObject.transform.position;
            }
        }
    }
    void GetSelected(GameObject BlöckÄndernZu){
        if(Startposition.x > Endposition.x){
            forxS =Mathf.FloorToInt(Endposition.x);
            forxE = Mathf.FloorToInt(Startposition.x);
        }else{
            forxS =Mathf.FloorToInt(Startposition.x);
            forxE = Mathf.FloorToInt(Endposition.x);
        }
        if(Startposition.z > Endposition.z){
            forzS = Mathf.FloorToInt(Endposition.z);
            forzE = Mathf.FloorToInt(Startposition.z);
        }else{
            forzS =Mathf.FloorToInt(Startposition.z);
            forzE = Mathf.FloorToInt(Endposition.z);
        }
        for(int x = forxS;x <= forxE;x+=4){
            for(int z = forzS; z <=forzE;z+=4){
                for(int i = 0;i<AlleBlöcke.Length;i++){
                    if(AlleBlöcke[i].transform.position.x == x &&AlleBlöcke[i].transform.position.z == z){
                        GameObject[] übergang = new GameObject[anzahl+1];
                        for(int j = 0;j<AusgewähleBlöcke.Length;j++){
                            übergang[j] = AusgewähleBlöcke[j];
                        }
                        übergang[anzahl] = AlleBlöcke[i];
                        AusgewähleBlöcke = übergang;
                        anzahl++;
                    }
                }
            }
        }

        foreach(GameObject o in  AusgewähleBlöcke){
            ChangeBlock(o,BlöckÄndernZu);
        }

    }
    void ChangeBlock(GameObject ChangingObj,GameObject ChangeTo){
        switch(currtool){
            case Tools.Schaufel:
            if(ChangingObj.transform.GetChild(0).name == "GrassBlock Variant(Clone)"){
                ChangingObj.GetComponent<BlockSpace>().SetBlock(ChangeTo);
            }
            break;
            case Tools.Hacke:
            if(ChangingObj.transform.GetChild(0).name ==  "ErdeBlock Variant(Clone)"){
                ChangingObj.GetComponent<BlockSpace>().SetBlock(ChangeTo);
            }
            break;
        }
    }
}
