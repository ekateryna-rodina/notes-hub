# Domain Models

## Insight

```csharp
 class Insight{
	Task<Insight> Create(Content content, List<PermanentNoteId> permanentNoteIds);
	Task Update(InsightId id, Content content);
	Task<IReadOnlyCollection<Insight>> Get();
	Task<Insight> GetById(InsightId id);
	Task Delete(InsighteId id);
 }
```

```json
{
    Id: "00000000-0000-0000-0000-00000000"
	Content: ""
	Questions [Question]
	PermanentNoteIds: ["00000000-0000-0000-0000-00000000", "00000000-0000-0000-0000-00000000"]
	CreatedAt: "2020-01-01T00:00:00.0000000Z"
	UpdatedAt: "2020-01-01T00:00:00.0000000Z"
	UserId: "00000000-0000-0000-0000-00000000"
}
```