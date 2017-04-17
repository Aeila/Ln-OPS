using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcloseScript : MonoBehaviour
{
    GameObject player, yellow;
    public GameObject[] DiscourSundi;
    bool isFinPremierDiscour;
    int compteurNomrbreEcran;
    int compteurNombreDeClic;

    void Awake()
    {
    }
	// Use this for initialization
	void Start ()
    {
        compteurNombreDeClic = 0;
        compteurNomrbreEcran = DiscourSundi.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            foreach (GameObject message in DiscourSundi)
            {
                if (message.activeSelf)
                {
                    AfficherPremierDiscour();
                }
            }
        }
	}

    void OnEclosionFinished(string canStart)
    {
        IAJaune yellow = GameObject.Find("Yellow").GetComponent<IAJaune>();
        yellow.startNewMiniJeu = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<Animator>().enabled = false;
    }

    void AfficherPremierDiscour()
    {
        if (compteurNomrbreEcran > compteurNombreDeClic)
        {
            DiscourSundi[compteurNombreDeClic + 1].SetActive(true);
        }
        if (compteurNomrbreEcran>0)
        {
            DiscourSundi[compteurNombreDeClic].SetActive(false);
            compteurNombreDeClic++;
        }
    }
}
