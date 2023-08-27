using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringboardRandomizer : MonoBehaviour
{
    [SerializeField] public Sprite[] Springboards;
    [SerializeField] public Sprite[] Railings;
    [SerializeField] public Sprite[] Supports;

    [SerializeField] private GameObject Springboard;
    [SerializeField] private GameObject Railing;
    [SerializeField] private GameObject Support;

    // Start is called before the first frame update
    void Start()
    {
        Springboard.GetComponent<SpriteRenderer>().sprite = Springboards[Random.Range(0,3)];
        Railing.GetComponent<SpriteRenderer>().sprite = Railings[Random.Range(0, 3)];
        Support.GetComponent<SpriteRenderer>().sprite = Supports[Random.Range(0, 3)];


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
