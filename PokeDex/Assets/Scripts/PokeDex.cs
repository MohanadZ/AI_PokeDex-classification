using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokeDex : MonoBehaviour
{
    [SerializeField] GameObject image;

    private bool activatePokedex = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DisplayPokedex();
    }

    private void DisplayPokedex()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            activatePokedex = !activatePokedex;
            image.SetActive(activatePokedex);
        }
    }
}
