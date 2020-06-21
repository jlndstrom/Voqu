using Voqu.Models;

namespace Voqu.Services.Mappers
{
    public interface IMapper<T1, T2>
    {
        T1 Map(T2 viewmodel);
        T2 Map(T1 model);
    }
}