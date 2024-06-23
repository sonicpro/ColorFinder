namespace Backend;

public class Error
{
    public string Title { get; set; }
    public string Details { get; set; }
    public Error(string title, string details = null)
    {
        Title = title;
        Details = details;
    }
}