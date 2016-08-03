using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityStandardAssets.CrossPlatformInput;
public class controls : MonoBehaviour
{
    Texture2D texture;
    int count;
    Sprite[] spriteSheet;
    List<Tile> tileList;
    public int opened;
    List<Tile> openedTiles;
    Quaternion defaultRotation;
    public Text scoreText;
    int rotationSpeed;
    int score;
    Preferences preferences;
    settings settings;
    public Transform preferenceObject;
    public Transform settingsObject;
    // Use this for initialization
    void Start()
    {
        preferences = preferenceObject.GetComponent<Preferences>();
        settings = settingsObject.GetComponent<settings>();


        Initiate();

        

    }

    void CreateList()
    {

        int id = 0;

        foreach (var item in spriteSheet)
        {
            GameObject tilePrefab = Resources.Load("tiles/" + settings.tilesNames[preferences.GetCurrentTiles()]) as GameObject;
            //tilePrefab.transform.localScale = new Vector3(spriteSheet[0].bounds.extents.x, item.bounds.extents.y, 0.1f);
            SpriteRenderer renderer = tilePrefab.GetComponentInChildren<SpriteRenderer>();
            renderer.sprite = null; 
            renderer.sprite = item;
            for (int i = 0; i < 2; i++)
            {
                GameObject newTile = Instantiate(tilePrefab);
                defaultRotation = newTile.transform.rotation;
                Tile tile = new Tile(newTile, id);
                tileList.Add(tile);
            }



            id++;


        }

        
    }
    public void RandomizeTiles()
    {

    }
   
    
    public void Initiate()
    {

        //QualitySettings.SetQualityLevel(1,false);
        if (tileList!=null)
        {
            foreach (var item in tileList)
            {
                Destroy(item.tile);
            }
        }
        tileList = new List<Tile>();
        openedTiles = new List<Tile>();

        string s = settings.spriteNames[preferences.GetCurrentFaces()];
        spriteSheet = Resources.LoadAll<Sprite>("faces/" + settings.spriteNames[preferences.GetCurrentFaces()]);

        count = spriteSheet.Length;
        //renderer.sprite = spriteSheet[1];
        CreateList();
        rotationSpeed = 10;
        score = 0;
        scoreText.text = "0";
        StopAllCoroutines();
        opened = 0;
        openedTiles = new List<Tile>();
        tileList.Sort((x, y) => Random.value < 0.5f ? -1 : 1);

        float xOffset = 0;
        float yOffset = 0;
        foreach (var item in tileList)
        {
            
            Destroy(item.tile.GetComponent<Rigidbody>());
            item.tile.transform.rotation = defaultRotation;
            item.tile.transform.position = new Vector3(xOffset, yOffset, 0);
            item.isRotating = false;
            xOffset += 1.2f;
            if (xOffset > 6)
            {
                yOffset += 1.2f;
                xOffset = 0;
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if ((Input.GetMouseButtonDown(0) || Input.touchCount != 0))
        {
            Ray vect = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.touchCount != 0)
            {
                vect = Camera.main.ScreenPointToRay(Input.touches[0].position);
            }
            if (Physics.Raycast(vect, out hit))
            {


                Tile selectedTile = tileList.Find(x => x.tile == hit.collider.gameObject);
                if (selectedTile == null)
                {
                    return;
                }
                if ((openedTiles.Count != 0 && !openedTiles.Exists(x => x.tile == selectedTile.tile)) || openedTiles.Count == 0 || (selectedTile.id == openedTiles[openedTiles.Count - 1].id && selectedTile != openedTiles[openedTiles.Count -1]))
                {
                    score++;
                    scoreText.text = score.ToString();
                    if (opened == 0)
                    {

                        StartCoroutine(RotateTile(selectedTile));

                    }


                    else
                    {
                        if (selectedTile.id == openedTiles[openedTiles.Count - 1].id)
                        {
                            StartCoroutine(EjectTiles(new Tile[] { openedTiles[openedTiles.Count - 1], selectedTile }));
                          
                        }
                        else
                        {
                            StartCoroutine(RotateTile(selectedTile));
                        }
                       






                    }
                }


            }

        }

    }

    class Tile
    {
        public GameObject tile;
        public int id;
        public bool isRotating;
        public Tile(GameObject tile, int id)
        {
            this.tile = tile;
            this.id = id;
            this.isRotating = false;
        }
    }
    IEnumerator RotateTile(Tile selectedTile)
    {
        openedTiles.Add(selectedTile);
        opened++;
        if (!selectedTile.isRotating)
        {
            selectedTile.isRotating = true;
            for (int i = 0; i < 180 / rotationSpeed; i++)
            {
                selectedTile.tile.transform.Rotate(0, rotationSpeed, 0);
                yield return null;
            }
            selectedTile.isRotating = false;
        }
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(ResetRotation(selectedTile));
      
    }
    IEnumerator PureRotation(Tile selectedTile)
    {
        openedTiles.Add(selectedTile);
        opened++;
        if (!selectedTile.isRotating)
        {
            selectedTile.isRotating = true;
            for (int i = 0; i < 180 / rotationSpeed; i++)
            {
                selectedTile.tile.transform.Rotate(0, rotationSpeed, 0);
                yield return null;
            }
            selectedTile.isRotating = false;
        }

    }
    IEnumerator ResetRotation(Tile selectedTile)
    {
        yield return new WaitForSeconds(0.3f);
        if (!selectedTile.isRotating)
        {
            selectedTile.isRotating = true;
            for (int i = 0; i < 180 / rotationSpeed; i++)
            {
                selectedTile.tile.transform.Rotate(0, rotationSpeed, 0);
                yield return null;
            }
            selectedTile.isRotating = false;
        }
        opened--;
        openedTiles.Remove(selectedTile);


    }
   
    IEnumerator EjectTiles(Tile[] tilesToEject)
    {
        opened -= 1;
        openedTiles.Remove(tilesToEject[0]);
        openedTiles.Remove(tilesToEject[1]);
        StartCoroutine(PureRotation(tilesToEject[1]));
        yield return new WaitForSeconds(0.2f);  
        for (int i = 0; i < 2; i++)
        {
            tilesToEject[i].tile.AddComponent<Rigidbody>();

            tilesToEject[i].tile.GetComponent<Rigidbody>().useGravity = false;
            tilesToEject[i].tile.GetComponent<Rigidbody>()
                .AddForceAtPosition((Camera.main.transform.position + new Vector3(0, Random.Range(-3, 3), Random.Range(-5, -10)) * 40),
                openedTiles[0].tile.transform.position + new Vector3(0.2f, -0.5f, 0.1f));

            tilesToEject[i].isRotating = true;
        }
        tileList.RemoveAll(x => x.id == tilesToEject[0].id);

    }
}
