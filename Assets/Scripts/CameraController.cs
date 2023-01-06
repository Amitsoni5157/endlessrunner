using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - Target.position;       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y,_offset.z+Target.position.z);
        transform.position = Vector3.Lerp(transform.position,newPosition,10 * Time.deltaTime);
    }
}
