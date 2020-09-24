using Assets.Enums;
using UnityEngine;

namespace Assets.Extensions
{
    public static class TransformExtension
    {
        public static void DefinirPosicaoMovimento(this Transform value, EDirecaoMovimento direcao)
        {
            switch (direcao)
            {
                case EDirecaoMovimento.DIREITA: value.eulerAngles = new Vector2(0, 0);break;
                case EDirecaoMovimento.ESQUERDA: value.eulerAngles = new Vector2(0, 180);break;
            }
        }
    }
}
