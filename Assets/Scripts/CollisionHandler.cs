using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Switch statement
    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;

            case "Finish":
                Debug.Log("Congratulations!");
                break;

            default:
                Debug.Log("Sorry, you lose");
                break;
        }
    }
}
