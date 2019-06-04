using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 horizontalMove = new Vector3(1, 0, 0);
    Vector3 verticalMove = new Vector3(0, 0, 1);

    [SerializeField] float pokemonDistance;
    [SerializeField] Camera camera;
    [SerializeField] HelloClient client;
    [SerializeField] TextMeshProUGUI pokedexText;
    [SerializeField] Image pokedex;

    RaycastHit hit;
    Ray pokemonRay;
    PageChanger pageChanger;

    string imagePath;
    bool trigger = false;

    // Start is called before the first frame update
    void Start()
    {
        client.GetComponent<HelloClient>();
        pageChanger = pokedex.GetComponent<PageChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        WhipOutPokedex();
        if (HelloRequester.whoIsThatPokemon.Count > 0 && trigger)
        {
            StartCoroutine(DisplayText());
        }
    }

    private void Move()
    {
        if (Input.GetButton("Vertical"))
        {
            gameObject.transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * 6 * Time.deltaTime);
        }
        if (Input.GetButton("Horizontal"))
        {
            gameObject.transform.Translate(Input.GetAxis("Horizontal") * Vector3.right * 6 * Time.deltaTime);
        }
    }

    private void WhipOutPokedex()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HelloRequester.whoIsThatPokemon.Clear();
            pokemonRay = new Ray(camera.transform.position, camera.transform.forward);

            if(Physics.Raycast(pokemonRay, out hit, pokemonDistance))
            {
                if(hit.collider.tag == "Pokemon")
                {
                    imagePath = hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial.name + ".png";
                    client.MessageToServer(imagePath);
                    pokedexText.text = "Processing...";
                    trigger = true;
                }
            }
        }
    }

    IEnumerator DisplayText()
    {
        pokedexText.text = "Your PokeDex is " + HelloRequester.whoIsThatPokemon[1] + " sure that the encountered Pokemon is a " + HelloRequester.whoIsThatPokemon[0];
        CheckPokemon();
        yield return new WaitForSeconds(5);
        pokedexText.text = "";
        trigger = false;
    }

    private void CheckPokemon()
    {
        if(HelloRequester.whoIsThatPokemon[0] == "bulbasaur")
        {
            pageChanger.bulbasaur = true;
        }
        else if (HelloRequester.whoIsThatPokemon[0] == "charmander")
        {
            pageChanger.charmander = true;
        }
        else if (HelloRequester.whoIsThatPokemon[0] == "squirtle")
        {
            pageChanger.squirtle = true;
        }
        else if (HelloRequester.whoIsThatPokemon[0] == "pikachu")
        {
            pageChanger.pikachu = true;
        }
        else if (HelloRequester.whoIsThatPokemon[0] == "mewtwo")
        {
            pageChanger.mewtwo = true;
        }
    }
}
