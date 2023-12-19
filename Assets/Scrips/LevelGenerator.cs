using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    public Transform camera;
    public string Tileset;
    public Vector2Int direction1;
    public Vector2Int direction2;
    public Vector2Int currentDirection;
    public Vector2Int position1;
    public Vector2Int position2;
    public Vector2Int currentPosition;
    public Tilemap tilemap;
    public TileBase currentTile;
    public Dictionary<TileType, TileBase> tileset = new Dictionary<TileType, TileBase>();
    public Dictionary<TileBase, TileType> tilesetReverse = new Dictionary<TileBase, TileType>();
    public int distance;

    [HideInInspector] public bool chunkMode; //should the generator create one tile per layer per time or make a section of the same tiles
    [HideInInspector] public int slopeLeftChance; //chance for an upward (left-facing) slope to appear
    [HideInInspector] public int slopeRightChance; //chance for an downward (right-facing) slope to appear
    [HideInInspector] public int cliffLeftChance; //chance for an upward (left-facing) cliff to appear
    [HideInInspector] public int cliffRightChance; //chance for an downward (right-facing) cliff to appear
    //[HideInInspector]  public int groundChance; 
    [HideInInspector] public Vector2Int groundRange; //minimum and maximum possible width of the ground (chunk mode only)
    [HideInInspector] public Vector2Int slopeLeftRange; //minimum and maximum possible size of the slope (chunk mode only)
    [HideInInspector] public Vector2Int slopeRightRange; //minimum and maximum possible size of the slope (chunk mode only)
    [HideInInspector] public Vector2Int cliffLeftRange; //minimum and maximum possible height of the cliff (chunk mode only)
    [HideInInspector] public Vector2Int cliffRightRange; //minimum and maximum possible height of the cliff (chunk mode only)

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
        camera = Camera.main.transform;
        currentPosition = (Vector2Int)tilemap.WorldToCell(new Vector3(currentPosition.x, currentPosition.y, 1));


        Tile[] tiles = Resources.LoadAll<Tile>($"tilesets/{Tileset}");
        for (int i = 0; i < tiles.Length; i++)
        {
            tileset.Add((TileType)i, tiles[i]);
        }

        // Reverse the key-value pairs
        foreach (KeyValuePair<TileType, TileBase> pair in tileset)
        {
            // Ensure the values are unique in the original dictionary
            if (!tilesetReverse.ContainsKey(pair.Value))
                tilesetReverse.Add(pair.Value, pair.Key);
            else
                Debug.Log($"Duplicate: {pair.Value}");
        }
    }

    void FixedUpdate()
    {
        if (camera.position.x + 50 - position1.x > 0)
        {
            currentPosition = position1;
            currentDirection = direction1;
            CreateTile();
            position1 = currentPosition;
        }
        if (camera.position.x - 50 - position2.x < 0)
        {
            currentPosition = position2;
            currentDirection = direction2;
            CreateTile();
            position2 = currentPosition;
        }
    }

    public void CreateTile()
    {
        //get the tile left of the curent position to get the tiles we can choose to place
        TileBase previousTile = tilemap.GetTile(new Vector3Int(currentPosition.x - direction1.x, currentPosition.y - direction1.y, 0));
        TileType? previousTileType;
        try { previousTileType = tilesetReverse[previousTile]; }
        catch { previousTileType = null; }
        //randomly place new tiles based on calculated chances for each tile
        int rand = Random.Range(0, 100);
        int slopeRightUpper = slopeLeftChance + slopeRightChance;
        int cliffLeftUpper = slopeRightUpper + cliffLeftChance;
        int cliffRightUpper = cliffLeftUpper + cliffRightChance;
        switch (previousTileType)
        {
            case TileType.LeftEdgeTop:
                if (rand < slopeLeftChance)
                    CreateSlopeUp();
                else if (slopeRightChance > 0 && rand < slopeRightUpper)
                    CreateSlopeDown();
                else if (cliffLeftChance > 0 && rand < cliffLeftUpper)
                    CreateCliffUp();
                else if (cliffRightChance > 0 && rand < cliffRightUpper)
                    CreateCliffDown();
                else
                    CreateGround();
                break;

            case TileType.Top:
                if (rand < slopeLeftChance)
                    CreateSlopeUp();
                else if (slopeRightChance > 0 && rand < slopeRightUpper)
                    CreateSlopeDown();
                else if (cliffLeftChance > 0 && rand < cliffLeftUpper)
                    CreateCliffUp();
                else if (cliffRightChance > 0 && rand < cliffRightUpper)
                    CreateCliffDown();
                else
                    CreateGround();
                break;

            case TileType.RightEdgeTop:
                Debug.Log(previousTile.name);
                break;

            case TileType.LeftEdge:
                Debug.Log(previousTile.name);
                break;

            case TileType.RightEdge:
                Debug.Log(previousTile.name);
                break;

            case TileType.Ground:
                Debug.Log(previousTile.name);
                break;

            case TileType.LeftEdgeConnector:
                Debug.Log(previousTile.name);
                break;

            case TileType.RightEdgeConnector:
                if (slopeRightChance > 0 && rand < slopeRightUpper)
                    CreateSlopeDown();
                else if (cliffRightChance > 0 && rand < cliffRightUpper)
                    CreateCliffDown();
                else
                    CreateGround();
                break;

            case TileType.LeftEdgeBottom:
                CreateCave();
                break;

            case TileType.Bottom:
                CreateCave();
                break;

            case TileType.RightEdgeBottom:
                Debug.Log(previousTile.name);
                break;

            case TileType.SlopeUpTop:
                if (rand < slopeLeftChance)
                    CreateSlopeUp();
                else if (cliffLeftChance > 0 && rand < cliffLeftUpper)
                    CreateCliffUp();
                else if (cliffRightChance > 0 && rand < cliffRightUpper)
                    CreateCliffDown();
                else
                    CreateGround();
                break;

            case TileType.SlopeUpBottom:
                Debug.Log(previousTile.name);
                break;

            case TileType.SlopeDownTop:
                Debug.Log(previousTile.name);
                break;

            case TileType.SlopeDownBottom:
                if (slopeRightChance > 0 && rand < slopeRightUpper)
                    CreateSlopeDown();
                else if (cliffRightChance > 0 && rand < cliffRightUpper)
                    CreateCliffDown();
                else
                    CreateGround();
                break;

            default:
                Debug.Log(previousTileType);
                CreateGround();
                break;
        }
        currentPosition += currentDirection;
    }

    public void CreateSlopeUp()
    {
        FillGround(new Vector2Int(0, 0));
        int rand = 0;
        if (chunkMode)
            rand = Random.Range(slopeLeftRange.x, slopeLeftRange.y);
        for (int i = 0; i <= rand; i++)
        {
            //place the bottom tile of the slope
            currentTile = tileset[TileType.SlopeUpBottom];
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);

            currentPosition.y += 1;
            //place the top tile of the slope
            currentTile = tileset[TileType.SlopeUpTop];
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
            if (i < rand)
                currentPosition += currentDirection;
        }
    }

    public void CreateSlopeDown()
    {
        int rand = 0;
        if (chunkMode)
            rand = Random.Range(slopeRightRange.x, slopeRightRange.y);
        for (int i = 0; i <= rand; i++)
        {
            //place the top tile of the slope
            currentTile = tileset[TileType.SlopeDownTop];
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);

            currentPosition.y -= 1;
            //place the bottom tile of the slope
            currentTile = tileset[TileType.SlopeDownBottom];
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
            if (i < rand)
                currentPosition += currentDirection;
        }
        FillGround(new Vector2Int(0, 0));
    }

    public void CreateCliffUp()
    {
        FillGround(new Vector2Int(0, 0));
        //create the bottom tile of an edge
        currentTile = tileset[TileType.LeftEdgeConnector];
        tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);

        int rand = 0;
        if (chunkMode)
            rand = Random.Range(cliffLeftRange.x, cliffLeftRange.y);
        for (int i = 0; i < rand; i++)
        {
            currentPosition.y += 1;
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
        }
        currentPosition.y += 1;
        //create the top tile of an edge
        currentTile = tileset[TileType.LeftEdgeTop];
        tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
    }

    public void CreateCliffDown()
    {
        //create the bottom tile of an edge
        currentTile = tileset[TileType.RightEdgeTop];
        tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);

        //create the edge
        int rand = Random.Range(cliffRightRange.x, cliffRightRange.y);
        currentTile = tileset[TileType.RightEdge];
        for (int i = 0; i < rand; i++)
        {
            currentPosition.y -= 1;
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
        }
        currentPosition.y -= 1;
        //create the top tile of an edge
        currentTile = tileset[TileType.RightEdgeConnector];
        tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
        FillGround(new Vector2Int(0, 0));
    }

    public void CreateGround()
    {
        int rand = 0;
        if (chunkMode)
            rand = Random.Range(groundRange.x, groundRange.y);
        for (int i = 0; i <= rand; i++)
        {
            //create the top tile
            currentTile = tileset[TileType.Top];
            tilemap.SetTile(new Vector3Int(currentPosition.x, currentPosition.y, 0), currentTile);
            if (i < rand)
                currentPosition += currentDirection;
        }
        FillGround(new Vector2Int(0, 0));
    }

    public void CreateCave()
    {
        CreateGround();
        Debug.Log("Cave");
    }

    public void FillGround(Vector2Int offset)
    {
        tilemap.BoxFill(new Vector3Int(currentPosition.x, currentPosition.y - 1, 0), tileset[TileType.Ground], currentPosition.x + offset.x, currentPosition.y - 100 + offset.y, currentPosition.x + offset.x, currentPosition.y - 1 + offset.y);
    }
}
