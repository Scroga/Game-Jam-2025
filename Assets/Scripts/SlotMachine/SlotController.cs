using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
public class SlotController : MonoBehaviour
{
    [SerializeField]
    private float _initialPosition = -2.7f;

    private float _positionStep = 4.35f;

    private const float _startPosition = 15.5f;
    private const float _endPosition = -18.5f;

    private const float _timeInterval = 0.01f; // time between shifts of row positions
    private const float _rotationSpeed = 1.2f;

    private float rotatingDuration = 955.0f;
    private float timeInterval;

    public bool rowStopped { get; private set; }
    public SlotType stoppedSlot { get; private set; }
    void Start()
    {
        rowStopped = true;
        _initialPosition = UnityEngine.Random.Range(_startPosition, _endPosition);
        SetYPos(_initialPosition);
    }

    private void SetYPos(float value)
    {
        var pos = transform.position;
        pos.y = value;
        transform.position = pos;
    }

    public void StartRotating()
    {
        if (rowStopped)
        {
            StartCoroutine(Rotate());
            //StartCoroutine(RotateDebug());
            rowStopped = false;
        }
    }

    public void SetInitialPosition()
    {
        SetYPos(_initialPosition);
    }

    private IEnumerator Rotate()
    {
        int randomSpeed = UnityEngine.Random.Range(6, 15);
        int randomSpeedFactor = UnityEngine.Random.Range(5, 9);

        float rotatinsSpeed = _rotationSpeed * (randomSpeed / 10.0f);
        while (rotatinsSpeed > 0.05f)
        {
            if (transform.position.y <= _endPosition)
            {
                SetYPos(_startPosition);
            }
            SetYPos(transform.position.y - rotatinsSpeed);
            rotatinsSpeed *= 1 - (randomSpeedFactor / rotatingDuration);

            yield return new WaitForSecondsRealtime(_timeInterval);
        }
        SetCurrentSlot();
        rowStopped = true;
    }

    private IEnumerator RotateDebug()
    {
        float timeInterval = 0.4f;

        while (true)
        {
            if (transform.position.y <= _endPosition)
            {
                SetYPos(_startPosition);
            }
            SetYPos(transform.position.y - _positionStep);
            Debug.Log(transform.position.y);
            yield return new WaitForSecondsRealtime(timeInterval);
        }
        SetCurrentSlot();
        rowStopped = true;
    }

    private bool IsInRange(float rangeCenter, float value)
    {
        float range = _positionStep / 2.0f;
        return (rangeCenter + range) >= value && (rangeCenter - range) < value;
    }

    private void SetCurrentSlot()
    {
        float heartPosition = -19.3f;

        if (IsInRange(heartPosition + (_positionStep * (int)SlotType.HEART), transform.position.y))
        {
            stoppedSlot = SlotType.HEART;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.SPADE), transform.position.y))
        {
            stoppedSlot = SlotType.SPADE;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.QUESTION), transform.position.y))
        {
            stoppedSlot = SlotType.QUESTION;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.APPLE), transform.position.y))
        {
            stoppedSlot = SlotType.APPLE;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.SEVEN), transform.position.y))
        {
            stoppedSlot = SlotType.SEVEN;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.DOLLAR), transform.position.y))
        {
            stoppedSlot = SlotType.DOLLAR;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.LEMON), transform.position.y))
        {
            stoppedSlot = SlotType.LEMON;
        }
        else if (IsInRange(heartPosition + (_positionStep * (int)SlotType.BAR), transform.position.y))
        {
            stoppedSlot = SlotType.BAR;
        }
        else if (IsInRange(heartPosition + (_positionStep * ((int)SlotType.BAR + 1)), transform.position.y))
        {
            stoppedSlot = SlotType.HEART;
        }
        else
        {
            stoppedSlot = SlotType.UNKNOWN;
        }
    }
}
