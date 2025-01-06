using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Start()
    {
        int sessionCount = FindObjectsOfType<ScenePersist>().Length;
        if (sessionCount > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ResetScenePersistence()
    {
        Destroy(this.gameObject);
    }
}
