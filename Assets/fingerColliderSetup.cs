using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerColliderSetup : MonoBehaviour
{
    public GameObject leftFinger;
    public GameObject rightFinger;

    private struct FingerBone
    {
        public readonly float Radius;
        public readonly float Height;

        public FingerBone(float radius, float height)
        {
            Radius = radius;
            Height = height;
        }

        public Vector3 GetCenter(bool isLeftHand)
        {
            return new Vector3(((isLeftHand) ? -1 : 1) * Height / 2.0f, 0, 0);
        }
    };

    private readonly FingerBone Phalanges = new FingerBone(0.01f, 0.03f);
    private readonly FingerBone Metacarpals = new FingerBone(0.01f, 0.05f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(leftFinger && rightFinger))
        {
            InitFingers();
        }
    }

    private void InitFingers()
    {
        // Get GameObject for both index fingers
        leftFinger = GameObject.Find("hands:b_l_index3");
        if (leftFinger)
        {
            if (!leftFinger.GetComponent<Collider>())
            {
                CreateCollider(leftFinger);
            }
        }
        rightFinger = GameObject.Find("hands:b_r_index3");
        if (rightFinger)
        {
            if (!rightFinger.GetComponent<Collider>())
            {
                CreateCollider(rightFinger);
            }
        }
    }

    private void CreateCollider(GameObject finger)
    {
        //System.Type mType = System.Type.GetType("PositionRotationTracker");
        //finger.AddComponent(mType);
        CapsuleCollider collider = finger.AddComponent<CapsuleCollider>();

        collider.radius = Phalanges.Radius;
        collider.height = Phalanges.Height;
        collider.center = Phalanges.GetCenter(transform.name.Contains("_l_"));
        collider.direction = 0;
        //collider.isTrigger = true;
    }
}
