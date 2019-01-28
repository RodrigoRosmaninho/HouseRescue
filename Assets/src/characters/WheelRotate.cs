using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{

    public bool rotate;

    public Transform rotationObject;

    public WallBouncingEnemy bouncing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (rotate) {
            rotationObject.Rotate(0, 0, -90 * Time.deltaTime * bouncing.sentidoInicial, Space.Self);
        }
    }
}
