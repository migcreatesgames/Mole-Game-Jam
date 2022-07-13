using System.Collections;
using UnityEngine;

public class HideComponent : MonoBehaviour
{
    private bool _isVisible = true;

    public GameObject _meshRenderer;
    [SerializeField]
    private GameObject _mound;
    private Animator _animator;

    public bool IsVisible { get => _isVisible; set => _isVisible = value; }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void Hide()
    {
        StartCoroutine("InitHide");
    }
    public void UnHide()
    {
        StopCoroutine("InitDig");
        _meshRenderer.SetActive(true);
        _mound.SetActive(false);
        _isVisible = true;

        //  change speed
    }
    private IEnumerator InitHide()
    {

        _animator.SetTrigger("DigInit");
        yield return new WaitForSeconds(.1f);
        _meshRenderer.SetActive(false);
        _mound.SetActive(true);
        _isVisible = false;
        //  change speed
        StopCoroutine("InitHide");
    }

}
