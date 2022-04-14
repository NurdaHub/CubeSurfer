using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private EventDetector eventDetector;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float limitPosition = 3.5f;
    
    private PointerEventData _currentEventData;
    private Camera _mainCamera;
    private Vector3 _startPosition = new Vector3(0, -0.43f, 0);
    private float _pointerDistance;
    private bool _isSwiping;
    private bool _mustMove;

    private void Start()
    {
        _mainCamera = Camera.main;
        StartMovement();
        eventDetector.OnPointerDownAction += OnPointerDown;
        eventDetector.OnPointerUpAction += OnPointerUp;
    }

    private void Update()
    {
        if (_mustMove)
            MoveForward();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }

    private IEnumerator MoveSide()
    {
        while (_isSwiping)
        {
            var playerPosition = transform.position;
            var newXPosition = EventToWorldPosition().x + _pointerDistance;
            playerPosition = new Vector3(newXPosition, playerPosition.y, playerPosition.z);

            if (playerPosition.x > limitPosition)
                playerPosition = new Vector3(limitPosition, playerPosition.y, playerPosition.z);
            
            if (playerPosition.x < -limitPosition)
                playerPosition = new Vector3(-limitPosition, playerPosition.y, playerPosition.z);

            transform.position = playerPosition;

            yield return null;
        }
    }

    private void OnPointerDown(PointerEventData eventData)
    {
        _currentEventData = eventData;

        if (_currentEventData != null)
        {
            _pointerDistance = transform.position.x - EventToWorldPosition().x;
            _isSwiping = true;
            StartCoroutine(MoveSide());
        }
    }

    private void OnPointerUp()
    {
        _isSwiping = false;
    }

    private Vector3 EventToWorldPosition()
    {
        var eventPosition = new Vector3(_currentEventData.position.x, _currentEventData.position.y, 1);
        var worldPosition = _mainCamera.ScreenToWorldPoint(eventPosition);

        return worldPosition;
    }

    public void StartMovement()
    {
        _mustMove = true;
    }

    public void StopMovement()
    {
        _mustMove = false;
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
        StartMovement();
    }
}
