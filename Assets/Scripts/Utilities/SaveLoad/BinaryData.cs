        using System.Collections.Generic;
        using System.IO;
        using System.Runtime.Serialization.Formatters.Binary;
        using UnityEngine;
        using Utilities.Store;

        namespace Utilities.SaveLoad
        {
            public class BinaryData : MonoBehaviour
            {
                public static void Save(List<StoreInformations> data, string filename)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    FileStream file = File.Create(Application.persistentDataPath + "/" + filename + ".gd");
                    binaryFormatter.Serialize(file, data);
                    file.Close();
                }

                public static List<StoreInformations> Load(List<StoreInformations> data, string filename)
                {
                    if (File.Exists(Application.persistentDataPath + "/" + filename + ".gd"))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/" + filename + ".gd", FileMode.Open);
                        data = (List<StoreInformations>)binaryFormatter.Deserialize(file);
                        file.Close();
                        return data;
                    }

                    return null;
                }
            }
        }