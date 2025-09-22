using UnityEngine;

public class ShopController : MonoBehaviour
{
    public void OpenClose()
    {
        if(UIController.instance.inventoryController.gameObject.activeSelf == false)
        {
            gameObject.SetActive(!gameObject.activeSelf);        
        }
    }
}
