using DataAccess.Models;
using Entities;

namespace DataAccess.Repository
{
    public interface IFruitRepository
    {
        Task<List<FruitDTO>> FindAll();
        Task<Fruit> FindById(long id);
        Task Save(Fruit fruitDTO);
        Task Update(Fruit fruit, FruitDTO fruitDTO);
        Task Delete(Fruit fruit);
    }
}