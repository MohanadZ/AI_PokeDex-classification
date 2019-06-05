using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokeDex : MonoBehaviour
{
    [SerializeField] GameObject image;

    private bool activatePokedex = false;

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
