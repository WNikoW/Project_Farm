using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] Transform ClickedPos;
    [SerializeField] Vector3 MoveToPos;
    [SerializeField] BlockController blockController;
    Camera cam;
    NavMeshAgent navMeshAgent;
    Ray ray;
    void Start()
    {
        cam = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !blockController.isActiveAndEnabled){
            ray = cam.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit rayhit)){
                navMeshAgent.destination = rayhit.point;
                ClickedPos.position = rayhit.point;
            }
        }
    }
}
