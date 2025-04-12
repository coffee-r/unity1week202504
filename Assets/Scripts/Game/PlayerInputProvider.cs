using UnityEngine;

public class PlayerInputProvider : MonoBehaviour
{
    int moveX = 0;
    int moveY = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1;
        }
        else {
            moveX = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1;
        }
        else {
            moveY = 0;
        }
    }

    public Vector3 MoveVector3()
    {
        return new Vector3(moveX, moveY, 0);
    }
}
