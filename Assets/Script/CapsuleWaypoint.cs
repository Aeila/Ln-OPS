using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class CapsuleWaypoint : MonoBehaviour {
	public CapsuleWaypoint next,next1,next2, previous, previous1;
	public bool isBlocked;
	public bool isMultipleNext;
    public bool isMultiplePrevious;
    public bool begin, end;

    public bool isTobogan;
    public bool isSaut;
    public bool isAscenceur;

    public AudioSource son;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
