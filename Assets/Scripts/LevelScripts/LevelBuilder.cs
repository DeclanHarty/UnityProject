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

    void BuildNextPiece(){
        LevelPiece nextPiece = null;
        if(lastPiece == null){
            nextPiece = doublePieces.GetRandomPiece();
        }
        else{
            switch(lastPiece.GetExit()){
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

        lastPiece = Instantiate(nextPiece.gameObject, new Vector3(0, nextBuildPosition, 0), Quaternion.identity).GetComponent<LevelPiece>();
        nextBuildPosition += distanceBetweenBuilds;
    }

    public void HandleBuilding(float playerYPos){
        if (Mathf.Abs(nextBuildPosition - playerYPos) < minPlayerDistanceToNextBuildPos){
            BuildNextPiece();
        }
    }
}
