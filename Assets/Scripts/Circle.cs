using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CircleLayoutGroup : MonoBehaviour
{
    [Range(0,360)]
    public int angleView;
    public float radius;
    public bool ChangeRotation;
    Transform[] Slot;
    
    void Start()
    {
        getChildTransform();
        CircleLG();
    }

#if UNITY_EDITOR
    void Update()
    {
        getChildTransform();
        CircleLG();
    }
#endif

    public void CircleLG()
    {
        //set the local position of all children
        for(int i=0;i<transform.childCount;i++)
        {
            //calculate the angle in radian for each children 
            float angle = i * (angleView*Mathf.Deg2Rad)/transform.childCount;
            Slot[i].localPosition = new Vector3(Mathf.Cos(angle)*radius ,Mathf.Sin(angle)*radius,0);
            
            if(ChangeRotation)
                //change the rotation of z axis to the angle in degree
                Slot[i].localRotation = Quaternion.Euler(0,0,angle * Mathf.Rad2Deg);
            else
                Slot[i].localRotation = Quaternion.identity;
        }
    }

    void getChildTransform()
    {
        // set the array size of Slot as much as the child
        Slot = new Transform[transform.childCount];

        //get Transform component of each child and attach it to the Slot variable
        for (int i = 0; i < transform.childCount; i++)
        {
            Slot[i] = transform.GetChild(i).GetComponent<Transform>();
        }
    }
}
