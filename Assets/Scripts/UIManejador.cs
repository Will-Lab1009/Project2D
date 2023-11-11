using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManejador : MonoBehaviour
{
    private static UIManejador instace;
    public static UIManejador Instance 
    { get 
        { if(instace == null)
            {
                instace = FindObjectOfType<UIManejador>();
            }
            return instace; } }

    [SerializeField] private Transform lifeParent;
    [SerializeField] private GameObject lifePrefab;

    private Stack<GameObject> listavidas = new Stack<GameObject>();

    public void AgregarVidas(int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            listavidas.Push( Instantiate(lifePrefab, lifeParent));
        }
    }

    public void QuitarVidas()
    {
        Destroy(listavidas.Pop());
    }
}
