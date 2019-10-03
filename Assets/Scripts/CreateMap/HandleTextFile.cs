using UnityEngine;
using UnityEditor;
using System.IO;

public class HandleTextFile
{
    public static void WriteString(string json)
    {
        PlayerPrefs.SetString("map", json);
        Debug.Log(PlayerPrefs.GetString("map"));
        return;

        string path = "Assets/Resources/map.txt";

        try
        {
            // Check if file exists with its full path    
            if (File.Exists(path))
            {
                // If file found, delete it    
                File.Delete(path);
            }
        }
        catch (IOException ioExp)
        {
        }

        using (StreamWriter writer = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
        {
            //Write some text to the test.txt file
            writer.Write(json);
            writer.Close();

            //Re-import the file to update the reference in the editor
            AssetDatabase.ImportAsset(path);
            TextAsset asset = Resources.Load("map") as TextAsset;

            //Print the text from the file
            Debug.Log(asset.text);
        }
       
    }

    public static string ReadString()
    {
        return PlayerPrefs.GetString("map");

        string path = "Assets/Resources/map.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        return reader.ReadToEnd();
    }

}