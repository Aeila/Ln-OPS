using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class accueilscript : MonoBehaviour
{
    Animation animation = new Animation();
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AnimationTerminated(string isAnimationTerminated)
    {
        SceneManager.LoadScene(isAnimationTerminated);
    }
}
