using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    //Know what objects are clickable
    public LayerMask clickableLayer;

    //Swap Cursors per object
    public Texture2D pointer; //Normal Pointer
    public Texture2D target; //Cursor for clickable objects like the world
    public Texture2D doorway; //Cursor for doorways
    public Texture2D combat; //Cursor combat actions

    public EventVector3 OnClickEnvironment; //creates onclick event

	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value)) //if mouse hits clickable spot in world
        {
            bool door = false;
            bool item = false;

            if(hit.collider.gameObject.tag == "Doorway") //if it hits a doorway
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto); // change to door cursor
                door = true;
            }

            else if(hit.collider.gameObject.tag == "Item") //if it hits an item
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto); //change to combat(action) cursor
                item = true;
            }

            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto); //else use target for movement, if stiil on moveable area
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (door)
                {
                    Transform doorway = hit.collider.gameObject.transform; //stores door vector data as variable

                    OnClickEnvironment.Invoke(doorway.position); //moves to door onclick using vector data from doorway variable
                    Debug.Log("DOOR"); //logs that the door object is a door
                }

                else if(item)
                {
                    Transform itemPos = hit.collider.gameObject.transform; //stores item vector data as variable

                    OnClickEnvironment.Invoke(itemPos.position); //onclick moves to item using vector data from variable
                    Debug.Log("ITEM"); //logs item object is an item
                }

                else
                {
                    OnClickEnvironment.Invoke(hit.point); //onclick sends signal to navmesh agent to move
                }
            }
        }

        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto); //if non clickable area use default pointer
        }
	}
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
