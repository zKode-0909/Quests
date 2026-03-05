public static class RuntimeIDGenerator
{
    private static int nextId = 1;

    public static int GetNext() => nextId++;
}