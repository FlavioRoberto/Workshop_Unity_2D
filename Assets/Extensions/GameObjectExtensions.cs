using UnityEngine;

namespace Assets.Extensions
{
    public static class GameObjectExtensions
    {
        public static void DesabilitarColisoresFilhos(this GameObject value)
        {
            var colisores = value.GetComponentsInChildren<Collider2D>();
            foreach (var colisor in colisores)
            {
                colisor.enabled = false;
            }
        }

        public static void HabilitarColisoresFilhos(this GameObject value)
        {
            var colisores = value.GetComponentsInChildren<Collider2D>();
            foreach (var colisor in colisores)
            {
                colisor.enabled = true;
            }
        }
    }
}
