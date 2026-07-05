using System.Collections;

namespace SamarPlanner.Note.Core.Entities;

public class Note
{
    public const string TableName = "Notes";
    public const int MaxTextPreviewLength = 50;
    
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string? Title { get; private set; }
    public string Text { get; private set; } = null!;
    public DateTime UpdateAt { get; private set; }

    public Guid? CategoryId { get; set; }
    public NoteCategory? Category { get; private set; }

    private readonly List<NoteFile> _files = new();
    public IReadOnlyCollection<NoteFile> Files => _files.AsReadOnly();

    public static Note Create(Guid userId, string? title, string text, List<NoteFile> files, Guid? categoryId)
    {
        var note = new Note()
        {
            UserId = userId,
            Title = title,
            Text = text,
            CategoryId = categoryId,
            UpdateAt = DateTime.Now
        };

        note.AddFiles(files);
        return note;
    }

    public void Update(string? title, string text, Guid? categoryId,IEnumerable<NoteFile> newFiles)
    {
        Title = title;
        Text = text;
        CategoryId = categoryId;
        UpdateAt = DateTime.Now;
        AddFiles(newFiles);
    }
    
    public void RemoveCategory()
    {
        CategoryId  = null;
    }

    public void RemoveFiles(IEnumerable<NoteFile> files)
    {
        foreach (var file in files)
        {
            if (_files.Contains(file))
                _files.Remove(file);
            else throw new ArgumentException($"File {file.Id} does not exist");
        }
    }

    private void AddFiles(IEnumerable<NoteFile> files)
    {
        _files.AddRange(files);
    }
}