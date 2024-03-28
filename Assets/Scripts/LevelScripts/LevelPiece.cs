using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Connector{
    NONE,
    SINGLE_LEFT,
    SINGLE_MIDDLE,
    SINGLE_RIGHT,
    DOUBLE
}
public class LevelPiece : MonoBehaviour
{
    [SerializeField] private Connector entrance;
    [SerializeField] private Connector exit;

    public Connector GetExit(){
        return exit;
    }
}
