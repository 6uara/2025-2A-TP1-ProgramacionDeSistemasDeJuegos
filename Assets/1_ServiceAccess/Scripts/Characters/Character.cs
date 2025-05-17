using System;
using UnityEngine;

namespace Excercise1
{
    public class Character : MonoBehaviour, ICharacter
    {
        [SerializeField] protected string id;

        protected virtual void OnEnable()
        {
            var characterService = ServiceLocator.GetService<CharacterService>();
            characterService.TryAddCharacter(id, this);
        }

        protected virtual void OnDisable()
        {
            var characterService = ServiceLocator.GetService<CharacterService>();
            characterService.TryRemoveCharacter(id);
        }
    }
}