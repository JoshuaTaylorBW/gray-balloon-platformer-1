using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO; 
using System.Collections.Generic;

public class FlareMap : MonoBehaviour {

	public Transform Tile;
	private Queue<Transform> tileQueue;

	private double x;
	private double y;
	private double initX;
	private double initY;
	private int beenPlaced = 0;

	//map
	private int[,] map;
	private int tileSize = 64;
	private int numRows;
	private int numCols;
	private char[] openings;
	private String[] mapNames;
	private String mapName;
	public String mapFileName;

	//tileset
	public Sprite[] tileset;
	private int numTilesAcross;
	private Tile[,] tiles;

	public String fileName;

	private int rowOffset;
	private int colOffset;

	private int numRowsToDraw;
	private int numColsToDraw;
	
	private int whichDirection = 0;

	// Use this for initialization
	public void Start () {
		tileQueue = new Queue<Transform>(numRows * numCols);
	}

	public void makeMap(String mapFileName){
		loadTiles();
		findFolder();
		Load (findFolder() + mapFileName);
		//print();
		createMap();
	}

	#region loading stuff
	private void Load(string fileName)
		{

			// Handle any problems that might arise when reading the text
			try
			{


				string line;
				// Create a new StreamReader, tell it which file to read and what encoding the file
				// was saved as
				StreamReader theReader = new StreamReader(fileName, Encoding.Default);
				
				// Immediately clean up the reader after this block of code is done.
				// You generally use the "using" statement for potentially memory-intensive objects
				// instead of relying on garbage collection.
				// (Do not confuse this with the using directive for namespace at the
				// beginning of a class!)
				using (theReader)
				{
				//reads the [header] line and does nothing with it
				theReader.ReadLine();

				//get height and width of the map
				//columns / width
				line = theReader.ReadLine();
				String[] tokens = line.Split('=');
				numCols = int.Parse(tokens[1]);

				//rows / height
				line = theReader.ReadLine();
				tokens = line.Split ('=');
				numRows = int.Parse(tokens[1]);
			
				String tempLine = " ";
				while(!tempLine.Equals("data=")){
					tempLine = theReader.ReadLine();
				}

				map = new int[numRows, numCols];

				for(int row = 0; row < numRows; row++) {
					line = theReader.ReadLine();
					tokens = line.Split(',');
					for(int col = 0; col < numCols; col++) {
						map[row,col] = int.Parse(
							tokens[col]) - 1;
						
					}
				}

			

//					do
//					{
//						line = theReader.ReadLine();
//						if (line != null)
//						{
//							// Do whatever you need to do with the text line, it's a string now
//							// In this example, I split it into arguments based on comma
//							// deliniators, then send that array to DoStuff()
//
//							string[] entries = line.Split(',');
//							if (entries.Length > 0){
//								
//							}
//						}
//					}
//					while (line != null);
//					
					// Done reading, close the reader and return true to broadcast success
					theReader.Close();
				}
			}
			
			// If anything broke in the try block, we throw an exception with information
			// on what didn't work
			catch (Exception e)
			{
				Debug.Log("{0}\n" +  e.Message);
			}
		}

	void loadTiles(){
		numTilesAcross = 1024 / tileSize;
		tiles = new Tile[6, numTilesAcross];

		for(int x = 0; x < numTilesAcross; x++){
			tiles[0,x] = new Tile();
			tiles[0,x].setImage(tileset[x]);
			tiles[0,x].setType(0);

			tiles[1,x] = new Tile();
			tiles[1,x].setImage(tileset[x+16]);
			tiles[1,x].setType(0);

            tiles[2, x] = new Tile();
            tiles[2, x].setImage(tileset[x + 32]);
            tiles[2, x].setType(1);


            tiles[3, x] = new Tile();
            tiles[3, x].setImage(tileset[x + 48]);
            tiles[3, x].setType(1);


            tiles[4, x] = new Tile();
            tiles[4, x].setImage(tileset[x + 64]);
            tiles[4, x].setType(1);


            tiles[5, x] = new Tile();
            tiles[5, x].setImage(tileset[x + 80]);
            tiles[5, x].setType(1);
        }
	}

