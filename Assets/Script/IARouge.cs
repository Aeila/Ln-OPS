using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IARouge : MonoBehaviour
{
    public Vector3 CurrentPostion;
    PoulpousseScript joueur;
    bool isStartMiniJeu2, startNewMiniJeu;
    Vector3 positionAtStart;

    // Use this for initialization
    void Start ()
    {
        CurrentPostion = this.transform.position;
        positionAtStart = this.transform.position;
        joueur = GameObject.Find("MainCharacter").GetComponent<PoulpousseScript>();
        isStartMiniJeu2 = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float distWithPlayer = Vector3.Distance(CurrentPostion, joueur.CurrentPosition);
		if(!isStartMiniJeu2 && distWithPlayer < 6 && distWithPlayer > 10)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, joueur.CurrentPosition, Time.deltaTime * 1.5f);
        }
        else if (!isStartMiniJeu2 && distWithPlayer < 8)
        {
            isStartMiniJeu2 = true;
            if(!joueur.isSecondMiniJeu)
            {
                joueur.isPlayingNewMiniJeu = true;
                joueur.isSecondMiniJeu = true;
            }
        }
    }
}
