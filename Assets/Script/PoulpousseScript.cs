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
    public bool isPlayingNewMiniJeu;

    //Premier Mini Jeu
    public bool isFirstSelection, isSameSelection;
    GameObject objectSelectionne;
    string previousObject;
    //Second Mini Jeu
    bool question1, question2, question3, question4;
    List<string> GoodReponse = new List<string>();

    GameObject Bebe, Enfant, Adolescent, Adulte, XtraAdulte;
    Color32 lastColor;

    public string newEvolution;
    public string newEmotion;

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

        for(int i = 0; i< 4; i++)
        {
            int randReponse = Random.Range(1, 4);
            switch(randReponse)
            {
                case 1:
                    GoodReponse.Add("Rosa");
                    break;
                case 2:
                    GoodReponse.Add("Bianca");
                    break;
                case 3:
                    GoodReponse.Add("RosaEtBianca");
                    break;
                case 4:
                    GoodReponse.Add("Aucun");
                    break;
            }                    
        }
    }

    // Use this for initialization
    void Start()
    {
        Bebe = GameObject.Find(MainPerso.Bebe.ToString());
        Adolescent = GameObject.Find(MainPerso.Adolescent.ToString());
        Enfant = GameObject.Find(MainPerso.Enfant.ToString());
        Adulte = GameObject.Find(MainPerso.Adulte.ToString());
        XtraAdulte = GameObject.Find(MainPerso.XtraAdulte.ToString());

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

        if(isPlayingNewMiniJeu)
        {
            #region 1er Mini Jeu
            if(isFirstMiniJeu && !isSecondMiniJeu && !isThirdMiniJeu)
            {
                if (isMiniJeuStarted && !isMiniJeuFinished)
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
                            if (hit.collider != null)
                            {
                                if (!isFirstSelection)
                                {
                                    isFirstSelection = true;
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (GameObject.Find("PremierMiniJeu").transform.GetChild(0).transform.GetChild(i).gameObject.name == hit.collider.tag)
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
                                    isFirstMiniJeu = false;
                                    isPlayingNewMiniJeu = false;
                                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
                                    GameObject.Find("CameraPremierMiniJeu").GetComponent<Camera>().enabled = false;


                                    if (hit.collider.tag == Emotion.Colere.ToString() + "Face")
                                    {
                                        lastColor = new Color32(232, 215, 130, 255);
                                    }
                                    else if (hit.collider.tag == Emotion.Bof.ToString() + "Face")
                                    {
                                        lastColor = new Color32(232, 215, 130, 255);
                                    }
                                    else if (hit.collider.tag == Emotion.Content.ToString() + "Face")
                                    {
                                        lastColor = new Color32(242, 216, 64, 255);
                                    }
                                    else if (hit.collider.tag == Emotion.Joie.ToString() + "Face")
                                    {
                                        lastColor = new Color32(255, 224, 3, 255);
                                    }

                                    newEmotion = hit.collider.tag;

                                    newEvolution = "Enfant";
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 2nd Mini Jeu
            else if(!isFirstMiniJeu && isSecondMiniJeu && !isThirdMiniJeu)
            {
                question1 = true;
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = false;
                    GameObject.Find("CameraSecondMiniJeu").GetComponent<Camera>().enabled = true;
                }
                if (question1)
                {
                    GameObject.Find("Discours").GetComponent<TextMesh>().text = "Je développe des jeux";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("CameraSecondMiniJeu").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Vector3 point = ray.origin + (ray.direction);
                        Vector2 touchPoint = new Vector2(point.x, point.y);
                        RaycastHit2D hit = Physics2D.Raycast(touchPoint, ray.direction);
                        if (hit.collider != null)
                        {
                            if (hit.collider.name == GoodReponse[1])
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }

                            question1 = false;
                            question2 = true;
                        }
                    }
                }
                else if(question2)
                {
                    GameObject.Find("Discours").GetComponent<TextMesh>().text = "Je suis un vrai cordon bleu";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("CameraSecondMiniJeu").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Vector3 point = ray.origin + (ray.direction);
                        Vector2 touchPoint = new Vector2(point.x, point.y);
                        RaycastHit2D hit = Physics2D.Raycast(touchPoint, ray.direction);
                        if (hit.collider != null)
                        {
                            if (hit.collider.name == GoodReponse[1])
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            question2 = false;
                            question3 = true;
                        }
                    }
                }
                else if(question3)
                {
                    GameObject.Find("Discours").GetComponent<TextMesh>().text = "J'ai fait le marathon de Little Big Seaty";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("CameraSecondMiniJeu").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Vector3 point = ray.origin + (ray.direction);
                        Vector2 touchPoint = new Vector2(point.x, point.y);
                        RaycastHit2D hit = Physics2D.Raycast(touchPoint, ray.direction);
                        if (hit.collider != null)
                        {
                            if (hit.collider.name == GoodReponse[1])
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            question3 = false;
                            question4 = true;
                        }
                    }
                }
                else if(question4)
                {
                    GameObject.Find("Discours").GetComponent<TextMesh>().text = "J'ai monté ma propre entreprise";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Ray ray = GameObject.Find("CameraSecondMiniJeu").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        Vector3 point = ray.origin + (ray.direction);
                        Vector2 touchPoint = new Vector2(point.x, point.y);
                        RaycastHit2D hit = Physics2D.Raycast(touchPoint, ray.direction);
                        if (hit.collider != null)
                        {
                            if (hit.collider.name == GoodReponse[1])
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            else
                            {
                                GameObject.Find("CadreVisageRosa").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                                GameObject.Find("CadreVisageRosa").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                                GameObject.Find("CadreVisageBianca").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                            }
                            question4 = false;
                        }
                    }
                }
            }
            #endregion

            else if(isFirstMiniJeu && isSecondMiniJeu && isThirdMiniJeu)
            {

            }
        }

        else if(!isPlayingNewMiniJeu)
        {
            if (newEvolution != EtapeEvolution)
            {
                ChangementEvolution(newEvolution, EtapeEvolution);
            }
            if (newEmotion != EmotionActuelle)
            {
                ChangementEmotion(EmotionActuelle, newEmotion);
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
                point = new Vector3(point.x, point.y, 0);
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
        if (nouvelleEvolution == "Bebe")
        {
            Bebe.SetActive(true);
        }
        else if (nouvelleEvolution == "Enfant")
        {
            Enfant.SetActive(true);
        }
        else if (nouvelleEvolution == "Adolescent")
        {
            Adolescent.SetActive(true);
        }
        else if (nouvelleEvolution == "Adulte")
        {
            Adulte.SetActive(true);
        }
        else if (nouvelleEvolution == "XtraAdulte")
        {
            XtraAdulte.SetActive(true);
        }

        if (oldEvolution == "Bebe")
        {
            Bebe.SetActive(false);
        }
        else if (oldEvolution == "Enfant")
        {
            Enfant.SetActive(false);
        }
        else if (oldEvolution == "Adolescent")
        {
            Adolescent.SetActive(false);
        }
        else if (oldEvolution == "Adulte")
        {
            Adulte.SetActive(false);
        }
        else if (oldEvolution == "XtraAdulte")
        {
            XtraAdulte.SetActive(false);
        }

        EtapeEvolution = nouvelleEvolution;

        for (int i = 0; i < 7; i++)
        {
            string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).name;
            if (elementToHide != (EmotionActuelle + "Face"))
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    void ChangementEmotion(string oldEmotion, string nouvelleEmotion)
    {
        for(int i = 0; i < GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.childCount;i++)
        {
            string elementToHide = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).name ;

            if (elementToHide == nouvelleEmotion)
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
                SpriteRenderer rendererToChange = GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
                Debug.Log(rendererToChange.color);
                rendererToChange.color = new Color32(lastColor.r, lastColor.g, lastColor.b,255);
                //rendererToChange.color = Color.red;
                Debug.Log(rendererToChange.color);
            }

            if(!oldEmotion.Contains("Face"))
            {
                oldEmotion = oldEmotion + "Face";
            }

            if (elementToHide == oldEmotion)
            {
                GameObject.Find(EtapeEvolution).transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        if(!nouvelleEmotion.Contains("Face"))
        {
            EmotionActuelle = nouvelleEmotion + "Face";
        }
        else
        {
            EmotionActuelle = nouvelleEmotion;
        }
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
