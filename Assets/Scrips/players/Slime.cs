using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Player
{
    void Start()
    {
        speed = 3;
        jumpHeight = 5; 
        maxJumps = 1;
    }
}
