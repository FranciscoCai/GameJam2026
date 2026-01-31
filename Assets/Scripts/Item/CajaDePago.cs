using UnityEngine;
using UnityEngine.SceneManagement;

public class CajaDePago : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventoryData = other.GetComponent<Inventory>();
            if(inventoryData != null)
            {
                if(inventoryData.HasAllItems())
                {
                    SceneManager.LoadScene("WinScene");
                }
            }
        }
    }
}
