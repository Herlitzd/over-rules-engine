using System.Threading.Tasks;

namespace Over.Rules
{
  public interface IRule<TEntityIn, TEntityOut>
  {
    Task<IExecutionResult<TEntityIn, TEntityOut>> Execute(TEntityIn entity);
  }
}