using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum SlotType { HEART = 0, SPADE,  QUESTION, APPLE, SEVEN, DOLLAR, LEMON, BAR, UNKNOWN};

public class SlotMachineController : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI prizeText;

    [SerializeField]
    private SlotMachineBotton botton;

    private int prizeValue;
    //private bool resultsChecked = false;

    public static UnityEvent OnStart;

    [SerializeField]
    private SlotController slot1;

    [SerializeField]
    private SlotController slot2;

    [SerializeField]
    private SlotController slot3;
    void Start()
    {
        botton.Activate();
        prizeValue = 0;
        //resultsChecked = false;
    }

    private bool AreSlotsStopped() {
        return slot1.rowStopped && slot2.rowStopped && slot3.rowStopped;
    }

    public void StartGame() {
        if (AreSlotsStopped())
        {
            botton.Deactivate();
            StartCoroutine(GameHandle());
        }
    }

    public void SetInitialState()
    {
        slot1.SetInitialPosition();
        slot2.SetInitialPosition();
        slot3.SetInitialPosition();
    }

    private IEnumerator GameHandle() {
        yield return new WaitForSeconds(0.5f);
        OnStart?.Invoke();
        slot1.StartRotating();
        slot2.StartRotating();
        slot3.StartRotating();

        yield return new WaitUntil(AreSlotsStopped);
        botton.Activate();
        CheckResults();
    }

    private bool CheckSlots(SlotType value1, SlotType value2, SlotType value3) {
        return 
            slot1.stoppedSlot == value1 &&
            slot2.stoppedSlot == value2 &&
            slot3.stoppedSlot == value3;
    }
    private void CheckResults()
    {
        if (slot1.stoppedSlot == slot2.stoppedSlot && slot1.stoppedSlot == slot3.stoppedSlot)
        {
            HandleThreeSlotsCase(slot1.stoppedSlot);
        }
        else {
            string s1 = slot1.stoppedSlot.ToString();
            string s2 = slot2.stoppedSlot.ToString();
            string s3 = slot3.stoppedSlot.ToString();

            Debug.Log($"{s1} {s2} {s3}");
        }
    }

    private void HandleThreeSlotsCase(SlotType value) {
        Debug.Log($"All {value.ToString()}");
    }
}
