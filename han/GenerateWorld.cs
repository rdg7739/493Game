using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;

public class GenerateWorld : MonoBehaviour {

	int sizeOfShip = 20;
	int miniBossFrequency = 5;
	int lastRoom = -1;

	// Use this for initialization
	void Start () {

		ArrayList rooms = LoadRooms();

		ArrayList alienShip = new ArrayList();

		//determine the layout of the ship
		for(int i = 0; i < sizeOfShip; i++){

			if(i % miniBossFrequency != miniBossFrequency - 1){

				int random = Random.Range(0, ((ArrayList)rooms[0]).Count );

				while( random == lastRoom){

					random = Random.Range(0, ((ArrayList)rooms[0]).Count );
				}

				lastRoom = random;

				alienShip.Add( ((ArrayList)rooms[0])[random] );
			}
			else{

				alienShip.Add( ((ArrayList)rooms[1])[Random.Range(0, ((ArrayList)rooms[1]).Count )] );
			}

		}

		alienShip[sizeOfShip - 1] = ((ArrayList)rooms[2])[Random.Range(0, ((ArrayList)rooms[2]).Count )];


		float X = 0;
		float Y = 0;

		foreach(Room room in alienShip){

			room.generate(ref X, ref Y);
		}


	}


	private ArrayList LoadRooms (){

		XmlDocument roomFile = new XmlDocument();
		roomFile.Load("Assets/Rooms.xml");


		if(roomFile == null){

			//TODO: Display File Not Found Error
			return null;
		}

		//roomFile.ChildNodes.size();

		ArrayList roomSets = new ArrayList();

		roomSets.Add (new ArrayList());	//Basic Room
		roomSets.Add (new ArrayList());	//Mini-Boss Room
		roomSets.Add (new ArrayList());	//Boss room

		foreach( XmlNode roomXML in roomFile.SelectNodes("Rooms//Room")){

			ArrayList rooms = null;

			switch(roomXML.SelectSingleNode("RoomType").InnerText){

			case "Mini-Boss":
				rooms = ((ArrayList)roomSets[1]);
				break;
			case "Boss":
				rooms = ((ArrayList)roomSets[2]);
				break;
			default:
				rooms = ((ArrayList)roomSets[0]);
				break;

			}

			//TODO: add error checking code
			Room room = new Room(float.Parse(roomXML.SelectSingleNode("XOffSet").InnerText), float.Parse (roomXML.SelectSingleNode("YOffSet").InnerText));
			
			foreach( XmlNode objXML in roomXML.SelectNodes("Object")){

				room.addObj( objXML.SelectSingleNode("Type").InnerText,
				            float.Parse( objXML.SelectSingleNode("PositionX").InnerText),
				            float.Parse( objXML.SelectSingleNode("PositionY").InnerText),
				            float.Parse( objXML.SelectSingleNode("RotationZ").InnerText),
				            float.Parse( objXML.SelectSingleNode("ScaleX").InnerText),
				            float.Parse( objXML.SelectSingleNode("ScaleY").InnerText) );
			}

			rooms.Add(room);

		}


		return roomSets;

	}


	public class Room{

		ArrayList objs;
		float Xoffset, Yoffset;

		public Room(float Xoffset, float Yoffset){

			this.Xoffset = Xoffset;
			this.Yoffset = Yoffset;
			objs = new ArrayList();

		}

		public Room(Room other){

			this.Xoffset = other.Xoffset;
			this.Yoffset = other.Yoffset;

			foreach( Obj obj in other.objs){

				this.objs.Add( new Obj( obj ) );
			}
		}

		public void generate(ref float X, ref float Y){

			foreach(Obj obj in objs){

				GameObject gameObj = null;

				if(obj.type.Equals("1")){

					gameObj = GameObject.CreatePrimitive(PrimitiveType.Cube);

				}
				//TODO: Add more types of game objects


				if( gameObj != null){

					gameObj.transform.position = new Vector3(obj.positionX + X, obj.positionY + Y, 0);
					gameObj.transform.Rotate( new Vector3(0, 0, obj.rotationZ)) ; //new Vector3(0,0, obj.rotationZ);
					gameObj.transform.localScale = new Vector3(obj.scaleX, obj.scaleY, 1);
				}


			}

			X += this.Xoffset;
			Y += this.Yoffset;

		}


		public void addObj(string type, float positionX, float positionY, float rotationZ, float scaleX, float scaleY){

			objs.Add(new Obj(type, positionX, positionY, rotationZ, scaleX, scaleY));
		}



		public class Obj{

			public string type;
			public float positionX, positionY, rotationZ, scaleX, scaleY;

			public Obj(string type, float positionX, float positionY, float rotationZ, float scaleX, float scaleY){

				this.type = type;
				this.positionX = positionX; 
				this.positionY = positionY;
				this.rotationZ = rotationZ;
				this.scaleX = scaleX;
				this.scaleY = scaleY;

			}

			public Obj( Obj other){

				this.type = string.Copy( other.type );
				this.positionX = other.positionX; 
				this.positionY = other.positionY;
				this.rotationZ = other.rotationZ;
				this.scaleX = other.scaleX;
				this.scaleY = other.scaleY;
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
