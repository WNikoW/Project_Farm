using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTool : MonoBehaviour
{
    public GameObject[] Tools;
    public Sprite[] ToolSprite;
    public Image[] UIimage;
    public Vector3[] SpawnPosition;
    public GameObject currTool;
    public Image currImage;
    public float maxSize;
    public float changeSpeedScale;
    public float changeSpeedUPDOWN;
    public bool ToolSelection;
    void Start()
    {
        ToolSelection = false;
        for(int x = 0; x < Tools.Length;x++){
            SpawnPosition[x] = UIimage[x].transform.position;
        }
        currTool = Tools[2];
        currImage.sprite = Tools[2].GetComponent<ToolScript>().image.sprite;
    }

    void Update()
    {
        if(ToolSelection){
            ChangeTools();
            currTool = Tools[2];
            currImage.sprite = Tools[2].GetComponent<ToolScript>().image.sprite;
            UpdateUI();
        }
    }
    public void SetToolSelectionFalse(){
        ToolSelection = false;
    }
    public void SetToolSelectionTrue(){
        ToolSelection = true;
    }
    void ChangeTools(){
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            GameObject[] Übergang = new GameObject[5];
            Übergang[4] = Tools[0];

            for(int i = 1;i<Tools.Length;i++){
                Übergang[i-1] = Tools[i];
            }
            Tools = Übergang;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0){
            GameObject[] Übergang = new GameObject[5];
            Übergang[0] = Tools[4];

            for(int i = 0;i<Tools.Length-1;i++){
                Übergang[i+1] = Tools[i];
            }
            Tools = Übergang;
        }

    }
    void UpdateUI(){
        for(int y = 0; y < Tools.Length;y++){
            ToolSprite[y] = Tools[y].GetComponent<ToolScript>().toolInfo.ToolImage;
        }
        for(int k = 0; k < Tools.Length;k++){
            Vector3 newPos = new Vector3(
            Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.position.x,SpawnPosition[k].x,changeSpeedUPDOWN),
            Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.position.y,SpawnPosition[k].y,changeSpeedUPDOWN),
            Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.position.z,SpawnPosition[k].z,changeSpeedUPDOWN));
            Tools[k].GetComponent<ToolScript>().image.transform.position = newPos;
            if(k == 2){
                Vector3 newScale = new Vector3(
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.x,maxSize,changeSpeedScale),
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.y,maxSize,changeSpeedScale),
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.z,maxSize,changeSpeedScale));
                Tools[k].GetComponent<ToolScript>().image.transform.localScale = newScale;
            }else{
                Vector3 newScale = new Vector3(
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.x,1f,changeSpeedScale),
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.y,1f,changeSpeedScale),
                Mathf.Lerp(Tools[k].GetComponent<ToolScript>().image.transform.localScale.z,1f,changeSpeedScale));
                Tools[k].GetComponent<ToolScript>().image.transform.localScale = newScale;
            }
        }
    }
}
