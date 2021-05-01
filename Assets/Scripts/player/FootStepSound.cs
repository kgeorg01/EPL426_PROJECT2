﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{

    public AudioSource grassWalk;
    public AudioSource rockWalk;
    public AudioSource dirtWalk;
    public AudioSource gravelWalk;
    public AudioSource mudWalk;
    public AudioSource waterWalk;
    public AudioSource poisonWalk;
    public AudioSource defaultWalk;

    public playerVariables playervar;


    private void OnTriggerEnter(Collider col)
    {
        if (playervar.sound )
        {
            footstepSound(col);
        }
    }

    void footstepSound(Collider col)
    {
        if (!playerVariables.grounded) return;

        //Poison and water are game objects with the tag. Terrain sound wont play if that tag is found.
        if (col.gameObject.tag.Equals("Water")) {
            AudioSource.PlayClipAtPoint(waterWalk.clip, this.gameObject.transform.position);
            
            return;
        } else if (col.gameObject.tag.Equals("Poison")) {
            AudioSource.PlayClipAtPoint(poisonWalk.clip, this.gameObject.transform.position);
            return;
        } else if (col.gameObject.tag.Equals("LavaPool"))
        {
            //no sound on lava, is handled by the parent
            return;
        }

        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            //not a terrain, play default sound
            AudioSource.PlayClipAtPoint(defaultWalk.clip, this.gameObject.transform.position);
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        float[] textureMix = GetTerrainTextureMix(transform.position, terrainData, terrain.GetPosition());
        int textureIndex = GetTextureIndex(transform.position, textureMix);
        TerrainLayer[] splatPrototypes = terrain.terrainData.terrainLayers;
        string textureName = splatPrototypes[textureIndex].name;
        
        textureName = textureName.ToLower();
        textureName = textureName.Replace(" ", "");
        
        if (textureName == "moss" || textureName == "terraingrass" || textureName == "rockygrass" || textureName == "rockygrass2") AudioSource.PlayClipAtPoint(grassWalk.clip, this.gameObject.transform.position);
        else if (textureName == "rock" || textureName == "scree" || textureName == "rockypath") AudioSource.PlayClipAtPoint(rockWalk.clip, this.gameObject.transform.position);
        else if (textureName == "mud") AudioSource.PlayClipAtPoint(mudWalk.clip, this.gameObject.transform.position);
        else if (textureName == "dirt" || textureName == "terraindirt") AudioSource.PlayClipAtPoint(dirtWalk.clip, this.gameObject.transform.position);
        else if (textureName == "gravel") AudioSource.PlayClipAtPoint(gravelWalk.clip, this.gameObject.transform.position);
        else AudioSource.PlayClipAtPoint(defaultWalk.clip, this.gameObject.transform.position);

       /// Debug.Log(textureName);
    }

    float[] GetTerrainTextureMix(Vector3 worldPos, TerrainData terrainData, Vector3 terrainPos)
    {
        // returns an array containing the relative mix of textures on the main terrain at this world position.
        // The number of values in the array will equal the number of textures added to the terrain.
        // calculate which splat map cell the worldPos falls within (ignoring y)
        int mapX = (int)(((worldPos.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)(((worldPos.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight);

        // get the splat data for this cell as a 1x1xN 3d array (where N = number of textures)
        float[,,] splatmapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        // extract the 3D array data to a 1D array:
        float[] cellMix = new float[splatmapData.GetUpperBound(2) + 1];

        for (int n = 0; n < cellMix.Length; n++)
        {
            cellMix[n] = splatmapData[0, 0, n];
        }
        return cellMix;
    }

    int GetTextureIndex(Vector3 worldPos, float[] textureMix)
    {
        // returns the zero-based index of the most dominant texture on the terrain at this world position.
        float maxMix = 0;
        int maxIndex = 0;
        // loop through each mix value and find the maximum
        for (int n = 0; n < textureMix.Length; n++)
        {
            if (textureMix[n] > maxMix)
            {
                maxIndex = n;
                maxMix = textureMix[n];
            }
        }
        return maxIndex;
    }

}
