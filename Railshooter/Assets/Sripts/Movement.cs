using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header ("Referenzen")]
    [SerializeField] public Transform playerCamera;
    [SerializeField] public Camera currCamera;
    [SerializeField] Transform groundCheck;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip JumpAudio;
    [SerializeField] AudioClip WalkAudio;
    CharacterController controller;
    [Header ("Camera / Smoothing")]
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime;
    [Header ("Physics")]
    [SerializeField] float gravity;
    [SerializeField] LayerMask layerMask;  
    [SerializeField] public LayerMask layerMask2;
    [SerializeField] LayerMask layerMaskDoor;
    [SerializeField] Collider[] hitcolliderDoorAuf;
    [SerializeField] GameObject[] AlleTüren;
    [SerializeField] float länge;
    [SerializeField] float radius;
    [Header ("PlayerStats")]
    public float normalSpeed; 
    float currSpeed;
    public float acc; 
    public float jumpHeight;
    [SerializeField] bool isGrounded;
    public float downForce;
    [Header("Camera Rotation")]
    public bool lookat;
    float velocityY;
    float cameraCap;
    float rotCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;
    public RaycastHit hit;
    public bool AnWaffe;
    public Transform Waffe = null;
    public float RotXCap;
    public float RotYCap;
    public bool done;
    public float offset;
    WeaponScript weaponScript;
    [SerializeField]Transform cam;
    public Vector3 oldPos;
    public bool SchiffSteuern;
    float newTime;
    [SerializeField] float cooldown;
    bool JumpSound;

 
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currSpeed = normalSpeed;
        AlleTüren = GameObject.FindGameObjectsWithTag("Door");
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
        currCamera = Camera.main;
        done = true;
    }
 
    void Update()
    {
        DoorHandler();
        Check();
        SoundBeimAufkommen();

        if(AnWaffe){
            AnWaffePos();
            UpdateMouse();
        }else{
            cam.transform.localPosition = new Vector3(0,0,-0.349f);
            PushDown();
            if(!SchiffSteuern){
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                UpdateMove();
                UpdateMouse();
                WalkAudioHandler();
            }else{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        InputChanges();

        if(Physics.Raycast(currCamera.ScreenPointToRay(Input.mousePosition),out hit,1000,layerMask2)){
            Debug.DrawLine(currCamera.transform.position,hit.point,Color.red,.1f);
            lookat = true;
        }else{
            lookat = false;
        }
    }
    void AnWaffePos(){
        if(Waffe != null){
            weaponScript = Waffe.GetComponent<WeaponScript>();
            cam.transform.position = weaponScript.transform.position;
            transform.position = new Vector3(Waffe.transform.position.x,Waffe.transform.position.y + offset,Waffe.transform.position.z);
            done = false;
        }
    }
    public void PushDown(){
        if(!done){
            Debug.Log("Down!");
            done = true;
        }
    }
    void InputChanges(){
        if(Input.GetKey(KeyCode.LeftShift)){
            if(currSpeed < normalSpeed*2){
                currSpeed += acc * Time.deltaTime;
            }
        }else{
            currSpeed = normalSpeed;
        }
    }
 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, radius);
    }
    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
 
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
 
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -RotYCap, RotYCap);
        playerCamera.localEulerAngles = Vector3.right * cameraCap;

        //CameraCap In X Richtung
        if(transform.eulerAngles.y <= 180f){
            rotCap = transform.eulerAngles.y;
        }else{
            rotCap = transform.eulerAngles.y - 360f;
        } 
        if(rotCap <= RotXCap && rotCap >= -RotXCap){
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }else{
            if(rotCap >= RotXCap && currentMouseDelta.x <0){
                transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
            }else if(rotCap <= -RotXCap && currentMouseDelta.x >0){
                transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
            }
        }
    }
    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radius, layerMask);
 
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
 
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
            audioSource.pitch = 1;
            audioSource.PlayOneShot(JumpAudio);
        }else if(isGrounded && velocityY < 0){
            velocityY = 0;
        }
        if(!isGrounded){
            velocityY += gravity * 2f * Time.deltaTime;
        }
        downForce = velocityY;
 
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currSpeed + Vector3.up * velocityY;
 
        controller.Move(velocity * Time.deltaTime);
        if( downForce < 0){
            JumpSound = false;
        }
    }
    void SoundBeimAufkommen(){
        if(!JumpSound && isGrounded){
            audioSource.pitch = 1;
            audioSource.PlayOneShot(JumpAudio);
            JumpSound = true;
        }
    }
    void WalkAudioHandler(){
        if((Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0) && Input.GetKey(KeyCode.LeftShift)){
            if(Time.time >= newTime){
                newTime = Time.time + Random.Range(cooldown,cooldown + 0.1f);
                audioSource.pitch = Random.Range(0.8f,.9f);
                audioSource.PlayOneShot(WalkAudio);
            }
        }else if((Input.GetAxis("Horizontal") != 0|| Input.GetAxis("Vertical") != 0) && !Input.GetKey(KeyCode.LeftShift)){
            if(Time.time >= newTime){
                newTime = Time.time + Random.Range((cooldown*2),(cooldown*2) + 0.1f);
                audioSource.pitch = Random.Range(0.8f,.9f);
                audioSource.PlayOneShot(WalkAudio);
            }
        }
    }
    void DoorHandler(){
        hitcolliderDoorAuf = Physics.OverlapSphere(transform.position, 5, layerMaskDoor);
        Check();
    }
    void Check(){
        foreach(GameObject g in AlleTüren){
            for(int i = 0 ; i < hitcolliderDoorAuf.Length;i++){
                if(g == hitcolliderDoorAuf[i].gameObject){
                    g.GetComponent<Animator>().SetBool("character_nearby",true); 
                    g.GetComponent<AudioSource>().Play();return;
                }
            }
            g.GetComponent<Animator>().SetBool("character_nearby",false); 
        }
    }
}
