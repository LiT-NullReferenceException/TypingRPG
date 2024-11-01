using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem2;

    [ContextMenu("aaaa")]
    public void Attack()
    {
        _particleSystem2.Play();
        Debug.Log("再生しました");
    }
}
