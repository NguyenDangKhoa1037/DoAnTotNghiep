using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    [SerializeField] LayerMask RoomLayer;
    private BoxCollider2D colider;
    private Vector2 bound;
    private bool canCheck = false;
    private bool isCheck = true;
    private Vector2 posCheck;
    [SerializeField] bool isDraw = false;
    public bool CanCheck { get => canCheck; set => canCheck = value; }

    private void Awake()
    {
        colider = GetComponent<BoxCollider2D>();
        bound = colider.bounds.size;
    }

    
   
    public bool isFitSpace(Vector2 pos) {
        posCheck = pos;
        Collider2D[] colid = Physics2D.OverlapBoxAll(pos, bound, 0);
        //Room room = GetComponent<Room>();
        for (int i = 0; i < colid.Length; i++) {
            if (colid[i].gameObject.GetComponent<RoomChecker>().CanCheck) {
                print(false);
                return false;
            }
        }
        //for (int i = 0; i < colid.Length; i++) {
        //    print(colid[i].name);
        //}
        //if (colid.Length == 0) print(pos);
        //print(true);
        //print("---------");
        return true;
    }

    private void OnDrawGizmos()
    {
        if(isDraw)
        Gizmos.DrawCube(posCheck, bound);
    }

    public Collider2D getColiderOnSpace(Vector2 pos)
    {
        
        Collider2D[] colid = Physics2D.OverlapBoxAll(pos, bound, 0);

        for (int i = 0; i < colid.Length; i++)
        {
            if (colid[i].gameObject.GetComponent<RoomChecker>().CanCheck) return colid[i];
        }
        return null;
    }
}
