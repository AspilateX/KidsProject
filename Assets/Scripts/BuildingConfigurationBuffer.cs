using System;

public static class BuildingConfigurationBuffer
{
    public static event Action<BuildingPowerConfiguration> OnBufferChanged;
    public static BuildingPowerConfiguration Buffer 
    { 
        get
        {
            return _buffer;
        }
        set
        {
            _buffer = value;
            OnBufferChanged?.Invoke(_buffer);
        } 
    }

    private static BuildingPowerConfiguration _buffer = null;
}
