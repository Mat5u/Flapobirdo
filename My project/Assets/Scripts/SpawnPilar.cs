using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPilar : MonoBehaviour
{
    public float x = 0f; //alustaa muuuttujan x arvoksi 0

    [SerializeField] private GameObject pilar; //Komponentti pilar


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("spawnPilarON", 1); //Asettaa spawnPilarON nimisen tietokannan arvoksi 1
        x = 15; //Asettaa muuttujan x arvoksi 15
    }

    // Update is called once per frame
    void Update()
    {
        spawnObject(); //Kutsuu funktiota spawnObject
    }

    public void spawnObject() //Funktio spawnObject
    {
        while (PlayerPrefs.GetInt("spawnPilarON") == 1) //Jos spawnPilarON nimisen tietokannan arvo on 1
        {
            var position = new Vector3(x, Random.Range(-2.6f, 3.6f), 0f); //Asettaa muuttujan position arvon
            Instantiate(pilar, position, Quaternion.identity); //Spawnaa objektin tiettyyn ennalta m‰‰riteltyyn paikkaan
            x += 5; //Lis‰‰ muuttujaan x 5
            PlayerPrefs.SetInt("spawnPilarON", 0); //Asettaa spawnPilarON nimisen tietokannan arvoksi 0
        }
        
    }
}
