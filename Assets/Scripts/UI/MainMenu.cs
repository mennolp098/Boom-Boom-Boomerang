using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour 
{
	private GameObject _saveSelectPanel;
	private GameObject _mainMenuPanel;
	private GameObject _deleteWarningPanel;

	private GameObject _deleteButtonY;
	private GameObject _deleteButtonN;

	private bool _deleteBool;

	public Text _saveGameText1;
	public Text _saveGameText2;
	public Text _saveGameText3;
	

	void Awake()
	{
		_saveSelectPanel = GameObject.Find ("SaveSelectPanel");
		_mainMenuPanel = GameObject.Find ("MainMenuPanel");
		_deleteWarningPanel = GameObject.Find ("DeleteWarningPanel");

		_deleteButtonY = GameObject.Find ("DeleteYes");
		_deleteButtonN = GameObject.Find ("DeleteNo");


	}

	void Start ()
	{
		_saveSelectPanel.SetActive (true);
		_mainMenuPanel.SetActive (false);
		_deleteWarningPanel.SetActive (false);
	}

	//loads the selected savegame
	public void SaveSelected()
	{
		//Put selected save here
		_saveSelectPanel.SetActive (true);
		_mainMenuPanel.SetActive (false);
	}
	//Delete warning popup before you delete your character
	public void DeleteSaveClicked()
	{
		Debug.Log ("Delete Clicked");
		_deleteWarningPanel.SetActive (true);

		if (_deleteBool == false && _deleteButtonN) 
		{
			_deleteWarningPanel.SetActive (false);
		} else if (_deleteBool == true && _deleteButtonY) {
			//delete save here
			Debug.Log("Save Deleted");
		}
		
	}


	//Loads level
    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }
	//Exits the game
	public void QuitGame()
	{
		Application.Quit ();
	}

}
