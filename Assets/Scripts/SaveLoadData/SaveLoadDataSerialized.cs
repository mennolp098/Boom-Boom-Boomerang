using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoadDataSerialized : MonoBehaviour {
    public delegate void NormalDelegate();
    public event NormalDelegate OnGameSaved;
    public event NormalDelegate OnGameLoaded;
    public event NormalDelegate OnNewGame;

    private GameController _gameController;
	public static SaveLoadDataSerialized Instance;

	void Awake()
	{
		if(Instance)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}
	public void DeleteSave(string path)
	{
		if(File.Exists(Application.persistentDataPath + path))
		{
			File.Delete(Application.persistentDataPath + path);
		}
	}

	public void Save (string path) 
	{
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + path);

        //creating a new instance of savedata that we can serialize later
		SaveData saveData = new SaveData();

		//get all components
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER);
        _gameController = gameController.GetComponent<GameController>();

        //getting all values that need to be saved
        saveData.lives = _gameController.lives;
        saveData.score = _gameController.score;
        saveData.checkpointPosition = _gameController.checkpointPosition;
        saveData.level = _gameController.level;
        saveData.goldCoins = _gameController.goldCoins;

        //serializing save data and closing the file
		binaryFormatter.Serialize(file,saveData);
		file.Close();

        if (OnGameSaved != null)
            OnGameSaved();
	}
	public void Load (string path) 
	{
		if(File.Exists(Application.persistentDataPath + path))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + path, FileMode.Open);

            //deserializing the file so we can get the values back
			SaveData saveData = binaryFormatter.Deserialize(file) as SaveData;

            //get all components
            GameObject gameController = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER);
            _gameController = gameController.GetComponent<GameController>();

            //setting all values
            _gameController.level = saveData.level;
            _gameController.lives = saveData.lives;
            _gameController.checkpointPosition = saveData.checkpointPosition;
            _gameController.score = saveData.score;
            _gameController.goldCoins = saveData.goldCoins;

            //closing the file after every value is set
			file.Close();
            if (OnGameLoaded != null)
                OnGameLoaded();
        }
        else
		{
            if (OnNewGame != null)
                OnNewGame();
			//new game
		}
		SavePaths.currentPath = path;
	}
}
[System.Serializable]
public class SaveData
{
	public int lives;
    public int score;
    public Dictionary<int, bool[]> goldCoins;
    public Vector3 checkpointPosition;
    public int level;
}
