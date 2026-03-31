
public static class CurrentUser
{
    public static int? ClientId { get; set; }
    public static string ClientLogin { get; set; }
    public static string ClientName { get; set; }
    public static string ClientMail { get; set; }
    public static bool IsAuthenticated => ClientId.HasValue;

    public static void Clear()
    {
        ClientId = null;
        ClientLogin = null;
        ClientName = null;
        ClientMail = null;
    }
}