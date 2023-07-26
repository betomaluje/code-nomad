public static class ResourceTypeExt
{
    public static bool IsType(this ResourceType res, ResourceType other)
        => (res & other) != 0;

    public static ResourceType SetFlag(ResourceType a, ResourceType b)
    {
        return a | b;
    }
    public static ResourceType UnsetFlag(ResourceType a, ResourceType b)
    {
        return a & (~b);
    }
    // Works with "None" as well
    public static bool HasFlag(ResourceType a, ResourceType b)
    {
        return (a & b) == b;
    }
    public static ResourceType ToogleFlag(ResourceType a, ResourceType b)
    {
        return a ^ b;
    }
}
