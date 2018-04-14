using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSquadSelector : MonoBehaviour {
    #region ABOUT
    /**
     * Handles touchpad input with relative X, Y coordinates for squad selection.
     * Assumes a maximum of 4 squads.
     **/
    #endregion

    #region VARIABLES
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController controller;

    private Vector2[] squads = { new Vector2(0, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(1, 0) };

    [Tooltip("Object with a Player.cs script attached.")]
    public Player mPlayer;
    #endregion

    // Attaches the controller callback handler after acquiring the components.
	void Start () {
        if (!mPlayer)
        {
            mPlayer = GetComponentInParent<Player>();
        }
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
	}
	
	// Checks for input on the touchpad
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObject.index);
	}

    // Processes the input and calls the ProcessSquadSelection with the X, Y values of the click
    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        if (device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            ProcessSquadSelection(device.GetAxis().x, device.GetAxis().y);
        }
    }

    // Compares to find the shortest distances and ties in the correct squad selected out of the 4.
    private void ProcessSquadSelection(float x, float y)
    {
        Vector2 clickPos = new Vector2(x, y);
        float dist = Vector2.Distance(clickPos, squads[0]);
        int squadID = 1; // The squad we're selecting
        for (int i = 1; i < 4; i++)
        {
            float newDist = Vector2.Distance(clickPos, squads[i]);
            if (dist < newDist)
            {
                dist = newDist;
                squadID = i+1;
            }
        }
        // At the end, pass squadID
        mPlayer.SelectSquad(squadID);
    }

}
