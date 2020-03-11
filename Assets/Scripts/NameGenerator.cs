using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class NameGenerator {
    private static Names names;

    static NameGenerator () {
        TextAsset jsonFile = Resources.Load<TextAsset>("first-names");
        names = JsonUtility.FromJson<Names>(jsonFile.text);
    }

    public static string GetRandomName () {
        return names[Random.Range(0, names.Length)];
    }

    [System.Serializable]
    private class Names {
        public string[] names;

        public string this[int key] {
            get => names[key];
        }
        public int Length {
            get => names.Length;
        }
    }
}
