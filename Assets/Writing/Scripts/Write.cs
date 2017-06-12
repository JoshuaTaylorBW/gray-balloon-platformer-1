using UnityEngine;
using System.Collections;

public class Write : MonoBehaviour
{
	public GameObject LetterPrefab;
	public string sentence;
	public GameObject protagonist;
	public Camera camera;
	public float startPosition;
	public float endPosition;
	GameObject a;
	public float letterHeight = 4.5f;
	private float letterPosition;
	float letterAnimationSpeed = 0.4615f;
	private ArrayList sentenceLetters = new ArrayList();

	void Start()
	{
		CenterText();
		MakeSentence();
	}
		
	void Update(){
		if(cameraIsPastStartingPosition()){
			StartCoroutine(AnimateWords());
		}
	}

	IEnumerator AnimateWords()
	{
		while (true)
		{
			char[] letters = splitSentenceIntoArray();
			for (int i = 0; i < sentenceLetters.Count; i++)
			{
				GameObject l = (GameObject)sentenceLetters[i];
				l.SendMessage("enableAnimator");
				yield return new WaitForSeconds(WaitForXSeconds(letters[i]));
			}
		}
	}

	void CenterText()
	{
		char[] letters = splitSentenceIntoArray();
		float[] positions = new float[letters.Length];
		float lastPosition = 0;
		for (int i = 0; i < letters.Length; i++)
		{
			positions[i] = lastPosition;
			lastPosition += getSpaceToNextLetter(letters[i]);
		}

		letterPosition = -(lastPosition / 2) + camera.transform.position.x;

	}

	void MakeSentence()
	{
		char[] letters = splitSentenceIntoArray();
		for (int i = 0; i < letters.Length; i++)
		{
			sentenceLetters.Add(makeLetter(letters[i]));
		}
	}

	private char[] splitSentenceIntoArray()
	{
		return sentence.ToCharArray();
	}

	private float WaitForXSeconds(char c){
		char together = char.ToLower(c);

		if(char.IsWhiteSpace(c)){
			return 0f * letterAnimationSpeed;
		}else if(together.Equals('w')){
			return 1.3f * letterAnimationSpeed;
		}else if(c.Equals('g') || together.Equals('m')){
			return 1.5f * letterAnimationSpeed;
		}else if( c.Equals('y') ){
			return 1.7f * letterAnimationSpeed;
		}else if( c.Equals('e') || c.Equals('v')){
			return 0.8f * letterAnimationSpeed;
		}else if( c.Equals('l')){
			return 0.5f * letterAnimationSpeed;
		}else if( c.Equals('a') || c.Equals('D')){
			return 1.2f * letterAnimationSpeed;
		}else{
			return 1f * letterAnimationSpeed;
		}

	}

	private bool cameraIsPastStartingPosition(){
		return camera.transform.position.x > startPosition * letterAnimationSpeed;
	}

	GameObject makeLetter(char c)
	{
		moveLeft(c);
		a = Instantiate(Resources.Load("LetterPrefab"), new Vector3(letterPosition, letterHeight, -4f), Quaternion.identity) as GameObject;
		a.transform.parent = camera.transform;
		a.SendMessage("setEnd", endPosition * letterAnimationSpeed);
		a.SendMessage("setPlayer", protagonist);
		a.SendMessage("creator", c);
		letterPosition = getSpaceToNextLetter(c);
		return a;
	}

	void moveLeft(char c)
	{
		if (c.Equals('w') || c.Equals('u') || char.ToLower(c).Equals('m'))
		{
			letterPosition += .24f;
		}
	}

	private float getSpaceToNextLetter(char c)
	{
		if (char.IsWhiteSpace(c))
		{
			return letterPosition + 0.70f;
		}

		if (c.Equals('W'))
		{
			return letterPosition + 0.70f;
		}
		else if (c.Equals('L'))
		{
			return letterPosition + 0.32f;
		}
		else if (char.ToLower(c).Equals('u') || c.Equals('w'))
		{
			return letterPosition + 0.7f;
		}
		else if (c.Equals('n'))
		{
			return letterPosition + 0.48f;
		}
		else if (char.IsUpper(c))
		{
			return letterPosition + 0.56f;
		}
		else
		{
			return letterPosition + 0.40f;
		}
	}

}
