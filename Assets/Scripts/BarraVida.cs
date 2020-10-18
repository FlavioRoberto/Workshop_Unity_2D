using Assets.Enums;
using UnityEngine;

public class BarraVida : MonoBehaviour
{
    public GameObject camera;
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        DefinirVida(EStatusVida.ALTA);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.parent = camera.gameObject.transform;
    }

    public void DefinirVida(EStatusVida status)
    {
        _animator.SetInteger("transicao", (int)status);
    }
}
