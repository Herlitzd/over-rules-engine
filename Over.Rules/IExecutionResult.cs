using System.Collections.Generic;

namespace Over.Rules
{
  public interface IExecutionResult<TEntityIn, TEntityOut>
  {
    IReadOnlyCollection<IMutation<TEntityIn, TEntityOut>> Mutations { get; }
  }
}