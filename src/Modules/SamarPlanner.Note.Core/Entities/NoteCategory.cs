namespace SamarPlanner.Note.Core.Entities;

public class NoteCategory
{
    public const string TableName = "NoteCategories";
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = null!;

    private readonly List<Note> _notes = new();
    public ICollection<Note> Notes => _notes.AsReadOnly();

    public static NoteCategory Create(Guid userId, string title)
        => new()
        {
            UserId =  userId,
            Title = title
        };

    public void Update(string title)
    {
        Title = title;
    }
}