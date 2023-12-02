using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public MoveBase Base { get; }
    public int PP { get; set; }

    public Move(MoveBase mBase)
    {
        Base = mBase;
        PP = mBase.PP;
    }
}
