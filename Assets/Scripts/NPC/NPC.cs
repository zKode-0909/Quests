using UnityEngine;

public class NPC : MonoBehaviour,IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log($"GOT DAMN NIGGA! I JUST GOT HIT for {damage} damage, my shit be bleedin!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
