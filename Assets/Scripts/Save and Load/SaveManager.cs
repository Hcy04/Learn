using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;

    private FileDataHandler dataHandler;
    private List<ISaveManager> saveManagers;
    [SerializeField] private GameData gameData;

    [ContextMenu("Delete save file")]

    private void DeleteSavedData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        dataHandler.Delete();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        saveManagers = FindAllSaveManager();

        LoadGame();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null) NewGame();
        foreach(ISaveManager saveManager in saveManagers) saveManager.LoadData(gameData);
    }

    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers) saveManager.SaveData(ref gameData);

        dataHandler.Save(gameData);
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManager()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }
}
