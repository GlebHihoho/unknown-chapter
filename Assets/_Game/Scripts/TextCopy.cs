using System.IO;
using UnityEngine;

public class TextCopy : MonoBehaviour
{

    [SerializeField] TextAsset[] texts;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (TextAsset text in texts)
        {
            string filename = Application.persistentDataPath + Path.DirectorySeparatorChar + text.name + ".txt";

            if (!File.Exists(filename))
            {
                File.WriteAllText(filename, text.text);
            }
        }
    }

}
