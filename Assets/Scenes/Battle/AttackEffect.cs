using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticle;

    [ContextMenu("aaaa")]
    public void PlayExplosionEffect()
    {
        _explosionParticle.Play();
        Debug.Log("爆発エフェクトを再生");
    }
}
