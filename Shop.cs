using UnityEngine;

public class Shop : MonoBehaviour
{
    private bool canShop;

    private void Update()
    {
        if (canShop)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                if (UIController.instance.shopController.gameObject.activeSelf == false)
                {
                    UIController.instance.shopController.OpenClose();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShop = false;
        }
    }
}
