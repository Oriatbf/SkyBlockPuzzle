using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Inst;

    public int endTurn,curTurn;

    private void Awake()
    {
        Inst = this;
    }
}
