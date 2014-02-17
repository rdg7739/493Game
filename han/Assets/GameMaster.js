#pragma strict

static var currentScore : int = 0;
var test = 0;
var offsetY : float = 25;
var sizeX : float = 100;
var sizeY : float = 40;
function OnGUI () {
	
	GUI.Box(new Rect(Screen.width/2-sizeX/2, offsetY, sizeX, sizeY), "Score: " + currentScore);
	
}