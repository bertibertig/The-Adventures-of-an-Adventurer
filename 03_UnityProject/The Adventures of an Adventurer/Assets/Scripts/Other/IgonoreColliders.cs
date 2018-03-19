using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgonoreColliders : MonoBehaviour {

    public static void IgnoreOrNoteColls(GameObject go1, GameObject go2, bool ignore = true)
    {
        if (go1.GetComponents<PolygonCollider2D>().Length > 0)
            Ignore(go1, go2, ignore);
        if (go1.GetComponents<CircleCollider2D>().Length > 0)
            Ignore(go1, go2, ignore);

    }

    private static void Ignore(GameObject go1, GameObject go2, bool ignore = true)
    {
        if (go2.GetComponents<PolygonCollider2D>().Length > 0)
        {
            foreach (PolygonCollider2D pol1 in go1.GetComponents<PolygonCollider2D>())
            {
                foreach (PolygonCollider2D pol2 in go2.GetComponents<PolygonCollider2D>())
                {
                    Physics2D.IgnoreCollision(pol1, pol2, ignore);
                }
            }
        }
        if (go2.GetComponents<CircleCollider2D>().Length > 0)
        {
            foreach (CircleCollider2D cir1 in go1.GetComponents<CircleCollider2D>())
            {
                foreach (PolygonCollider2D pol2 in go2.GetComponents<PolygonCollider2D>())
                {
                    Physics2D.IgnoreCollision(cir1, pol2, ignore);
                }
            }
        }
    }
}
