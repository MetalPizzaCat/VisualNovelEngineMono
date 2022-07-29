
public class DialogText
{
    /// <summary>
    /// This id is used to reference proper speaker from the memory
    /// Leave this as null to speak as "narrator"(no speaker will be explicitly used)
    /// </summary>
    /// <value></value>
    public int? SpeakerId { get; set; }

    public string Text { get; set; } = "PLACEHOLDER_TEXT_123456789";
}