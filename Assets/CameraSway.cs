using System.Collections;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float newTargetEverySecondsMin = 1;
    public float newTargetEverySecondsMax = 10;
    public float radius;
    public float speed;

    private Vector3 target;
    private Vector3 origin;

    void Awake()
    {
        origin = transform.position;
    }

    IEnumerator Start()
    {
        while (true)
        {
            target = NewTarget();
            yield return new WaitForSeconds(Random.Range(
                newTargetEverySecondsMin,
                newTargetEverySecondsMax));
        }
    }

    void Update()
    {
        transform.position = (Vector3.Lerp(transform.position, target, speed * Time.deltaTime));
    }

    private Vector3 NewTarget() {
        float x = UnityEngine.Random.Range(-radius, radius);
        float y = UnityEngine.Random.Range(-radius, radius);
        float z = UnityEngine.Random.Range(-radius, radius);
        return origin + new Vector3(x,y,z);
    }
}
