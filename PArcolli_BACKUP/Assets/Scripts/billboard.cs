using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(Camera.main.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y+180, 0);
    }
}
