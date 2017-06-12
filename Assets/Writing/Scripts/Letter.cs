using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Letter : MonoBehaviour
{
    public RuntimeAnimatorController capitalA;
    Animator letterAnimator;
    char letter;
	GameObject protagonist;
    private Sprite sprite;
    private String s;
	private float endPosition;
	private Rigidbody rb;

	public void creator(char c)
    {
		rb = GetComponent<Rigidbody>();
        letterAnimator = GetComponent<Animator>();
        letterAnimator.runtimeAnimatorController = Resources.Load(spritePath(c)) as RuntimeAnimatorController;
		letterAnimator.speed = 2.6f;
        letterAnimator.enabled = false;
    }

	public void setPlayer(GameObject p){
		protagonist = p;
	}

	public void setEnd(float f){
		endPosition = f;
	}

    public string spritePath(char c)
    {
        return getSpriteFileName(c);
    }

    public string getSpriteFileName(char c)
    {
        return c + "-" + upperOrLower(c) + "Sheet_0";
    }

    public string upperOrLower(char c)
    {
        if (char.IsUpper(c))
        {
            return "uppercase-";
        }
        return "lowercase-";
    }

    // Update is called once per frame
    void Update()
    {
		if(protagonist.transform.position.x > endPosition){
			rb.AddForce(0, 0.1f, 0, ForceMode.Impulse);
		}
    }

    Animator getAnimator()
    {
        return letterAnimator;
    }

    public void enableAnimator()
    {
        letterAnimator.enabled = true;
    }

}
