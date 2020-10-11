using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static PlanetaryUtils;

public class UnitSpawner : PlanetCharacter
{
    [System.Serializable]
    public class SpawnItem
    {
        public float SpawnRate;
        public BaseUnit UnitPrefab;
        public float spawnTimer;
    }

    public Team Team;
    public float StartPosition;
    public int MaxUnits = 4;
    [SerializeField]
    private List<SpawnItem> Spawnables;
    private List<BaseUnit> spawnedUnits;
    void Start()
    {
        spawnedUnits = new List<BaseUnit>(MaxUnits);
        SetPosition(StartPosition);
        for(int i = 0; i < Spawnables.Count; i++)
        {
            Spawnables[i].spawnTimer = Spawnables[i].SpawnRate;
        }
    }

    void Update()
    {

        for(int i = 0; i < Spawnables.Count; i++)
        {
            Spawnables[i].spawnTimer -= Time.deltaTime;

            if(Spawnables[i].spawnTimer <= 0 && GetUnitCount() < MaxUnits)
            {
                var parent = Team == Team.Nature ? Planet.Instance.NatureUnits : Planet.Instance.RobotUnits;
                var unit = Instantiate(Spawnables[i].UnitPrefab, parent);
                unit.StartPosition = StartPosition;
                spawnedUnits.Add(unit);
                Spawnables[i].spawnTimer = Spawnables[i].SpawnRate;
            }
        }
    }

    void FixedUpdate()
    {
        // Prioritize spawnables by spawnTimer to avoid 
        // the first item having a spawn bias.
        // We do it in fixedUpdate 
        // as we shouldn't have to do it very often
        Spawnables.Sort((lhs, rhs) => lhs.spawnTimer.CompareTo(rhs.spawnTimer));
    }

    private int GetUnitCount()
    {
        // Get rid of any destroyed units;
        spawnedUnits.RemoveAll(unit => unit == null);
        return spawnedUnits.Count;
    }
}