        using System.Collections.Generic;
        using System.IO;
        using System.Runtime.Serialization.Formatters.Binary;
        using UnityEngine;
        using Utilities.Store;

        namespace Utilities.SaveLoad
        {
            public class BinaryData : MonoBehaviour
            {
                private const string _fileExtension = ".gd"; // Oluşacak dosyasının uzantısı
                
                /// <summary>
                /// Skinler için save dosyası oluşturup kayıt eder
                /// </summary>
                /// <param name="data"> Kaydedilecek liste</param>
                /// <param name="fileName"> Kaydedilecek dosyanın adı</param>
                public static void Save(List<StoreInformations> data, string fileName)
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    if (!File.Exists(Application.persistentDataPath + "/" + fileName + _fileExtension))
                    {
                        FileStream file = File.Create(Application.persistentDataPath + "/" + fileName + _fileExtension);
                        binaryFormatter.Serialize(file, data);
                        file.Close();
                    }
                    else
                    {
                        FileStream file = File.OpenWrite(Application.persistentDataPath + "/" + fileName + _fileExtension);
                        binaryFormatter.Serialize(file, data);
                        file.Close();
                    }
                }
                
                /// <summary>
                /// Kayıtlı dosyadan yükleme işlemi yapar.
                /// </summary>
                /// <param name="fileName">Yükleme yapılacak dosya adı</param>
                /// <returns>Dosya var ise içerisindeki bilgileri yok ise null döner</returns>
                public static List<StoreInformations> Load(string fileName)
                {
                    if (File.Exists(Application.persistentDataPath + "/" + fileName + _fileExtension))
                    {
                        BinaryFormatter binaryFormatter = new BinaryFormatter();
                        FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + _fileExtension, FileMode.Open);
                        List<StoreInformations> data = (List<StoreInformations>)binaryFormatter.Deserialize(file);
                        file.Close();
                        return data;
                    }
                    return null;
                }
                
                /// <summary>
                /// Dosyanın daha önceden oluşturulup oluşturulmadğını kontrol eder
                /// </summary>
                /// <param name="fileName">Kontrol edilecek dosya adı</param>
                /// <returns>Dosya var ise true yoksa false döner</returns>
                public static bool IsSaveDataExits(string fileName)
                {
                    return File.Exists(Application.persistentDataPath + "/" + fileName + _fileExtension);
                }
            }
        }