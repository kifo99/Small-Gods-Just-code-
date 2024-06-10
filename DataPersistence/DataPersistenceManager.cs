using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Inventory.Model;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField]
    private bool disableDataPersistence = false;
    [SerializeField]
    private bool initializeDataIfNull = false;
    [SerializeField]
    private bool overrideSelectedProfileId = false;
    [SerializeField]
    private string testSelectedProfileId = "test";
    [Header("File storage Config")]
    [SerializeField]
    private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError(
                "There is more than one DataPersistenceManager. Destroying the newest one."
            );
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        if(disableDataPersistence)
        {
            Debug.Log("Data Persistence is currently disabled!");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();

        if(overrideSelectedProfileId)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Overrode selected profile id with test id: " + testSelectedProfileId);
        }

        
    }

   

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
         PlayerPrefs.DeleteAll();
    }

    public void LoadGame()
    {
        if(disableDataPersistence)
        {
            return;
        }


        this.gameData = dataHandler.Load(selectedProfileId);


        if (this.gameData == null)
        {
            Debug.Log(
                "No data was found. A new game needs to be started befoure data can be loaded."
            );
            return;
        }

        foreach (IDataPersistence dataPersObj in dataPersistenceObjects)
        {
            dataPersObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        Debug.Log("Saved");
        if(disableDataPersistence)
        {
            return;
        }



        if (this.gameData == null)
        {
            Debug.LogWarning(
                "No data was found. A new game needs to be started before data can be saved."
            );
            return;
        }

        foreach (IDataPersistence dataPersObj in dataPersistenceObjects)
        {
            dataPersObj.SaveData(ref gameData);
        }

        gameData.lastUpdate = System.DateTime.Now.ToBinary();

        dataHandler.Save(gameData, selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistencesObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
     
}
