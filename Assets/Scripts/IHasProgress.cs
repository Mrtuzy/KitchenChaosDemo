using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public interface IHasProgress 
{
    public event EventHandler<OnProgressChangedEventAargs> OnProgressChanged;
    public class OnProgressChangedEventAargs : EventArgs
    {
        public float progressNormalized;
    }
}
