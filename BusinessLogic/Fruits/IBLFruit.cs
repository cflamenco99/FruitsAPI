using BusinessLogic.Utils;
using Entities;

namespace BusinessLogic.Fruits
{
    public interface IBLFruit
    {
        Task<Response<List<FruitDTO>>> FindAllFruits();
        Task<Response<FruitDTO>> FindFruitById(int id);
        Task<Response<bool>> SaveFruit(FruitDTO fruitDTO);
        Task<Response<bool>> UpdateFruit(FruitDTO fruitDTO);
        Task<Response<bool>> DeleteFruit(int id);
    }
}
