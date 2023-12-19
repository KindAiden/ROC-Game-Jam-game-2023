using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kirby : Player
{
    void Start()
    {
        speed = 5;
        jumpHeight = 12;
        maxJumps = 6;
    }
}
