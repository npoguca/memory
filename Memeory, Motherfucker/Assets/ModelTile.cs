using UnityEngine;
using System.Collections;

public class ModelTile : MonoBehaviour
{
    private Sprite[] spriteSheet;
    private GameObject tilePrefab;
    bool isRotating;
    private Animation tileAnimation;
    Coroutine rotationCoroutine;
    public void Create(string tile, string faces)
    {
        newTile(tile, faces, new Vector3(3.1f, -1f, -4.2f), Quaternion.identity);
    }
    public void Change(string tile, string faces)
    {
        newTile(tile, faces, tilePrefab.transform.position, tilePrefab.transform.rotation);
    }
    public void newTile(string tile, string faces, Vector3 position, Quaternion rotation)
    {
        if (tilePrefab != null)
        {
            Destroy(tilePrefab);
            StopCoroutine(rotationCoroutine);
        }
        isRotating = true;
        tilePrefab = Resources.Load("tiles/" + tile) as GameObject;
        Destroy(tilePrefab.GetComponent<BoxCollider>());
        //tilePrefab.transform.localScale = new Vector3(spriteSheet[0].bounds.extents.x, item.bounds.extents.y, 0.1f);
        SpriteRenderer renderer = tilePrefab.GetComponentInChildren<SpriteRenderer>();
        spriteSheet = Resources.LoadAll<Sprite>("faces/" + faces);

        renderer.sprite = null;
        renderer.sprite = spriteSheet[0];

        tilePrefab = Instantiate(tilePrefab, position, rotation) as GameObject;
        tileAnimation = tilePrefab.AddComponent<Animation>();
        AnimationClip clip = Resources.Load<AnimationClip>("tiles/modelSlideIn");

        AnimationClip[] clips = new AnimationClip[] { clip };
        //tileAnimation.ok
        //tileAnimation.AddClip(clips[0],"sure");
        //tileAnimation.Play("sure");

        rotationCoroutine = StartCoroutine(Rotate());

    }
    public void SlideIn()
    {
        StartCoroutine(SlideInCor());


    }
  
    public void SlideOut()
    {
        StartCoroutine(SlideOutCor());
    }
    IEnumerator SlideInCor()
    {
        while (tilePrefab.transform.position.y < 2)
        {
            tilePrefab.transform.Translate(0, +0.3f, 0);
            yield return null;
        }
    }
    IEnumerator SlideOutCor()
    {
        while (tilePrefab.transform.position.y > -1)
        {
            tilePrefab.transform.Translate(0, -0.3f, 0);
            yield return null;
        }
    }
    IEnumerator Rotate()
    {
        while (true)
        {
            tilePrefab.transform.Rotate(0, 1f, 0);
            yield return null;
        }
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
