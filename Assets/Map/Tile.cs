using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Sprite image;
	private int type;

	private readonly int NORMAL = 0;	//AIR
	private readonly int BLOCKED = 1;	//WALL

	// Use this for initialization
	void Start () {
		checkCollisionBox();
	}

	public void setImage(Sprite image2){image = image2;}
	public void setType(int newType){type = newType;}

	public Sprite getImage(){return image;}
	public int getType(){return type;}
	void checkCollisionBox()
	{
		if(type == BLOCKED)
		{
			BoxCollider2D collider = this.GetComponent<BoxCollider2D>();
			collider.offset = new Vector2(-16, 16);
		}
	}

}
