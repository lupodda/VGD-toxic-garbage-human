using System.Collections;
using System.Collections.Generic;
using UnityEngine;//


/**
 * Billboard serve per mostrare la barra di salute del nemico sempre verso la camera del giocatore
 * In questo modo la barra non è mai "specchiata" o distorta nella visuale
 */
public class Billboard : MonoBehaviour
{

    public Transform mainCamera;


    private void Start()
    {
        mainCamera = Camera.main.transform;
    }


    /**
     * LateUpdate viene chiamato dopo rispetto ad Update
     * in questo modo viene data "precedenza" al movimento della camera
     * la visualizzazione della barra si sposta correttamente sopra ai nemici
     */
    void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
