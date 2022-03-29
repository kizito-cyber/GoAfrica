using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpwaner : MonoBehaviour
{

    public PieceType type;
    private Piece currentPiece;
    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.car:
                amtObj = LevelManager.Instance.cars.Count;
                break;
            case PieceType.log:
                amtObj = LevelManager.Instance.logs.Count;
                break;
            case PieceType.human:
                amtObj = LevelManager.Instance.humans.Count;
                break;



        }
        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0, amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }


    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
