using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RemoveText()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
