using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageChanger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pokemonName;
    [SerializeField] Image pokemonImage;
    [SerializeField] TextMeshProUGUI pokemonNumber;
    [SerializeField] Sprite[] pokemonImages;

    public bool bulbasaur, charmander, squirtle, pikachu, mewtwo;
    private int pageCounter = 1;

    // Start is called before the first frame update
    void Start()
    {
        pokemonName.text = "Unknown";
        pokemonImage.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangePage();
    }

    private void ChangePage()
    {
        UpdatePokemon();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(pageCounter > 1)
            {
                pageCounter--;
                UpdatePokemon();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(pageCounter < 5)
            {
                pageCounter++;
                UpdatePokemon();
            }
        }
    }

    public void UpdatePokemon()
    {
        if(pageCounter == 1 && bulbasaur)
        {
            pokemonName.text = "Bulbasaur";
            pokemonImage.sprite = pokemonImages[1]; 
        }
        else if(pageCounter == 2 && charmander)
        {
            pokemonName.text = "Charmander";
            pokemonImage.sprite = pokemonImages[2];
        }
        else if (pageCounter == 3 && squirtle)
        {
            pokemonName.text = "Squirtle";
            pokemonImage.sprite = pokemonImages[3];
        }
        else if (pageCounter == 4 && pikachu)
        {
            pokemonName.text = "Pikachu";
            pokemonImage.sprite = pokemonImages[4];
        }
        else if (pageCounter == 5 && mewtwo)
        {
            pokemonName.text = "Mewtwo";
            pokemonImage.sprite = pokemonImages[5];
        }
        else
        {
            pokemonName.text = "Unknown";
            pokemonImage.sprite = pokemonImages[0];
        }

        if(pageCounter == 1)
        {
            pokemonNumber.text = "No. 1";
        }
        else if (pageCounter == 2)
        {
            pokemonNumber.text = "No. 4";
        }
        else if (pageCounter == 3)
        {
            pokemonNumber.text = "No. 7";
        }
        else if (pageCounter == 4)
        {
            pokemonNumber.text = "No. 25";
        }
        else if (pageCounter == 5)
        {
            pokemonNumber.text = "No. 150";
        }
    }
}
