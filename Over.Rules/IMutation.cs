using System.Threading.Tasks;

namespace Over.Rules
{
  public interface IMutation<TEntityIn, TEntityOut>
  {
    Task<TEntityOut> Apply(TEntityIn entity);
  }
}