using UnityEngine;

public class ChildCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        transform.GetComponentInParent<IChildCollidable>().Collision(col);
    }
}