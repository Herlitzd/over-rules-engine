using System;

namespace Over.Rules
{
  public interface IIndentifiable
  {
    string Name { get; }
    Guid Identifier { get; }
  }
}
