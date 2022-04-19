using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjj : MonoBehaviour
{
   
    public GameObject dom1;
    public GameObject dom2;
    public Transform SpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SpawnDom1()
    {
        GameObject domsc1 = Instantiate(dom1, transform.position, transform.rotation);
        
        
    }

    public void SpawnDom2()
    {
        GameObject domsc2 = Instantiate(dom2, transform.position, transform.rotation);


    }


}
