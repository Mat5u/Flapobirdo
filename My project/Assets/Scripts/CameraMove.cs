using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float camMoveSpeed = 0; //Alustaa camMoveSpeed muuttujan arvoksi 0

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * camMoveSpeed; //M‰‰r‰‰ komponentin jossa t‰m‰ koodi on liikkumaan kokoajan oikealle.
    }
}