	void print(){
		for(int y = 0; y < numRows; y++){
			for(int x = 0; x < numCols; x++){
				Debug.Log(map[x,y]);
			}
		}
	}

	#endregion

	#region getters and setters
	public int getType(int row, int col){
		int rc = map[row, col];
		int r = rc / numTilesAcross;
		int c = rc % numTilesAcross;
		return tiles[r,c].getType();
	}
	public char[] getOpenings(){
		return openings;
	}
	public int getNumRows(){
		return numRows;
	}
	public void setMapName(String name){
		mapName = name;
	}
	public String getMapName(){
		return mapName;
	}
	public int[,] getMap(){
		return map;
	}
	public double getX(){
		return x;
	}
	public double getY(){
		return y;
	}
	public String getFileName(){
		return fileName;
	}
	public void setPosition(double x, double y) {
		
		//System.out.println(x);
		//System.out.println(this.x);
		//System.out.println((x - this.x) * tween);
		
		//		this.x += (x - this.x) * tween;
		//		this.y += (y - this.y) * tween;
		
		this.x = x;
		this.y = y;
		
		if(beenPlaced == 0){
			initX = this.x;
			//System.out.println("iX " + initX);
			initY = this.y;
			//System.out.println("iY " + initY);
			beenPlaced++;
		}
		
		//		fixBounds();
		//		
		colOffset = (int)-this.x / tileSize;
		rowOffset = (int)-this.y / tileSize;
		
	}
	public double getInitX() {
		return initX;
	}
	public void setInitX(double initX) {
		this.initX = initX;
	}
	public double getInitY() {
		return initY;
	}
	public void setInitY(double initY) {
		this.initY = initY;
	}
	public String findFolder(){
		String directory = Application.dataPath + "/";
		return directory;
	}
   
	#endregion

	void Update () {
	
	}

	void createMap(){
		for(int row = 0; row < numRows; row++){
			for(int col = 0; col < numCols; col++){

				int rc = map[row, col];

				int r = rc / numTilesAcross;
				int c = rc % numTilesAcross;



				if((float)(col * 64) == 0){
					tileQueue.Enqueue((Transform)Instantiate(
						Tile, new Vector3(transform.position.x + 0, transform.position.y + (float)(0 - (row * 32)), -4f), Quaternion.identity));
					Tile.GetComponent<SpriteRenderer>().sprite = tileset[rc];
					if(rc >= 32)
					{
						Tile.GetComponent<BoxCollider>().size = new Vector3(0.64f, 0.64f, 40f);
						
						Tile.GetComponent<BoxCollider>().center = new Vector2(0, 0);
						Tile.GetComponent<Tile>().setType(1);
					}else{
						Tile.GetComponent<BoxCollider>().size = new Vector2(0, 0);
						
						Tile.GetComponent<Tile>().setType(0);
					}
				}
				else
				{
				tileQueue.Enqueue((Transform)Instantiate(
						Tile, new Vector3(transform.position.x + (float)(col * 0.63), transform.position.y + (float)(0 - (row * 0.63)), -4f), Quaternion.identity));
						Tile.GetComponent<SpriteRenderer>().sprite = tileset[rc];
					if(rc >= 32)
					{
						Tile.GetComponent<BoxCollider>().size = new Vector3(0.64f, 0.64f, 40f);
						
						Tile.GetComponent<BoxCollider>().center = new Vector2(0, 0);
						Tile.GetComponent<Tile>().setType(1);
					}
					else
					{
						Tile.GetComponent<BoxCollider>().size = new Vector2(0.64f, 0.64f);
						
						Tile.GetComponent<Tile>().setType(0);
					}
				}
			}
        }
    }
}
