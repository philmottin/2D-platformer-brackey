using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

static class Constants
{
    public const int LEFT = -1;
    public const int RIGHT = 1;
}
public class Tiling : MonoBehaviour
{
    public int offsetX = 2; // offset for checking the edges

    // check if need to instatiate
    public bool hasRightBuddy = false;
    public bool hasLeftBuddy = false;

    // used if object is not tilable
    public bool reverseScale = false;

    //width of our element
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake() {
        cam = Camera.main;
        myTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update() {
        // does it still need buddies? if not do nothing
        if (!hasLeftBuddy || !hasRightBuddy) {
            // calculate the camera's extend (half the widht) of what the camera can see in world coordinates
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            // calculate the x position where the camera can see the edge of the sprite (element)
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            // checking if we can see the edge of the element and then calling MakeNewBuddy() if we can
            if (cam.transform.position.x >= (edgeVisiblePositionRight - offsetX) && hasRightBuddy == false) {
                MakeNewBuddy(1);
                hasRightBuddy = true;
            } else if (cam.transform.position.x <= (edgeVisiblePositionLeft + offsetX) && hasLeftBuddy == false) {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft) {
        // calculating the new position for our new buddy
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        // instantiating newBuddy and storing in a variable
        Transform newBuddy = (Transform)Instantiate(myTransform, newPosition, myTransform.rotation);

        // if not tileble let's reverse the x size
        if (reverseScale) {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0) {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        } else {
            newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
        }

    }
}
