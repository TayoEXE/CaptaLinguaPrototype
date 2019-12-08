using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPosition : MonoBehaviour
{
    public Transform headset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(headset.transform.position.x, 0.3f, headset.transform.position.z);
        //transform.rotation = Quaternion.identity;
        transform.rotation = headset.rotation;
    }
}
