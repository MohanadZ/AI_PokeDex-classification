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
                    if (HelloRequester.whoIsThatPokemon.Count > 0)
                    {
                        StartCoroutine(DisplayText());
                    }
                }
            }
        }
    }

    IEnumerator DisplayText()
    {
        pokedexText.text = "Your PokeDex is " + HelloRequester.whoIsThatPokemon[1] + " sure that the encountered Pokemon is a " + HelloRequester.whoIsThatPokemon[0];
        yield return new WaitForSeconds(5);
        pokedexText.text = "";
    }
}
