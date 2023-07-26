using System;

/*
None = 0,
NotEnoughVertices           = 1 << 0,
NotConvex                   = 1 << 1,
NotSimple                   = 1 << 2,
AreaTooSmall                = 1 << 3,
SidesTooCloseToParallel     = 1 << 4,
TooThin                     = 1 << 5,
Degenerate                  = 1 << 6,
Unknown                     = 1 << 30,
*/
[Flags]
public enum ResourceType
{
    None = 0,
    Wood = 1 << 0,
    Rock = 1 << 1,
    Mushroom = 1 << 2,
    Apple = 1 << 3
}
