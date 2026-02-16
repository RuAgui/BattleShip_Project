using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player myPlayer;

    void Start()
    {
        //dando valor a la variable experience a traves de la propiedad Experience, set

        myPlayer.Experience = 5;

        myPlayer.Health = 100;

        // obteniendo el valor de la variable experience a traves de la propiedad Experience, get
        int x = myPlayer.Experience; 
    }
}
