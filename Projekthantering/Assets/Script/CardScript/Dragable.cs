using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    public bool isMouseDown;

    private Vector3 mOffset;

    private float mZCoord;

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        GetComponent<Card>().SetState(Card.CardState.PickedUp);
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        isMouseDown = true;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (isMouseDown)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            GetComponent<Card>().SetState(Card.CardState.InHand);
            isMouseDown = false;
        }
    }
}
