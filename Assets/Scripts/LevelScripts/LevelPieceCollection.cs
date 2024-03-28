using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

public class LevelPieceCollection : MonoBehaviour
{
    [SerializeField] private String folderPath;
    private List<LevelPiece> levelPieces;

    // Start is called before the first frame update
    void Start()
    {
        levelPieces = new List<LevelPiece>();

        String[] guids = AssetDatabase.FindAssets("t:object", new String[1] {folderPath});

        foreach (String guid in guids){
            String path = AssetDatabase.GUIDToAssetPath(guid);

            levelPieces.Add(AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object)).GetComponent<LevelPiece>());
        }

        if(levelPieces.Count() > 0) Debug.Log(levelPieces[0].GetExit());
    }

    public LevelPiece GetRandomPiece(){
        System.Random random = new System.Random();
        int randomIndex = random.Next(levelPieces.Count);

        return levelPieces.ElementAt(randomIndex);
    }
}
