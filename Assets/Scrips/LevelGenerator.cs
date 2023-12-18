using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public CameraBehavior camera;
    public string Tileset;
    public Vector2Int position1;
    public Vector2Int position2;
    public Vector2Int currentPosition;
    public Tilemap layer1;
    public Tilemap layer2;
    public Tilemap currentLayer;
    public TileBase currentTile;
    public Dictionary<TileType, TileBase> tileset = new Dictionary<TileType, TileBase>();
    public Dictionary<TileBase, TileType> tilesetReverse = new Dictionary<TileBase, TileType>();
    public int distance;

    public enum TileType
    {
        LeftEdgeTop,
        Top,
        RightEdgeTop,
        LeftEdge,
        RightEdge,
        Ground,
        LeftEdgeConnector,
        RightEdgeConnector,
        LeftEdgeBottom,
        Bottom,
        RightEdgeBottom,
        SlopeUpTop,
        SlopeUpBottom,
        SlopeDownTop,
        SlopeDownBottom,
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
