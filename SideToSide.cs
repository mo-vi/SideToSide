using UnityEngine;

public class SideToSide : MonoBehaviour
{
    private enum ma { worldX, worldY, worldZ }
    private enum mt { linear, sine, customCurve }
    private Transform owner;
    private Vector3 startPosition;
    private Vector3 currentDirection;
    [SerializeField] private ma moveAlong = ma.worldX;
    [SerializeField] private mt movementType = mt.linear;
    [SerializeField] [Range(0, 1)] private float movementSpeed = 1.0f;
    [SerializeField] private float movementDistance = 5.0f;
    [Header("CUSTOM CURVE ONLY:")]
    [SerializeField] AnimationCurve customCurve = null;

    private void Start()
    {
        owner = GetComponent<Transform>();
        startPosition = owner.position;
    }

    private void Update()
    {
        switch(moveAlong)
        {
            case ma.worldX:
                currentDirection = Vector3.right;
                break;
            case ma.worldY:
                currentDirection = Vector3.up;
                break;
            case ma.worldZ:
                currentDirection = Vector3.forward;
                break;
        }

        switch(movementType)
        {
            case mt.linear:
                owner.position = Vector3.Lerp(startPosition, startPosition + (currentDirection * movementDistance), Mathf.PingPong(Time.time * movementSpeed, 1.0f));
                break;
            case mt.sine:
                owner.position = Vector3.Lerp(startPosition, startPosition + (currentDirection * movementDistance), (Mathf.Sin(Time.time * movementSpeed * Mathf.PI) + 1.0f) * 0.5f);
                break;
            case mt.customCurve:
                owner.position = Vector3.Lerp(startPosition, startPosition + (currentDirection * movementDistance), customCurve.Evaluate(Mathf.PingPong(Time.time * movementSpeed, 1.0f)));
                break;
        }
    }
}
