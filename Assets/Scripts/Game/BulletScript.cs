using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject destroyEffect = null;

    private Vector3 targetPosition = Vector3.zero;
    public float speed = 30f;
    public void SetTargetPosition(Vector3 start, Vector3 target)
    {
        transform.position = start;
        targetPosition = target;
    }

    public void SetYOffset(float offset)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPosition != Vector3.zero)
        {
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                if(destroyEffect != null)
                {
                    Debug.Log("*EXPLOSION!*");
                    Instantiate(destroyEffect, transform.position, Quaternion.identity);
                }
                Destroy(transform.gameObject);
            }
        }
    }
}
