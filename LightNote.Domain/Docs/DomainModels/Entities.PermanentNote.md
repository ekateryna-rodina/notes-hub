# Domain Models

## Permanent Note

```csharp
 class PermanentNote{
	Task<PermanentNote> Create(Content content, Title title, List<SlipNoteId> slipNoteIds);
	Task Update(PermanentNoteId id, Content content, Title title);
	Task<IReadOnlyCollection<PermanentNote>> Get();
	Task<PermanentNote> GetById(PermanentNoteId id);
	Task Delete(PermanentNoteId id);
 }
```

```json
{
	Id: "00000000-0000-0000-0000-00000000"
	Title: "Title"
	Content: "New Idea"
	SlipNoteIds: ["00000000-0000-0000-0000-00000000", "00000000-0000-0000-0000-00000000"]
	UserId: "00000000-0000-0000-0000-00000000"
	CreatedAt: "2020-01-01T00:00:00.0000000Z"
	UpdatedAt: "2020-01-01T00:00:00.0000000Z"
}
```