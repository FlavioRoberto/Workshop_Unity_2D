using UnityEngine;
using UnityEngine.UI;

public class BarraVida2 : MonoBehaviour
{
    public float Vida;
    public Image Barra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Barra.fillAmount = Vida;
    }
}
