using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Data;

public class FileDataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        if(profileId == null)
        {
            return  null;
        }
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData loadedData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                // Load the serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // Deserialite the data from Json to c#
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError(
                    "Error occured when trying to save data to file: " + fullPath + "\n" + e
                );
            }
        }

        return loadedData;
    }

    public void Save(GameData data, string profileId)
    {
        Debug.Log("Save is called");
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(
                "Error occured when trying to save data to file: " + fullPath + "\n" + e
            );
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName); 

            if (!File.Exists(fullPath))
            {
                Debug.LogError(
                    $"Skipping directory when loading all profiles becouse it doesnt contain data: {profileId}"
                );
                continue;
            }

            GameData profileData = Load(profileId);

            if (profileData != null)
            {
                profileDictionary.Add(profileId, profileData);
            }
            else
            {
                Debug.LogError(
                    $"Tried to load profile but something went wrong. ProfileId: {profileId}"
                );
            }
        }

        return profileDictionary;
    }

    public string GetMostRecentlyUpdatedProfileId()
    {
        string mostRecentProfileId = null;


        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();

        foreach(KeyValuePair<string,GameData> pair in profilesGameData)
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;


            if(gameData == null)
            {
                continue;
            }
            if(mostRecentProfileId == null)
            {
                mostRecentProfileId = profileId;
            }
            else
            {
                DateTime mostRecentDataTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdate);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdate);

                if(newDateTime > mostRecentDataTime)
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    public int DirectoryCount()
    {
        int count = 0;
        string fullPath = Path.Combine(dataDirPath);

        string[] directoryCount = Directory.GetDirectories(fullPath);
        foreach (string directory in directoryCount)
        {
            count++;
        }
        Debug.Log($"Count is: {count}");
        return count;
    }
    public List<string> DirectoryName()
    {
        List<string> directoryIDs = new List<string>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileId = dirInfo.Name;

            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName); //, profileId, dataFileName

            if (!File.Exists(fullPath))
            {
                Debug.LogError(
                    $"Skipping directory when loading all profiles becouse it doesnt contain data: {profileId}"
                );
                continue;
            }

            

            if (profileId != null)
            {
                directoryIDs.Add(profileId);
            }
            else
            {
                Debug.LogError(
                    $"Tried to load profile but something went wrong. ProfileId: {profileId}"
                );
            }
        }
        return directoryIDs;
    }

   
    // string name = "";
    // string fullPath = Path.Combine(dataDirPath);

    // string[] directoryNames= Directory.GetDirectories(fullPath);
    // foreach (string directory in directoryNames)
    // {
    //     Debug.Log($"Name is: {directory}");
    //     return name = directory;
    // }
    // return name;
}
