﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI: MonoBehaviour
{
    public abstract Vector3 GetNextAction();
}
