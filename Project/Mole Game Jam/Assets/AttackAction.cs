using System.Collections;
using UnityEngine;

public class AttackAction : MonoBehaviour
{
    public float DamageValue = 25;
    public float DelayDuration = 3f;
    
    private float curTime;
    private bool _attackEnabled = true;

    private IEnumerator attackRoutine = null;
    private void OnTriggerStay(Collider other)
    {
        if (CanAttack(other.tag))
            Attack();
    }

    private void Attack()
    {
        PlayerController.Instance.DamageTaken(DamageValue);
        attackRoutine = DelayAttack();
        StartCoroutine(attackRoutine);
    }

    private bool CanAttack(string target)
    {
        return target == "Player" && _attackEnabled;
    }

    private IEnumerator DelayAttack() 
    {
        curTime = 0;
        _attackEnabled = false;
        while (curTime < DelayDuration)
        {
            curTime += 1f;
            yield return new WaitForSeconds(curTime);
        }
        _attackEnabled = true;
        StopCoroutine(attackRoutine);
    }
}
