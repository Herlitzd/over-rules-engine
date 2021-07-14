using System;
using System.Collections.Generic;
using Over.Rules;

namespace Over.Rules.Tests.Unit.Example
{
  public class Order
  {
    public IEnumerable<IOrderItem> OrderItems { get; set; }
  }

  public interface IOrderItem : IIndentifiable
  {
  }

  public abstract class OrderItem : IOrderItem
  {
    public abstract string Name { get; }

    public abstract Guid Identifier { get; }
  }
}