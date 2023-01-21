# Domain Models

## Tag

```csharp
class Tag{
	Task<Tag> Create(TagName tag);
	Task Update(TagId id, TagName tag);
	Task<IReadOnlyCollection<Tag>> Tag Get();
	Task<Tag> GetById(TagId id);
	Task Delete(TagId id);
}
```

```json
{
	Id: "00000000-0000-0000-0000-00000000"
	Tag: "Tag"
	References: [Reference]
}
```