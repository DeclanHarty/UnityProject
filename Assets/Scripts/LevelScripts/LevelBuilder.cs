using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private LevelPieceCollection singleLeftPieces;
    [SerializeField] private LevelPieceCollection singleMiddlePieces;
    [SerializeField] private LevelPieceCollection singleRightPieces;
    [SerializeField] private LevelPieceCollection doublePieces;

    private float nextBuildPosition = 18f;
    [SerializeField] private float minPlayerDistanceToNextBuildPos;
    [SerializeField] private float distanceBetweenBuilds;

    private LevelPiece lastPiece;

    private LevelPiece[] activePieces;

    void Start(){
        activePieces = new LevelPiece[5];
    }

    void BuildNextPiece(){
        LevelPiece nextPiece = null;
        if(activePieces[0] == null){
            nextPiece = doublePieces.GetRandomPiece();
        }
        else{
            switch(activePieces[0].GetExit()){
                case Connector.SINGLE_LEFT:
                nextPiece = singleLeftPieces.GetRandomPiece();
                    break;
                case Connector.SINGLE_MIDDLE:
                nextPiece = singleMiddlePieces.GetRandomPiece();
                    break;
                case Connector.SINGLE_RIGHT:
                nextPiece = singleRightPieces.GetRandomPiece();
                    break;
                case Connector.DOUBLE:
                nextPiece = doublePieces.GetRandomPiece();
                    break;
            }
        }

        activePieces = SetActivePieces(nextPiece);
        activePieces[0] = Instantiate(nextPiece.gameObject, new Vector3(0, nextBuildPosition, 0), Quaternion.identity).GetComponent<LevelPiece>();
        foreach(LevelPiece piece in activePieces){
            Debug.Log(piece);
        }
        nextBuildPosition += distanceBetweenBuilds;
    }

    private LevelPiece[] SetActivePieces(LevelPiece newPiece){
        LevelPiece[] stillActivePieces = new LevelPiece[5];
        Array.Copy(activePieces, 0, stillActivePieces, 1, 4);

        if(activePieces[4] != null){
            Debug.Log("Destroy");
            Destroy(activePieces[4].gameObject);
        }
        return stillActivePieces;
    }

    public void HandleBuilding(float playerYPos){
        if (Mathf.Abs(nextBuildPosition - playerYPos) < minPlayerDistanceToNextBuildPos){
            BuildNextPiece();
        }
    }
}
