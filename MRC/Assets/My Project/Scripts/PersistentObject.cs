using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        // Garante que este objeto não seja destruído ao carregar uma nova cena
        DontDestroyOnLoad(gameObject);
    }
}
