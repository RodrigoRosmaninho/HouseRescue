using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class PlayerProgress
{
    public bool[] unlockedLevels;

    private PlayerProgress() {
        unlockedLevels = new bool[6];
        for (int i = 0; i < unlockedLevels.Length; i++) {
            unlockedLevels[i] = false;

        }

        unlockedLevels[0] = false;
        unlockedLevels[1] = true;

    }

    public void unlockLevel(int level){
        unlockedLevels[level+1] = true;
    }

    public void LogProgress(){
        foreach (var level in unlockedLevels) {
            Debug.Log(level);
        }
    }

    public void Save() {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream = new FileStream(Application.persistentDataPath + "/playerProgress.dat", FileMode.Create);

        bf.Serialize(stream, this);

        stream.Close();
    }

    public static PlayerProgress Load() {

        if (File.Exists(Application.persistentDataPath + "/playerProgress.dat")) {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/playerProgress.dat", FileMode.Open);

            PlayerProgress data = (PlayerProgress)bf.Deserialize(stream);


            stream.Close();

            return data;
        } else {
            PlayerProgress pp = new PlayerProgress();
            
            
            return pp;
        }
    }
}
