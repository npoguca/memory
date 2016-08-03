using UnityEngine;
using System.Collections;

public class Preferences : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("savedTiles", 3);
        PlayerPrefs.SetInt("savedFaces", 0);
        PlayerPrefs.SetInt("savedQuality", 5);

        SetCurrentTiles(GetSavedTiles());
        SetCurrentQuality(GetSavedQuality());
        SetCurrentFaces(GetSavedFaces());
        
        
    }

    //Get Current Prefs
    public int GetCurrentTiles()
    {
        return PlayerPrefs.GetInt("currentTiles");
    } 
    public int GetCurrentFaces()
    {
        return PlayerPrefs.GetInt("currentFaces");
    }
    public int GetCurrentQuality()
    {
        return PlayerPrefs.GetInt("currentQuality");
    }

    //Set Current Prefs
    public void SetCurrentTiles(int index)
    {
        PlayerPrefs.SetInt("currentTiles", index);
    }
    public void SetCurrentFaces( int face)
    {
        PlayerPrefs.SetInt("currentFaces", face);
    }
    public void SetCurrentQuality(int index)
    {
        PlayerPrefs.SetInt("currentQuality", index);

    }

    //Get Saved Prefs
    public int GetSavedTiles()
    {
        return PlayerPrefs.GetInt("savedTiles");
    }
    public int GetSavedFaces()
    {
        return PlayerPrefs.GetInt("savedFaces");
    }
    public int GetSavedQuality()
    {
        return PlayerPrefs.GetInt("savedQuality");
    }

    //Set Saved Prefs
    public void SetSavedTiles(int index)
    {
        PlayerPrefs.SetInt("savedTiles", index);
    }
    public void SetSavedFaces(int index)
    {
        PlayerPrefs.SetInt("savedFaces", index);
    }
    public void SetSavedQuality(int index)
    {
        PlayerPrefs.SetInt("savedQuality", index);

    }
    // Update is called once per frame
    void Update () {
        

    }
}
