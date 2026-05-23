using AsyncDataLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Data;
using System.Numerics;
using UnityEngine;

namespace AsyncDataLibrary.Models
{
    public class GameSave : IEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("save_time")]
        public DateTime SaveTime { get; set; }
        [JsonProperty("current_level")]
        public string CurrentLevelName { get; set; }

        [JsonProperty("last_checkpoint_id")]
        public string LastActivatedCheckpointId { get; set; }

        [JsonProperty("player_position_x")]
        public float PlayerPositionX { get; set; }

        [JsonProperty("player_position_y")]
        public float PlayerPositionY { get; set; }

        [JsonProperty("current_health")]
        public int CurrentHealth { get; set; }


        [JsonProperty("collected_coins")]
        public int CollectedCoins { get; set; }
    }
}
