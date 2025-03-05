using System;
using System.Collections.Generic;
using System.Linq;
using Features.SpawnPoint;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace SpawnPoint
{
    public class SpawnPointTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private SpawnPointTilemapData[] tilemapTextureType;

        [HideInInspector] public SpawnPointTilemapData[] spawnPointTilemapDatas;

        internal void Init()
        {
            TryGetComponent(out tilemap);
            Scan();
        }

        private void Scan()
        {
            spawnPointTilemapDatas = Array.Empty<SpawnPointTilemapData>();
            var spawnPointTilemapDataList = new List<SpawnPointTilemapData>();
            var bounds = tilemap.cellBounds;
            var allTiles = tilemap.GetTilesBlock(bounds);
            for (var x = 0; x < bounds.size.x; x++)
            for (var y = 0; y < bounds.size.y; y++)
            {
                var tile = allTiles[x + y * bounds.size.x];
                if (tile == null) continue;

                var tilePosition = new Vector3Int(x + bounds.xMin, y + bounds.yMin, 0);
                var tilemapTexture = tilemap.GetSprite(tilePosition);
                var tilemapTexture2D = tilemapTexture.texture;
                var textureType = GetTypeSpawnPoint(tilemapTexture2D);
                if (textureType == TypeSpawnPoint.None) continue;
                var spawnPoint = tilemap.CellToWorld(tilePosition);
                var cellSize = tilemap.cellSize;
                spawnPoint.x += cellSize.x / 2;
                spawnPoint.y += cellSize.y / 2;

                var spawnPointTilemapData = new SpawnPointTilemapData(
                    spawnPoint,
                    tilemapTexture2D,
                    tile,
                    textureType
                );
                spawnPointTilemapDataList.Add(spawnPointTilemapData);
            }

            spawnPointTilemapDatas = spawnPointTilemapDataList.ToArray();
        }

        private TypeSpawnPoint GetTypeSpawnPoint(Object tilemapTexture2D)
        {
            return (from spawnPointTilemapData in tilemapTextureType
                where spawnPointTilemapData.texture2D == tilemapTexture2D
                select spawnPointTilemapData.typeSpawnPoint).FirstOrDefault();
        }

        [Serializable]
        public class SpawnPointTilemapData
        {
            public string name;
            public TypeSpawnPoint typeSpawnPoint;
            public Vector3 position;
            public Texture2D texture2D;
            public TileBase TileBase { get; }

            public SpawnPointTilemapData(Vector3 position, Texture2D texture2D, TileBase tileBase,
                TypeSpawnPoint typeSpawnPoint = TypeSpawnPoint.None)
            {
                this.position = position;
                this.texture2D = texture2D;
                TileBase = tileBase;
                name = typeSpawnPoint.ToString();
                this.typeSpawnPoint = typeSpawnPoint;
            }

            public void SetTypeSpawnPoint(TypeSpawnPoint newTypeSpawnPoint)
            {
                typeSpawnPoint = newTypeSpawnPoint;
                name = newTypeSpawnPoint.ToString();
            }

            public override string ToString()
            {
                return $"{name} {typeSpawnPoint} {position}";
            }
        }
    }
}