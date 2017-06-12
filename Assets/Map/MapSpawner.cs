using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO; 
using System.Collections.Generic;


public class MapSpawner : MonoBehaviour {

	public Transform MapPrefab;
	public Transform otherMaps;
	public Transform otherMaps1;
	public Transform otherMaps2;
	public Transform otherMaps3;
	public String mapFileName;

	private Transform[,] theMap;
	private Queue<Transform> MapQueue;

	private int PIECES_MADE = 0;

	public int NumberOfMaps;			//How many maps are a part of this dungeon

	private bool showParams = true;



	// Use this for initialization
	void Start () {



		MapQueue = new Queue<Transform>(NumberOfMaps);
		firstMap();

		print();

	}

	void firstMap()
	{
		theMap = new Transform[5,5];
		MapQueue.Enqueue((Transform)Instantiate(
			MapPrefab, new Vector3(2,2, -0f), Quaternion.identity)); 
		MapPrefab.GetComponent<Transform>().position = new Vector3(-9.32f, (float)14.788f, -4f);
		MapPrefab.GetComponent<FlareMap>().Start();
		MapPrefab.GetComponent<FlareMap>().makeMap(mapFileName);
	}

	public void print(){

			for(int i = 0; i < 5; i++)
			{
				for(int j = 0; j < 5; j++)
				{
					try{
					Debug.Log(i + "" + j + "" + theMap[i,j].GetComponent<FlareMap>().getFileName());
				}catch(Exception e){
					}
				}
			}
	}

	void mapBuilder(int x, int y)
		//m is the piece we're building off of, x and y are where the new
		//piece is located in theMap array.. 
		//Ex: top left piece is [0][0] and the one to the right of it is [1][0]
		
		/*we initially set these to the old pieces position 
		so that we don't always have to set both variables...*/
	{
		char north = 'm', east = 'n', south = 'v', west = 'z';

		Transform newMP;

		MapQueue.Enqueue((Transform)Instantiate(
			otherMaps, new Vector3(x,y-1, -0f), Quaternion.identity)); 
		otherMaps.GetComponent<Transform>().position = new Vector3((x-1)*640, (y * 480) - 32, -0f);
		otherMaps.GetComponent<FlareMap>().Start();
	}



	// Update is called once per frame
	void Update () {
	}
}
