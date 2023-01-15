# Domain Models

## Slip Note

```csharp
class SlipNote{
	Task<SlipNote> Create(SlipNoteContent content);
	Task Update(SlipNoteId id, SlipNoteContent content);
	Task<IReadOnlyCollection<SlipNote>> SlipNotes Get();
	Task<SlipNote> GetById(SlipNoteId id);
	Task Delete(SlipNoteId id);
}
```

```json
{
	Id: "00000000-0000-0000-0000-00000000"
	Content: "Content
	References: [Reference]
	UserId: "00000000-0000-0000-0000-00000000"
	CreatedAt: "2020-01-01T00:00:00.0000000Z"
	UpdatedAt: "2020-01-01T00:00:00.0000000Z"
}
```