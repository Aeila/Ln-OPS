using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulpousseScript : MonoBehaviour
{
    public string EtapeEvolution;
    public string EmotionActuelle;
    public bool isProfil;
    public bool isDeDos;
    public bool isChangementOrientation;
    public bool isTournerGauche;
    public bool isTournerDroite;
    public bool isHold = false;
    public Vector3 CurrentPosition;

    public bool isFirstMiniJeu, isSecondMiniJeu, isThirdMiniJeu;
    public bool isMiniJeuStarted, isMiniJeuFinished;
    public bool isFirstSelection, isSameSelection;
    GameObject objectSelectionne;
    string previousObject;

    public string newEvolution;
    public string newEmotion;
    GameObject EmotionAffiche;
    GameObject EvolutionUtilise;
    Vector3 LasMousePosition;

    enum Emotion
    {
        Bof,
        Colere,
        Content, 
        Joie,
        Love,
        Surpris,
        Tristesse
    }

    enum MainPerso
    {
        Bebe,
        Enfant,
        Adolescent,
        Adulte,
        XtraAdulte
    }

    void Awake()
    {
        EtapeEvolution = "Bebe";
        EmotionActuelle = Emotion.Joie.ToString();

        newEmotion = Emotion.Joie.ToString();
        newEvolution = "Bebe";
        isDeDos = false;
        isProfil = false;

        for(int i = 0; i < 7;i++)
        {
            string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).name;
            if(elementToHide != (EmotionActuelle + "Face"))
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }

        for(int i =0;i<4;i++)
        {
            float randChild = Random.Range(1, 4);
            GameObject.Find("PremierMiniJeu").transform.GetChild(4).transform.GetChild(i).tag = GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild((int)randChild).name;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameObject.Find(MainPerso.Adolescent.ToString()).SetActive(false);
        GameObject.Find(MainPerso.Enfant.ToString()).SetActive(false);
        GameObject.Find(MainPerso.Adulte.ToString()).SetActive(false);
        GameObject.Find(MainPerso.XtraAdulte.ToString()).SetActive(false);
        CurrentPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPosition = this.transform.position;

        if(!isFirstMiniJeu)
        {
            if(isMiniJeuStarted && !isMiniJeuFinished)
            {
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }

                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
                    GameObject.Find("CameraPremierMiniJeu").GetComponent<Camera>().enabled = true;
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("CameraPremierMiniJeu").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Vector3 point = ray.origin + (ray.direction);
                        Vector2 touchPoint = new Vector2(point.x, point.y);
                        RaycastHit2D hit = Physics2D.Raycast(touchPoint, ray.direction);
                        if(hit.collider != null)
                        {
                            if (!isFirstSelection)
                            {
                                isFirstSelection = true;
                                for(int i=0;i<4;i++)
                                {
                                    if(GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.name == hit.collider.tag)
                                    {
                                        GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                        objectSelectionne = GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject;
                                    }
                                }
                                previousObject = objectSelectionne.name;
                            }
                            else if (objectSelectionne.name != hit.collider.tag)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.name == hit.collider.tag)
                                    {
                                        GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                        objectSelectionne = GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject;
                                    }
                                }
                                GameObject.Find(previousObject).GetComponent<SpriteRenderer>().enabled = false;
                                //objectSelectionne.GetComponent<SpriteRenderer>().enabled = true;
                                previousObject = objectSelectionne.name;
                            }
                            else
                            {
                                isMiniJeuFinished = true;

                            }
                        }
                    }
                }
            }
        }
        else if(isFirstMiniJeu)
        {
            if (newEvolution != EtapeEvolution)
            {
                ChangementEvolution(newEvolution, EtapeEvolution);
            }
            if (newEmotion != EmotionActuelle)
            {
                ChangementEmotion(newEmotion, EmotionActuelle);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = GameObject.Find(Camera.main.name).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.origin + (ray.direction);

                #region Verification orientation
                if (point.x > this.transform.position.x + 2f)
                {
                    if (point.y < this.transform.position.y + 1.5f)
                    {
                        isProfil = true;

                        isTournerDroite = true;
                        isTournerGauche = false;
                    }
                    else if (point.y > this.transform.position.y + 1.5f)
                    {
                        isDeDos = true;
                        isProfil = false;
                    }
                }
                else if (point.x < this.transform.position.x - 2f)
                {
                    if (point.y < this.transform.position.y + 1.5f)
                    {
                        isProfil = true;

                        isTournerGauche = true;
                        isTournerDroite = false;
                    }
                    else if (point.y > this.transform.position.y + 1.5f)
                    {
                        isDeDos = true;
                        isProfil = false;
                    }
                }


                if (point.y < this.transform.position.y - 1.5f)
                {
                    isProfil = false;
                    isDeDos = false;
                }

                ChangementOrientation();
                isHold = true;
                #endregion

            }

            if (Input.GetMouseButtonUp(0))
            {
                isHold = false;
            }

            if (isHold)
            {
                #region Verfication Orientation
                Ray ray = GameObject.Find(Camera.main.name).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Vector3 point = ray.origin + (ray.direction);
                int valueX = 0;
                int valueY = 0;
                if (point.x > this.transform.position.x + 2f)
                {
                    if (point.y < this.transform.position.y + 1.5f)
                    {
                        isProfil = true;

                        isTournerDroite = true;
                        isTournerGauche = false;
                        valueX = 1;
                        valueY = -1;
                    }
                    else if (point.y > this.transform.position.y + 1.5f)
                    {
                        isDeDos = true;
                        isProfil = false;
                        valueX = 2;
                        valueY = -2;
                    }
                }
                else if (point.x < this.transform.position.x - 2f)
                {
                    if (point.y < this.transform.position.y + 1.5f)
                    {
                        isProfil = true;

                        isTournerGauche = true;
                        isTournerDroite = false;
                        valueX = -1;
                        valueY = -1;
                    }
                    else if (point.y > this.transform.position.y + 1.5f)
                    {
                        isDeDos = true;
                        isProfil = false;
                        valueX = 2;
                        valueY = -2;
                    }
                }


                if (point.y < this.transform.position.y - 1.5f)
                {
                    isProfil = false;
                    isDeDos = false;
                    valueX = 2;
                    valueY = 2;
                }

                ChangementOrientation();
                MoveTo(point, valueX, valueY);

                #endregion
            }
        }
    }

    void ChangementEvolution(string nouvelleEvolution, string oldEvolution)
    {
        GameObject.Find(nouvelleEvolution).SetActive(true);
        GameObject.Find(oldEvolution).SetActive(false);

        EtapeEvolution = nouvelleEvolution;
    }

    void ChangementEmotion(string oldEmotion, string nouvelleEmotion)
    {
        for(int i = 0; i < GameObject.Find(EtapeEvolution).transform.childCount;i++)
        {
            string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(i).name + "Face";

            if (elementToHide == nouvelleEmotion)
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
            }
            if (elementToHide == oldEmotion)
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        EmotionActuelle = nouvelleEmotion;
    }

    void ChangementOrientation()
    {
        if(isProfil)
        {
            #region Affichage
            for (int i = 0; i < 7; i++)
            {
                string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).transform.GetChild(i).name;
                if (!GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.activeSelf)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
                }

                if (elementToHide != (EmotionActuelle + "Profil"))
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).transform.GetChild(i).gameObject.SetActive(true);
                }

                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            }
            #endregion
            #region Position Visage
            if(isTournerDroite)
            {
                float positionToCheck = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.x;
                if(positionToCheck <= 0)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition = new Vector3(4.07f, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.y, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.y);
                }
                float scaleToCheck = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.x;
                if (scaleToCheck <= 0)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale = new Vector3(-GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.x, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.y, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.y);
                }
            }
            else if(isTournerGauche)
            {
                float valueToCheck = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.x;
                if (valueToCheck >= 0)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition = new Vector3(-4.07f, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.y, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localPosition.y);
                }

                float scaleToCheck = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.x;
                if (scaleToCheck >= 0)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale = new Vector3(-GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.x, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.y, GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).localScale.y);
                }
            }
            #endregion
        }
        #region isDeDos
        else if(isDeDos)
        {

            for (int i = 0; i < 7; i++)
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);

                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        #endregion

        #region De Face
        else
        {
            for (int i = 0; i < 7; i++)
            {
                if (!GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).gameObject.activeSelf)
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                }

                string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).name;
                if (elementToHide != (EmotionActuelle + "Face"))
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
                }

                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        #endregion

        isChangementOrientation = false;
    }

    void MoveTo(Vector3 newPosition, int valueX,int valueY)
    {
        Vector3 positionXMoins1 = new Vector3(newPosition.x - valueX, newPosition.y - valueY, newPosition.z);
        this.transform.position = Vector3.Lerp(this.transform.position, positionXMoins1, Time.deltaTime * 2);
    }
}
