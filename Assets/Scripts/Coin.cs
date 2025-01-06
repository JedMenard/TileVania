using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickupSound;

    private GameSession gameSession => FindObjectOfType<GameSession>();

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this.isCollected && collision.CompareTag("Player"))
        {
            this.gameSession.IncrementScore();
            AudioSource.PlayClipAtPoint(this.pickupSound, this.transform.position);
            Destroy(this.gameObject);
            this.isCollected = true;
        }
    }
}
