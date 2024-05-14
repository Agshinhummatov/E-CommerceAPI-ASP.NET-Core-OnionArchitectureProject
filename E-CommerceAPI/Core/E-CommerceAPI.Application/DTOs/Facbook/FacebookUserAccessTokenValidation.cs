using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.DTOs.Facbook
{
    public class FacebookUserAccessTokenValidation
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidationData Data { get; set; }
    }

    public class FacebookUserAccessTokenValidationData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }

}
