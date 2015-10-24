using UnityEngine;
using System.Collections;

public class Building : Selectable
{
    public int hitPoints;
    public int buildingsize;

    Camera cam;

    public enum BuildingState
    {
        Placed,         //Set on ground
        Moving          //Moving / Repositioning
    }

    public enum PositionState
    {
        Possible,       //Able to be placed in its current location
        NotPossible     //Not able to be placed in its current location
    }


    PositionState currentPosition = new PositionState();
    BuildingState currentState = new BuildingState();

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("worldCam").GetComponent<Camera>();
        // rend = GetComponent<Renderer>();
    }


    public void CheckState()
    {
        if (currentState == BuildingState.Moving)
        {
            /*
            *   check if tile bellow is free to go on
            *
            *   if tiles is free to go on, change building tiles to green;
            *   else if tiles are not free to go on, change building tiles to red;
            *
            */
        }
    }

    public void BuildingDestroyed()
    {
        //Destroy Building
    }


    void OnMouseDrag()
    {
        transform.position = GameManager.gm.mousePos;//new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, cam.ScreenToWorldPoint(Input.mousePosition).z);
    }
    void OnMouseExit()
    {
        // rend.material.color = Color.white;
    }
}