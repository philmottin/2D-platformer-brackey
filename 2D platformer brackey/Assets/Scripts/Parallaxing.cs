using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;         // Array (list) of the background and foreground to be parallaxed
    private float[] parallexScales;         // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;             // How smooth the parallex is going to be. Make sure to set this above 0
    public float force = 10f;               // smooth doesn't seenm to be causing any results, so use this force instead
    private Transform cam;                  // reference to the main camera's transform
    private Vector3 previousCamPos;         // The position of the camera in the previous frame

    // Is called before start(). Great for references
    private void Awake() {
        // set up camera reference
        cam = Camera.main.transform;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;

        // assigning corresponding parallex scales
        parallexScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallexScales[i] = backgrounds[i].position.z * force; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each background
        for (int i = 0; i < backgrounds.Length; i++) {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
            //float parallax = (previousCamPos.x - cam.position.x) * parallexScales[i];
            float parallax = (cam.position.x - previousCamPos.x) * parallexScales[i];

            // set a target x position wich is the current position plus the parallax
            float backgroundTargetX = backgrounds[i].position.x + parallax;

            // create a target position wich is the background's current position with it's target x postion
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current postion and the target postion using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;        
    }
}
