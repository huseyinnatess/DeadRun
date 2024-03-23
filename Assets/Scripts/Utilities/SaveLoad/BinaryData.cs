        using System.Collections.Generic;
        using System.IO;
        using System.Runtime.Serialization.Formatters.Binary;
        using UnityEngine;
        using Utilities.Store;

        namespace Utilities.SaveLoad
        {
            public class BinaryData : MonoBehaviour
            {
                private const string FileExtension = ".gd";

                public static void Save(List<StoreInformations> data, string fileName)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    if (!File.Exists(Application.persistentDataPath + "/" + fileName + FileExtension))
                    {
                        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + FileExtension);
                        binaryFormatter.Serialize(file, data);
                        file.Close();
                    }
                    else
                    {
                        FileStream file = File.OpenWrite(Application.persistentDataPath + "/" + fileName + FileExtension);
                        binaryFormatter.Serialize(file, data);
                        file.Close();
                    }
                }

                public static List<StoreInformations> Load(string fileName)
                {
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + FileExtension))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + FileExtension, FileMode.Open);
                        List<StoreInformations> data = (List<StoreInformations>)binaryFormatter.Deserialize(file);
                        file.Close();
                        return data;
                    }
                    return null;
                }

                public static bool IsSaveDataExits(string fileName)
                {
                    return File.Exists(Application.persistentDataPath + "/" + fileName + FileExtension);
                }
            }
        }