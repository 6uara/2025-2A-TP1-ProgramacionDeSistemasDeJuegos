using System.Collections.Generic;
using UnityEngine;

namespace Excercise1
{
    public class CharacterService : MonoBehaviour
    {
        private static CharacterService _instance;

        public static CharacterService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<CharacterService>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("CharacterService");
                        _instance = go.AddComponent<CharacterService>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            ServiceLocator.RegisterService<CharacterService>(this);
        }

        private readonly Dictionary<string, ICharacter> _charactersById = new();

        public bool TryAddCharacter(string id, ICharacter character)
            => _charactersById.TryAdd(id, character);

        public bool TryRemoveCharacter(string id)
            => _charactersById.Remove(id);

        public bool TryGetCharacter(string id, out ICharacter character)
            => _charactersById.TryGetValue(id, out character);

    }
}
