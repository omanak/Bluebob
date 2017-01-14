using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
	Controls the UI on the menu screen. A more rebust menu system is needed.
	This will do for now though.
*/
public class UIMenu : MonoBehaviour 
{
	public void callMe() 
	{
		SceneManager.LoadScene("TrueLevel1");
	}
}
