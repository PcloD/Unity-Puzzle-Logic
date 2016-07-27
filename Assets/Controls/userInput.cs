using UnityEngine;
using System.Collections;

public class userInput : MonoBehaviour
{
    private bool _mouseState;
    private GameObject target;
    public Vector3 screenSpace;
    public Vector3 offset;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            target = GetClickedObject(out hitInfo);

            if (target != null && target.gameObject.tag == "Puzzle")
            {
                _mouseState = true;
                screenSpace = Camera.main.WorldToScreenPoint(target.transform.position);
                offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (target != null && target.tag == "Puzzle")
            {
                Vector3 newPosition = new Vector3(Mathf.Round(target.transform.position.x), Mathf.Round(target.transform.position.y), Mathf.Round(target.transform.position.z));
                Vector3 oldPosition = target.transform.GetComponent<PuzzlePiece>().getPosition();

                if (newPosition.x >= 0 && newPosition.y >= 0)
                {
                    if (this.GetComponent<GameManager>().returnPiece((int)newPosition.x, (int)newPosition.y) == -1)
                    {
                        this.GetComponent<GameManager>().changeStatus((int)Mathf.Round(oldPosition.x), (int)Mathf.Round(oldPosition.y), (int)newPosition.x, (int)newPosition.y, target.transform.GetComponent<PuzzlePiece>().ID);
                        target.GetComponent<PuzzlePiece>().setPosition(newPosition);
                        target.transform.position = newPosition;
                    }
                    else
                    {
                        target.transform.position = oldPosition;
                    }
                }
                else
                {
                    target.transform.position = newPosition;
                    target.GetComponent<PuzzlePiece>().setPosition(newPosition);
                    if ((int)oldPosition.x >= 0 && (int)oldPosition.y >= 0)
                    {
                        this.GetComponent<GameManager>().assignPiece((int)oldPosition.x, (int)oldPosition.y, -1);
                    }
                }
                _mouseState = false;
            }
        }
        if (_mouseState)
        {
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            target.transform.position = curPosition;
        }
    }
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}