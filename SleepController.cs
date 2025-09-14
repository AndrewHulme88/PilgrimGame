using UnityEngine;
using UnityEngine.InputSystem;

public class SleepController : MonoBehaviour
{
    private bool canSleep;

    private void Update()
    {
        if(canSleep)
        {
            if(Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (TimeController.instance != null)
                {
                    TimeController.instance.EndDay();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSleep = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSleep = false;
        }
    }
}
