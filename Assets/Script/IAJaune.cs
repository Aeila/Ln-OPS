using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAJaune : MonoBehaviour
{
    public Vector3 CurrentPostion;
    PoulpousseScript joueur;
    bool isStartGame, startNewMiniJeu;
    Vector3 positionAtStart;

    // Use this for initialization
    void Start ()
    {
        CurrentPostion = this.transform.position;
        positionAtStart = this.transform.position;
        joueur = GameObject.Find("MainCharacter").GetComponent<PoulpousseScript>();
        isStartGame = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!startNewMiniJeu)
        {
            float dist = Vector3.Distance(this.transform.position, joueur.CurrentPosition);
            if (isStartGame && dist >= 2)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, joueur.CurrentPosition, Time.deltaTime * 1.5f);
            }
            else if (isStartGame && dist <= 2)
            {
                isStartGame = false;
                if (!joueur.isFirstMiniJeu)
                {
                    joueur.isPlayingNewMiniJeu = true;
                    joueur.isMiniJeuStarted = true;
                    joueur.isFirstMiniJeu = true;
                }
            }
            else if (joueur.isMiniJeuFinished)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, positionAtStart, Time.deltaTime * 1.5f);
            }
        }
	}
}
