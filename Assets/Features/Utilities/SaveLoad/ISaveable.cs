﻿namespace Features.Utilities.SaveLoad
{
    public interface ISaveable
    {
        string GetUniqueIdentifier();
        object CaptureState();
        void RestoreState(object state);
    }
}