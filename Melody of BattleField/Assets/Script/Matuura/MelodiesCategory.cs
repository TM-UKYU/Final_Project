using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Magic
{
    Fire,
    Ice,
    Electro,
    Wind
}

public enum Spell
{
    Deel,
    Srea
}

public class MelodiesCategory : MonoBehaviour
{
    public List<Spell> FireSpellList = new List<Spell>();
    public List<Spell> IceSpellList = new List<Spell>();
    public List<Spell> ElectroSpellList = new List<Spell>();
    public List<Spell> WindSpellList = new List<Spell>();


    private void Start()
    {
      
        for (int i = 0; i < 3; i++)
        {
            FireSpellList.Add(Spell.Deel);
            WindSpellList.Add(Spell.Srea);
        }
        ElectroSpellList.Add(Spell.Deel);
        for (int i = 0; i < 2; i++)
        {
            IceSpellList.Add(Spell.Deel);
            ElectroSpellList.Add(Spell.Srea);
        }
        IceSpellList.Add(Spell.Srea);
    }

    public class Melodies : MonoBehaviour
    {
        public Magic MelodyTag;
        public string Message;

    }

    public class Fire : Melodies
    {
        private void Start()
        {
            MelodyTag = Magic.Fire;
            Message = "Fire";
        }

    }

    public class Ice : Melodies
    {
        private void Start()
        {
            MelodyTag = Magic.Ice;
            Message = "Ice";
        }

    }

    public class Electro : Melodies
    {
        private void Start()
        {
            MelodyTag = Magic.Electro;
            Message = "Electro";
        }

    }

}