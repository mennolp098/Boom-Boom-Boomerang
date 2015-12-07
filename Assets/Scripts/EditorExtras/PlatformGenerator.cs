using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlatformGenerator : ScriptableObject {
    private string _artPath = "Assets/Art/PlatformTiles/";

    private Sprite _topPiece;
    private Sprite _topLeftPiece;
    private Sprite _topRightPiece;
    private Sprite _middlePiece;
    private Sprite _middleLeftPiece;
    private Sprite _middleRightPiece;
    private Sprite _bottomPiece;
    private Sprite _bottomLeftPiece;
    private Sprite _bottomRightPiece;

    private float _newXStart = 0;
    private float _oldTileHeight = 0;
    private float _newYStart = 0;
    private float _oldY = 0;

    private float _firstY = 0;
    private float _firstX = 0;
    private float _lastX = 0;

    /// <summary>
    /// Function to generate new platform with a height and width
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="pos"></param>
	public void GeneratePlatform(int width, int height, Vector3 pos, int layer, string name)
    {
        //getting all the resources if they are not retrieved yet
        if(_topPiece == null || _topLeftPiece == null || _topRightPiece == null || _middlePiece == null || _middleLeftPiece == null || _middleRightPiece == null || _bottomLeftPiece == null || _bottomPiece == null || _bottomRightPiece == null)
        {
            _topPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile1.png", typeof(Sprite));
            Debug.Log(_topPiece);
            _topLeftPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile5.png", typeof(Sprite));
            _topRightPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile3.png", typeof(Sprite));
            _middlePiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile2.png", typeof(Sprite));
            _middleLeftPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile6.png", typeof(Sprite));
            _middleRightPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile4.png", typeof(Sprite));
            _bottomPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile9.png", typeof(Sprite));
            _bottomLeftPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile11.png", typeof(Sprite));
            _bottomRightPiece = (Sprite)AssetDatabase.LoadAssetAtPath(_artPath + "tile10.png", typeof(Sprite));
        }

        //reseting all the values
        _firstX = pos.x;
        _newXStart = pos.x;
        _oldTileHeight = 0;
        _oldY = pos.y;
        _newYStart = pos.y;
        //creating a platform object to use as parent for all the tiles
        GameObject newPlatform = new GameObject();
        newPlatform.transform.name = name;
        newPlatform.transform.tag = Tags.GROUND;
        for (int column = 0; column < height; column++)
        {
            for (int row = 0; row < width; row++)
            {
                //creating a new gameobject
                GameObject newTileObject = new GameObject();//Instantiate(tileObject, _oldTilePos, Quaternion.identity) as GameObject;
                //adding a spriterenderer to the object
                SpriteRenderer newTileSpriteRenderer = newTileObject.AddComponent<SpriteRenderer>();
                //changing the sortingorder so it will be in the background
                newTileSpriteRenderer.sortingOrder = layer;
                //setting the correct sprites
                if (column == 0)
                {
                    if(row == 0)
                    {
                        newTileSpriteRenderer.sprite = _topLeftPiece;
                        newTileObject.transform.name = "TileArtTopLeft";
                    }
                    else if(row != width-1)
                    {
                        newTileSpriteRenderer.sprite = _topPiece;
                        newTileObject.transform.name = "TileArtTop";
                    }
                    else
                    {
                        newTileSpriteRenderer.sprite = _topRightPiece;
                        newTileObject.transform.name = "TileArtTopRight";
                    }
                    
                }
                else if(column != height-1)
                {
                    if (row == 0)
                    {
                        newTileSpriteRenderer.sprite = _middleLeftPiece;
                        newTileObject.transform.name = "TileArtLeft";
                    }
                    else if (row != width-1)
                    {
                        newTileSpriteRenderer.sprite = _middlePiece;
                        newTileObject.transform.name = "TileArtMiddle";
                    }
                    else
                    {
                        newTileSpriteRenderer.sprite = _middleRightPiece;
                        newTileObject.transform.name = "TileArtRight";
                    }
                }
                else
                {
                    if (row == 0)
                    {
                        newTileSpriteRenderer.sprite = _bottomLeftPiece;
                        newTileObject.transform.name = "TileArtBotLeft";
                    }
                    else if (row != width - 1)
                    {
                        newTileSpriteRenderer.sprite = _bottomPiece;
                        newTileObject.transform.name = "TileArtBotMiddle";
                    }
                    else
                    {
                        newTileSpriteRenderer.sprite = _bottomRightPiece;
                        newTileObject.transform.name = "TileArtBotRight";
                    }
                }
                
                //calculating new position
                Vector3 newPos = new Vector3(0, 0, 0);
                newPos.x = _newXStart + newTileSpriteRenderer.sprite.bounds.size.x / 2;
                newPos.y = _newYStart - newTileSpriteRenderer.sprite.bounds.size.y / 2;

                if (row == 0 && column == 0)
                {
                    //working around the bad art work with the grass being different on every top tile
                    _firstY = newPos.y -= 0.025f;
                    newPos.y = _firstY;

                    //adding a boxcollider to the first row
                    //newTileObject.AddComponent<BoxCollider2D>();
                }
                else if (row > 0 && row < width-1 && column == 0)
                {
                    //working around the bad art work with the grass being different on every top tile
                    newPos.y = _firstY;

                    //adding a boxcollider to the first row
                    //newTileObject.AddComponent<BoxCollider2D>();
                } else if(row == width-1 && column == 0)
                {
                    _lastX = newPos.x;
                }
                //setting the new position of the tile
                newTileObject.transform.position = newPos;

                //some variables we are using for the next tile
                _newXStart = newPos.x + newTileSpriteRenderer.sprite.bounds.size.x / 2;
                _oldTileHeight = newTileSpriteRenderer.sprite.bounds.size.y;
                _oldY = newPos.y;

                //setting the tag to ground and adding it inside the platformparent.
                newTileObject.transform.tag = Tags.GROUND;
                newTileObject.transform.parent = newPlatform.transform;

                
            }
            //setting some variables we are using for the next row of tiles.
            _newXStart = pos.x;
            _newYStart = _oldY - _oldTileHeight / 2;
        }
        BoxCollider2D boxColl = newPlatform.AddComponent<BoxCollider2D>();
        boxColl.size = new Vector3((_lastX - _firstX + 0.5f), 1, 0);
        boxColl.offset = new Vector3((_lastX - _firstX + 0.5f) / 2, _firstY, 0);
    }
}
