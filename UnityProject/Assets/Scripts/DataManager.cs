﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class DataManager : MonoBehaviour
{
    private MaterialStruct[] m_materialData;
    private WeaponStruct[] m_weaponData;

	// Use this for initialization
	void Start () {
        LoadMaterialData();
        LoadWeaponData();
    }
	
    private void LoadMaterialData()
    {
        var materialDataParse = JSON.ParseFile("material_definitions");
        var materialDataArray = materialDataParse["MATERIAL_DEFINITIONS"].AsArray;
        var materialDataCount = materialDataArray.Count;

        m_materialData = new MaterialStruct[materialDataCount];
  
        for (int i = 0; i < materialDataCount; i++)
        {
            var thisMaterial = materialDataArray[i];
        
            string name = thisMaterial["NAME"];
            string colHex = thisMaterial["HEX_COL"];
            int breakOnImpact = thisMaterial["BREAK_ON_IMPACT"].AsInt;

            Color col = HexUtility.hexToColor(colHex);
            string hexColUnityFormat = HexUtility.colorToHex(col);

            //Debug.Log(name + hexColUnityFormat);

            m_materialData[i] = new MaterialStruct(name, hexColUnityFormat, breakOnImpact, col, i);
        }
    }


    private void LoadWeaponData()
    {
        var weaponDataParse = JSON.ParseFile("weapon_definitions");
        var weaponDataArray = weaponDataParse["WEAPON_DEFINITIONS"].AsArray;
        var weaponDataCount = weaponDataArray.Count;

        m_weaponData = new WeaponStruct[weaponDataCount];

        for (int i = 0; i < weaponDataCount; i++)
        {
            var thisWeapon = weaponDataArray[i];

            string name = thisWeapon["NAME"];

            string soundPathActivateWeapon = thisWeapon["SOUND_PATH_ACTIVATE_WEAPON"];

            // note: the particle paths must have all material types defined in same order as 
            // material definitions
            List<string> particlePathHit = new List<string>();
            List<string> decalPathHit = new List<string>();

            for (int m = 0; m < m_materialData.Length; m++)
            {
                string matName = m_materialData[m].m_name.ToUpper();

                // Add Particle path
                string particleName = "PARTICLE_PATH_HIT_" + matName;
                string particlePath = thisWeapon[particleName];
                particlePathHit.Add(particlePath);

                // Add Decal Path
                string decalName = "DECAL_PATH_HIT_" + matName;
                string decalPath = thisWeapon[decalName];
                decalPathHit.Add(decalPath);
            }

            m_weaponData[i] = new WeaponStruct(name, soundPathActivateWeapon, particlePathHit, decalPathHit);
        }
    }

    public MaterialStruct[] GetMaterialDataArray()
    {
        return m_materialData;
    }

    public WeaponStruct[] GetWeaponDataArray()
    {
        return m_weaponData;
    }
}
