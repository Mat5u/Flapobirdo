using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPilar : MonoBehaviour
{
    [SerializeField] GameObject pilar; //Pilar komponentti
    

    private void Update()
    {
        Destroy(pilar, 15f); //Tuhoaa pilari komponentin 15 sekunnin p‰‰st‰ kun se on spawnannut
    }


}
