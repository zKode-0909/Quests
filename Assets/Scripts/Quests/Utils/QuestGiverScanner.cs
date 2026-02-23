using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverScanner
{
    QuestGiverRegistry giverRegistry;
    QuestLogRegistry logRegistry;

    float detectionRadius;
    LayerMask layerMask;

    public void Initialize(QuestGiverRegistry qRegistry,QuestLogRegistry lRegistry,float radius,LayerMask mask) { 
        giverRegistry = qRegistry;
        logRegistry = lRegistry;
        detectionRadius = radius;
        layerMask = mask;
    }

    
}
