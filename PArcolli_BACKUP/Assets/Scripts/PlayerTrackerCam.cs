using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackerCam : MonoBehaviour
{
    public Transform trackedObj;
    public float maxDistance = 10;
    public float moveSpeed = 20;
    public float updateSpeed = 10;

    [Range(0, 10)]
    public float currentDistance = 5;
    private string moveAxis = "Mouse ScrollWheel";
    private GameObject gameObj;
    private MeshRenderer _renderer;
    public float hideDistance = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        gameObj = new GameObject();
        _renderer = trackedObj.gameObject.GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        gameObj.transform.position = trackedObj.position + trackedObj.forward * (maxDistance * .25f);

        //distanza camera player gestibile tramite middle mouse button
        currentDistance += Input.GetAxis(moveAxis) * moveSpeed * Time.deltaTime;
        currentDistance = Mathf.Clamp(currentDistance, 0, maxDistance);

        //vettore start, vettore target, float
        transform.position = Vector3.MoveTowards(transform.position, trackedObj.position + Vector3.up * currentDistance - trackedObj.forward * (currentDistance + maxDistance * .5f), updateSpeed * Time.deltaTime);
        transform.LookAt(gameObj.transform);

        _renderer.enabled = (currentDistance > hideDistance);
    }
}
