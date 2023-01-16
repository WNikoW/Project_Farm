using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    [SerializeField] Transform playerMesh;
    [SerializeField] float offset;
    public float check;
    public float Scroll;
    public PlayerTool playerTool;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward* MoveSpeed * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.right* MoveSpeed * Input.GetAxis("Horizontal"));
        if(Input.GetKey(KeyCode.Mouse2)){
            transform.Rotate(Vector3.up *Input.GetAxis("Mouse X"));

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }else{
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        Vector3 newpos = new Vector3(playerMesh.transform.position.x,playerMesh.transform.position.y,playerMesh.transform.position.z + offset);
        transform.position = newpos;

        if(playerTool.ToolSelection == false){
            Scroll = Input.GetAxis("Mouse ScrollWheel") * 10;
        if(Physics.Raycast(Camera.main.transform.position,-transform.up,2)){
            if(Input.GetAxis("Mouse ScrollWheel") < 0f){
                Camera.main.transform.Translate(Vector3.forward * Scroll);
            }
        }else{
            if(Input.GetAxis("Mouse ScrollWheel")!= 0f){
                Camera.main.transform.Translate(Vector3.forward * Scroll);
            }
        }
        }
    }
    
}
