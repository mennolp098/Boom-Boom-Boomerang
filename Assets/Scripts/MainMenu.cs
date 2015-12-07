using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour 
{
	private GameObject _saveSelectPanel;
	private GameObject _mainMenuPanel;
	private GameObject _deleteWarningPanel;

	private bool _deleteBool;

	public Text _saveGameText1;
	public Text _saveGameText2;
	public Text _saveGameText3;

	private Dictionary<int, string> load = new Dictionary<int, string>();

	void Awake()
	{
		_saveSelectPanel = GameObject.Find ("SaveSelectPanel");
		_mainMenuPanel = GameObject.Find ("MainMenuPanel");
		_deleteWarningPanel = GameObject.Find ("DeleteWarningPanel");
	}

	void Start ()
	{
	

		//_saveSelectPanel.SetActive (false);
		_deleteWarningPanel.SetActive (false);
	}

	//loads the selected savegame
	public void SaveSelected()
	{
		//Put selected save here
		_saveSelectPanel.SetActive (false);
		_mainMenuPanel.SetActive (true);
	}
	//Delete warning popup before you delete your character
	public void DeleteSaveClicked()
	{
		_deleteWarningPanel.SetActive (true);

		if (_deleteBool == false) {
			return;
		} else if (_deleteBool == true) {
			//delete save here
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
