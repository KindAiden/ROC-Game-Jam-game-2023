using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Setplayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Transform players = GameObject.Find("players").transform;
        foreach (Transform child in players)
        {
            if (child.name.Equals(GameManager.character))
            {
                child.tag = "Player";
                continue;
            }
            else
                child.gameObject.SetActive(false);
        }
    }
}
