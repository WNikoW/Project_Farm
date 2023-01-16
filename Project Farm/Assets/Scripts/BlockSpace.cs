using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpace : MonoBehaviour
{
    [SerializeField] Transform Parent;
    public GameObject currBlock;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBlock(GameObject block){
        currBlock = block;
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Instantiate(currBlock,
        new Vector3(Parent.transform.position.x,Parent.transform.position.y,Parent.transform.position.z),
        Quaternion.identity,Parent);

    }
    public GameObject GetBlock(){
        return currBlock;
    }
}
