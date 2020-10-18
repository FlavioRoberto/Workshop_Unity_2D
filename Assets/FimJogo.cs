using UnityEngine;
using UnityEngine.SceneManagement;

public class FimJogo : StateMachineBehaviour
{   
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var gameOverUi = GameObject.Find("FimJogoCanvas");

        gameOverUi.SetActive(true);

        if (Input.GetKey(KeyCode.KeypadEnter))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
