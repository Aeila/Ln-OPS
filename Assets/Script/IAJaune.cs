using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAJaune : MonoBehaviour
{
    public Vector3 CurrentPostion;
    PoulpousseScript joueur;
    bool isStartGame;

    // Use this for initialization
    void Start ()
    {
        CurrentPostion = this.transform.position;
        joueur = GameObject.Find("MainCharacter").GetComponent<PoulpousseScript>();
        isStartGame = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float dist = Vector3.Distance(this.transform.position, joueur.CurrentPosition);
        if (isStartGame&& dist >= 2)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, joueur.CurrentPosition, Time.deltaTime * 1.5f);
        }
        else if(isStartGame && dist <= 2)
        {
            isStartGame = false;
            if(!joueur.isFirstMiniJeu)
            {
                joueur.isMiniJeuStarted = true;
            }
        }


	}
}
