﻿using Newtonsoft.Json;
using System.ComponentModel;

namespace Region.Persistence.Communication.Request.Enum;

[JsonConverter(typeof(DescriptionEnumConverter))]
public enum RegionRequestEnum
{
    [Description("Norte")]
    Norte,

    [Description("Nordeste")]
    Nordeste,

    [Description("CentroOeste")]
    CentroOeste,

    [Description("Sudeste")]
    Sudeste,

    [Description("Sul")]
    Sul
}