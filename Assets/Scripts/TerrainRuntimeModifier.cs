using UnityEngine;
using System.Collections;

public class TerrainRuntimeModifier : MonoBehaviour {

	public Terrain terrain1; // terrain to modify int hmWidth; // heightmap width int hmHeight; // heightmap height
	public Terrain terrain2;

	public Terrain movingTerrain;

	int posXInTerrain; // position of the game object in terrain width (x axis) int posYInTerrain; // position of the game object in terrain height (z axis)
	int posYInTerrain;

	int size = 100; // the diameter of terrain portion that will raise under the game object float desiredHeight = 0; // the height we want that portion of terrain to be

	int hmWidth1;
	int hmHeight1;

	int hmWidth2;
	int hmHeight2;


	int v_count = 0;

	private float[,] originalHeights;

	void Start () {

		hmWidth1 = terrain1.terrainData.heightmapWidth;
		hmHeight1 = terrain1.terrainData.heightmapHeight;

		hmWidth2 = terrain2.terrainData.heightmapWidth;
		hmHeight2 = terrain2.terrainData.heightmapHeight;

		this.originalHeights = this.terrain1.terrainData.GetHeights(
			0, 0, this.terrain1.terrainData.heightmapWidth, this.terrain1.terrainData.heightmapHeight);

		movingTerrain.terrainData.SetHeights (0, 0, 
			terrain1.terrainData.GetHeights (0, 0, this.terrain1.terrainData.heightmapWidth, this.terrain1.terrainData.heightmapHeight));
	}


	void Update () {

		hmWidth1 = terrain1.terrainData.heightmapWidth;
		hmHeight1 = terrain1.terrainData.heightmapHeight;

		hmWidth2 = terrain2.terrainData.heightmapWidth;
		hmHeight2 = terrain2.terrainData.heightmapHeight;

		if (v_count < 80) {
			// get the heights of the terrain under this game object
			float[,] movingHeight = movingTerrain.terrainData.GetHeights (0, 0, this.movingTerrain.terrainData.heightmapWidth, this.movingTerrain.terrainData.heightmapHeight);
			float[,] heights2 = terrain2.terrainData.GetHeights (0, 0, this.terrain2.terrainData.heightmapWidth, this.terrain2.terrainData.heightmapHeight);
			// we set each sample of the terrain in the size to the desired height
			for (int i = 0; i < this.terrain1.terrainData.heightmapWidth; i++) {
				for (int j = 0; j < this.terrain1.terrainData.heightmapHeight; j++) {
					if (movingHeight [i, j] < heights2 [i, j]) {
						movingHeight [i, j] += 0.001f;
					}
					if (movingHeight [i, j] > heights2 [i, j]) {
						movingHeight [i, j] -= 0.001f;
					}
				}
			}
			// set the new height
			movingTerrain.terrainData.SetHeights (0, 0, movingHeight);
			//Debug.Log ("Type1");
		} else if ( v_count < 160) {
			// get the heights of the terrain under this game object
			float[,] movingHeight = movingTerrain.terrainData.GetHeights (0, 0, this.movingTerrain.terrainData.heightmapWidth, this.movingTerrain.terrainData.heightmapHeight);
			float[,] heights2 = terrain1.terrainData.GetHeights (0, 0, this.terrain1.terrainData.heightmapWidth, this.terrain1.terrainData.heightmapHeight);
			// we set each sample of the terrain in the size to the desired heighty
			for (int i = 0; i < this.terrain1.terrainData.heightmapWidth; i++) {
				for (int j = 0; j < this.terrain1.terrainData.heightmapHeight; j++) {
					if (movingHeight [i, j] > heights2 [i, j]) {
						movingHeight [i, j] -= 0.001f;
					}
					if (movingHeight [i, j] < heights2 [i, j]) {
						movingHeight [i, j] += 0.001f;
					}
				}
			}
			// set the new height
			movingTerrain.terrainData.SetHeights (0, 0, movingHeight);
			//Debug.Log ("Type2");
		} else {
			v_count = 0;
			//Debug.Log ("Rest");
		}

		v_count++;
	} 

		
}
