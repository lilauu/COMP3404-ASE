namespace COMP3404_Client;

internal class Bob2
{
    private static readonly string name = "bob";

    public static bool NameIsBob(string nameToCheck)
    {
        return nameToCheck.ToLower() == name;
    }
}
