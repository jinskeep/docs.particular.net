﻿using NServiceBus;
using NServiceBus.Features;

public static class NServiceBusFeatures
{
    public static void DisableNotUsedFeatures(this BusConfiguration busConfiguration)
    {
        busConfiguration.DisableFeature<Sagas>();
        busConfiguration.DisableFeature<SecondLevelRetries>();
        busConfiguration.DisableFeature<TimeoutManager>();
    }
}
