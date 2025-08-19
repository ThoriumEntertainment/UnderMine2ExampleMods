using UnityEngine;
using Thor.Core;
using System.Collections.Generic;

namespace Mod_d4eb6a34bbed4ad3bc88870d9cef7b75
{
    /**
     * Utility DataObject to build a list of spawnables based on a configurable list
     * 
     * This can be used with a Chooser action
     */
    [CreateAssetMenu(menuName = "SimpleExamplesMod/UniqueSpawnableChooserData")]
    public class UniqueSpawnableChooserData : DataObject, ISpawnable
    {
        [SerializeField, RequireInterface(typeof(ISpawnable))]
        private ListReference<AssetReference<DataObject>> m_spawnables;

        // ISpawnable
        public bool GetSpawnables(SimEntity source, List<ISpawnable.SpawnableData> results, ISpawnable.SpawnCallback callback = null)
        {
            if (callback == null || callback(this))
            {
                foreach (var spawnable in m_spawnables.Values)
                {
                    (spawnable.Asset as ISpawnable).GetSpawnables(source, results, callback);
                }
            }
            return (results.Count > 0);
        }

        public bool CanSpawn(out ISpawnable.SpawnErrorReason reason, out LocID description)
        {
            description = new LocID();
            reason = ISpawnable.SpawnErrorReason.None;
            return true;
        }
    }
}
