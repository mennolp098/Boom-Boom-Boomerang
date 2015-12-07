using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class LevelSelect : MonoBehaviour 
{
	[SerializeField]private Button _levelButton1;
	[SerializeField]private Button _levelButton2;
	[SerializeField]private Button _levelButton3;
	[SerializeField]private Button _levelButton4;
	[SerializeField]private Button _levelButton5;

	private int _currentLevel;
	private int _unlockedLevel;

	private bool _unlocked;

	//private List<GameObject> levelButtons = new List<Button>();
	private List<Button> levelButtons = new List<Button>();

	void LevelAdd()
	{
		levelButtons.Add (_levelButton1);
		levelButtons.Add (_levelButton2);
		levelButtons.Add (_levelButton3);
		levelButtons.Add (_levelButton4);
		levelButtons.Add (_levelButton5);
	}

	void Start()
	{
		/*_levelButton1.interactable = false;
		_levelButton2.interactable = false;
		_levelButton3.interactable = false;
		_levelButton4.interactable = false;
		_levelButton5.interactable = false;*/

		//_levelButton1.GetComponent<Button>

		_levelButton1 = GetComponent<Button> ();


		//levelButtons.interactable == false;

		LevelAdd ();
	}



	void SelectLevel()
	{
		if (_currentLevel == _unlockedLevel) 
		{
			_unlockedLevel += 1;
		}

		//unlocking levels
		/*if (_currentLevel != 0) 
		{
			_levelButton1.interactable == true;
		}

		if(_currentLevel >= 2 && _levelButton3.interactable == false)
		{
			_levelButton2.interactable == true;
		}

		if(_currentLevel >= 3 && _levelButton4.interactable == false)
		{
			_levelButton3.interactable == true;
		}

		if(_currentLevel >= 4 && _levelButton5.interactable == false)
		{
			_levelButton4.interactable == true;
		}*/






	}
}
