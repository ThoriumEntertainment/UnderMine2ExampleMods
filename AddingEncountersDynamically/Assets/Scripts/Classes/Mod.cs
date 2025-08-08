using Thor.Core;
using UnityEngine;

namespace Mod_e27493c24d5347f894c02a48faf3a207
{
    public class Mod : MonoBehaviour
    {
        [SerializeField]
        private PrefabReference<Encounter> m_spawnInEncounter;

        [SerializeField]
        private AssetReference<SetPieceData> m_setPieceToSpawn;

        private void Awake()
        {
            // Fired the first time an encounter is entered
            Services.Events.RegisterGameEvent(GameEventType.EncounterBegin, OnEncounterBegin);
        }

        private void OnEncounterBegin(GameEvent evt)
        {
            var encounter = evt.ObjectValue as Encounter;
            if (encounter == null)
            {                
                return;
            }
            
            if (encounter.GUID == m_spawnInEncounter.Asset.GUID)
            {                
                if (!Services.Entities.Spawn(
                    new IEntityService.SpawnParams()
                    {
                        data = m_setPieceToSpawn.Asset,
                        scale = Vector3.one,
                        position = Vector3.zero,                        
                    },
                    out var spawned
                ))
                {
                    Debug.LogError($"[Mod] Failed to spawn set piece");
                }
            }
        }
    }
}
