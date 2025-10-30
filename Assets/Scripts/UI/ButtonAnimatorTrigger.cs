using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimatorTrigger : MonoBehaviour
{
    private Animator animator;
    private Button button;

    void Awake()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayPressAnimation);
    }

    void PlayPressAnimation()
    {
        if (animator != null)
            animator.SetTrigger("pressed");
    }
}
