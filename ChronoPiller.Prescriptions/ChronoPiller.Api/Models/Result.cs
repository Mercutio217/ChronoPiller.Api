using System.Collections;

namespace ChronoPiller.Api.Models;

public class Result<TResult> where TResult : ICollection
{
    public TResult Items { get; set; }
    public int TotalCount { get => Items.Count; }
}