# Domain Models

## Reference

```csharp
class Reference{
    Task<Reference> Create(ReferenceLink link, List<Tag> tagIds);
    Task Update(ReferenceId id, ReferenceLink tag);
    Task<IReadOnlyCollection<Reference>> Get();
    Task<Reference> GetById(ReferenceId id);
    Task Delete(ReferenceId id);
}
```

```json
{
    Id:  "00000000-0000-0000-0000-00000000"
    Reference: "ref" 
    TagIds [ "00000000-0000-0000-0000-00000000", "00000000-0000-0000-0000-00000000"]
    Questions: [Question]
    UserId: "00000000-0000-0000-0000-00000000"
    CreatedAt: "2020-01-01T00:00:00.0000000Z"
    UpdatedAt: "2020-01-01T00:00:00.0000000Z"
}
```