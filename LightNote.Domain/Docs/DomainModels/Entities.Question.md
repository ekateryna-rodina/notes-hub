# Domain Models

## Question

```csharp
class Question{
    Task<Question> Create(Content link, InsightId? insightI);
    Task Update(QuestionId id, Content content);
    Task<IReadOnlyCollection<Question>> Get();
    Task<Question> GetById(QuestionId id);
    Task Delete(QuestionId id);
}
```

```json
{
    Id: "00000000-0000-0000-0000-00000000"
    Content: "Question?"
    InsightId: "00000000-0000-0000-0000-00000000"
    CreatedAt: "2020-01-01T00:00:00.0000000Z"
    UpdatedAt: "2020-01-01T00:00:00.0000000Z"
    UserId: "00000000-0000-0000-0000-00000000"	
}
```