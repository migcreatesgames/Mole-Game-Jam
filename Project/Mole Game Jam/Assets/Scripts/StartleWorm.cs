using UnityEngine;

public class StartleWorm : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private bool _isScarred = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isScarred)
        {
            _animator.SetTrigger("Escape");
            _isScarred = true;
        }
    }
}
