using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PieceType
{
    none = -1,
    car = 0,
    log = 1,
    human = 2,
}
public class Piece : MonoBehaviour
{
    public PieceType type;
    public int visualIndex;
    
}
