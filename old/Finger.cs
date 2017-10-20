using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger: MonoBehaviour {

    //the object is updated to this position every frame
    Vector3 realPos;

    RaycastHit sight;

    private Touch touch;
    public LayerMask touchyLayer;
    //Next var is used for the translation of screen coordinates to world coordinates.
    public Camera view;
    //Distance of the ray.
    public float rayDist = 10f;

    void FixedUpdate ()
    {
        //updates this.object to this position every frame
        transform.position = realPos;
        TouchControls();
}

    private void TouchControls()
    {
        //if nr of touches equals 1 or more
        if(Input.touchCount > 0)
        {
            //Gets the usefull coord from 1st touch
            TouchControlsSTW();
            //Detects what players are touching
            RayCastHitDetection();
        }

    }

    //Gets the useful coords from 1st touch
    private void TouchControlsSTW()
    {
        /* instantiates touch as the first finger pressed
         * assigns tempCoord with touched coordinates. Here coords are still in absolute, i.e.: screen pixels
         * translates screen coordinaes to world coordinates*/
            touch = Input.GetTouch(0);
            Vector3 tempCoord = new Vector3(touch.position[0], touch.position[1], 1f);
            realPos = view.ScreenToWorldPoint(tempCoord);
    }

    //Detects what players are touching
    private void RayCastHitDetection()
    {
        /* draws the ray's raycast
         * creates a ray
         * checks physics stuff and returns info about the ray that hit something. Then it's stored in "sight"*/
        Debug.DrawRay(transform.position, Vector3.forward * rayDist);
        Ray ray = new Ray(transform.position, Vector3.forward * rayDist);
        if(Physics.Raycast(ray, out sight, rayDist, touchyLayer))
        {
            if (touch.phase == TouchPhase.Began)
            {
                print("Started touching: ");
                if (sight.collider.tag != "Player1" && sight.collider.tag != "Player2")
                { print(sight.collider.name); }
                else
                { print(sight.collider.name + " tab."); }
            }
        }
        else
             {   
                //resets this.object to a position (hopefully) outisde of view.
                realPos.Set(50f, 0f, 0f);
                print("Stopped touching.");
             }
    }
}
