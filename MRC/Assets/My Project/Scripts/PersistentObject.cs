using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    void Awake()
    {
        // Garante que este objeto n�o seja destru�do ao carregar uma nova cena
        DontDestroyOnLoad(gameObject);
    }
}
