using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

public class settings : MonoBehaviour
{
    public Transform modelObject;
    public Transform preferencesObject;
    public Transform controlsObject;
    public Transform settingsObject;
    GameObject mask;
    bool isShowing;
    bool isSliding;
    public string[] qualityLevels;
    public Dropdown dDownTiles;
    public Dropdown dDownQuality;
    public Dropdown dDownFaces;
    Preferences preferences;
    controls controls;
    GameObject settingsPrefab;
    ModelTile modelTile;
    public List<string> tilesNames { get; private set; }

    public List<string> qualityList { get; private set; }
    public List<string> spriteNames { get; private set; }

    ////METHODS
    public void ShowSettings()
    {
        Animation anim = settingsObject.GetComponent<Animation>();
        if (!isShowing)
        {
            modelTile.SlideIn();

            anim.Play("slideIn");
            isShowing = true;
            mask.transform.localScale = new Vector3(5, 5, 0);
          GameObject.Find("settings").GetComponent<Animator>().Play("slide");
            
            //settingsPrefab.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        }
        else
        {
            modelTile.SlideOut();
            anim.Play("slideOut");
            mask.transform.localScale = new Vector3(0, 0, 0);
            isShowing = false;
            //settingsPrefab.transform.localScale = new Vector3(0, 0, 0);
        }


    }
    void TransparentMask()
    {
    }
    void InitiateTile()
    {
        
    }
    public void TilesSettings()
    {
        

    }
    public void Apply()
    {
        string choice = "";
        switch (dDownFaces.value)
        {
            case 0:
                choice = "Fruits(B&W)";
                break;
            case 1:
                choice = "Fruits(Oil)";
                break;
            case 2:
                choice = "Sunny";
                break;
            default:
                break;
        }
        PlayerPrefs.SetString("savedFaces", choice);
        controls.Initiate();
    }
    public void QualSettings()
    {
        
        Dropdown.OptionData data = new Dropdown.OptionData();
        data.text = Enum.GetName(typeof(QualityLevel), 0);
        data.image = Resources.Load<Sprite>("sliders/menuSlider");
        dDownQuality.options.Add(data);
       

    }
    public void ImagesSettings()
    {


    }
   
    // Use this for initialization
    void Start()
    {

        preferences = preferencesObject.GetComponent<Preferences>();

        controls = controlsObject.GetComponent<controls>();
        modelTile = modelObject.GetComponent<ModelTile>();
        settingsPrefab = GameObject.Find("settings");
        isShowing = false;
        mask =  Instantiate(Resources.Load("misc/mask"), new Vector3(0, 0, -1.5f), Quaternion.identity) as GameObject;
        mask.transform.localScale = new Vector3(0, 0, 0);

        //TilesInit
        dDownTiles.onValueChanged.AddListener(delegate { onDropdownTilesValueChanged(); });

        List<GameObject> tiles = new List<GameObject>(Resources.LoadAll<GameObject>("tiles"));
        tilesNames = new List<string>(tiles.Select(x => x.name));
        dDownTiles.ClearOptions();
        dDownTiles.AddOptions(tilesNames);

        //Quality Init
        dDownQuality.onValueChanged.AddListener(delegate { onDropdownQualityValueChanged(); });
        qualityLevels = Enum.GetNames(typeof(QualityLevel));
         qualityList = new List<string>();
        qualityList.AddRange(qualityLevels);
        dDownQuality.ClearOptions();
        dDownQuality.AddOptions(qualityList);
        QualitySettings.SetQualityLevel(5);
        //FacesInit
        dDownFaces.onValueChanged.AddListener(delegate { onDropDownFacesValueChanged(); });
        Sprite[] smth = Resources.LoadAll<Sprite>("faces");
        //THIS ONE IS FOR YOU HARAMBE APPRECIATE YA GOD BLESS 
         spriteNames = new List<string>(smth.Where(x => x.name.Contains("_0")).Select(y => y.name.TrimEnd(new char[]{ '_', '0' })));
        dDownFaces.ClearOptions();
        dDownFaces.AddOptions(spriteNames);
        controls.Initiate();
        modelTile.Create(dDownTiles.options[preferences.GetSavedTiles()].text, dDownFaces.options[preferences.GetSavedFaces()].text);
    }

    private void onDropDownFacesValueChanged()
    {

        preferences.SetCurrentFaces(dDownFaces.value);
    }

    private void onDropdownTilesValueChanged()
    {
        string s = dDownTiles.options[dDownTiles.value].text;
        modelTile.Change(dDownTiles.options[dDownTiles.value].text, dDownFaces.options[dDownFaces.value].text);
    
        preferences.SetCurrentTiles(dDownTiles.value);
    }
    private void onDropdownQualityValueChanged()
    {
        QualitySettings.SetQualityLevel(dDownQuality.value);

    }

    void Initialize()
    {
        dDownQuality.ClearOptions();

        qualityLevels = Enum.GetNames(typeof(QualityLevel));
        List<string> qualityList = new List<string>();
        qualityList.AddRange(qualityLevels);

        dDownQuality.AddOptions(qualityList);
    }
    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator slideIn(Dropdown menuStrip)
    {
        yield return null;

    }
    IEnumerator slideOut()
    {
        yield return null;

    }
}
