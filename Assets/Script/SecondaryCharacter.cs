using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCharacter : MonoBehaviour
{
    public Vector3 positionCharacter;
    public string profilCharacter;
    public Color colorCharacter;
    GameObject[] ListWaypoint;
    
    public List<GameObject> ListOfPossition = new List<GameObject>();
    bool debut = true;
    bool isMoving = false;
    bool isRetour = false;
    string currentWaypoint, nextWaypoint;

	// Use this for initialization
	void Start ()
    {
        positionCharacter = this.gameObject.transform.position;
        currentWaypoint = "WayPoint1";

        if(profilCharacter == "Blue")
        {
            ListOfPossition = new List<GameObject>();
            ListWaypoint = GameObject.FindGameObjectsWithTag("CheminBlue");
            for (int i = 0; i < ListWaypoint.Length; i++)
            {
                ListOfPossition.Add(ListWaypoint[i]);
            }
        }
        else if (profilCharacter == "Red")
        {
            ListWaypoint = GameObject.FindGameObjectsWithTag("CheminRed");
            ListOfPossition = new List<GameObject>();
            for (int i = 0; i < ListWaypoint.Length; i++)
            {
                ListOfPossition.Add(ListWaypoint[i]);
            }
        }
        else if(profilCharacter == "Yellow")
        {
            ListOfPossition = new List<GameObject>();
            ListWaypoint = GameObject.FindGameObjectsWithTag("CheminYellow");
            for (int i = 0; i < ListWaypoint.Length; i++)
            {
                ListOfPossition.Add(ListWaypoint[i]);
            }
        }

        ListOfPossition.Reverse();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(!isMoving)
        {
            if (profilCharacter != null)
            {
                if (GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().begin)
                {
                    nextWaypoint = GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().next.name;
                    this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find(nextWaypoint).transform.position, Time.deltaTime * 0.5f);
                    
                    isRetour = false;
                }
                else if (GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().end)
                {
                    nextWaypoint = GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().previous.name;
                    this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find(nextWaypoint).transform.position, Time.deltaTime * 0.5f);
                    isRetour = true;
                }
                else
                {
                    if (!isRetour)
                    {
                        nextWaypoint = GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().next.name;
                        this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find(nextWaypoint).transform.position, Time.deltaTime * 0.5f);
                    }
                    else
                    {
                        nextWaypoint = GameObject.Find(currentWaypoint).GetComponent<CapsuleWaypoint>().previous.name;
                        this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find(nextWaypoint).transform.position, Time.deltaTime * 0.5f);
                    }
                }
            }
            Debug.Log(nextWaypoint);

            isMoving = true;
        }
        else
        {
            if(Mathf.Round(this.transform.position.x) == GameObject.Find(nextWaypoint).transform.position.x)
            {
                isMoving = false;
                currentWaypoint = nextWaypoint;
            }
            else
            {
                this.transform.position = Vector3.Lerp(this.transform.position, GameObject.Find(nextWaypoint).transform.position, Time.deltaTime * 0.5f);
            }
        }
	}
}
