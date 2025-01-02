using System.Collections;

namespace ChronoPiller.Authorization.Core.Models;

public class Result<TResult> where TResult : ICollection
{
    public TResult Items { get; set; }
    public int TotalCount { get => Items.Count; }
}