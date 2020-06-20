using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticle : MonoBehaviour
{
    public List<GameObject> particles;

    public void Emit(int count)
    {
        foreach (GameObject particle in particles)
        {
            Instantiate(particle, transform);
            particle.GetComponent<ParticleSystem>().Emit(count);
        }
    }
}
