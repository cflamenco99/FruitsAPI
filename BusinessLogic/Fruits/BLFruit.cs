using BusinessLogic.Utils;
using DataAccess.Repository;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogic.Fruits
{
    public class BLFruit : IBLFruit
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMemoryCache _cache;
        private static string FruitsKey => "FruitList";

        public BLFruit(IFruitRepository fruitRepository, IMemoryCache cache)
        {
            _fruitRepository = fruitRepository;
            _cache = cache;
        }

        public async Task<Response<List<FruitDTO>>> FindAllFruits()
        {
            try
            {
                if (_cache.TryGetValue(FruitsKey, out List<FruitDTO> fruits))
                {
                    return Response<List<FruitDTO>>.Success(fruits);
                }

                var response = await _fruitRepository.FindAll();

                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                _cache.Set(FruitsKey, response, cacheOptions);

                return Response<List<FruitDTO>>.Success(response);
            }
            catch (Exception ex)
            {
                return Response<List<FruitDTO>>.Fault(ex.Message);
            }
        }

        public async Task<Response<FruitDTO>> FindFruitById(int id)
        {
            try
            {
                var fruit = await _fruitRepository.FindById(id);
                if (fruit is null)
                {
                    return Response<FruitDTO>.Fault(Messages.MSF008, notFound: true);
                }

                var fruitDTO = new FruitDTO()
                {
                    Description = fruit.Description,
                    Id = fruit.Id,
                    Name = fruit.Name,
                    TypeId = fruit.TypeId
                };

                return Response<FruitDTO>.Success(fruitDTO);
            }
            catch (Exception ex)
            {
                return Response<FruitDTO>.Fault(ex.Message);
            }
        }

        public async Task<Response<bool>> SaveFruit(FruitDTO fruitDTO)
        {
            try
            {
                var domainResponse = BLFruitDomain.CheckFruitCreation(fruitDTO);
                if (!domainResponse.Ok)
                {
                    return Response<bool>.Fault(domainResponse.Message);
                }

                await _fruitRepository.Save(domainResponse.Data);

                fruitDTO.Id = domainResponse.Data.Id;
                _cache.Remove(FruitsKey);

                return Response<bool>.Success();
            }
            catch (Exception ex)
            {
                return Response<bool>.Fault(ex.Message);
            }
        }

        public async Task<Response<bool>> UpdateFruit(FruitDTO fruitDTO)
        {
            try
            {
                var isFruitReadyToUpdateResponse = BLFruitDomain.IsFruitReadyToUpdate(fruitDTO);
                if (!isFruitReadyToUpdateResponse.Ok)
                {
                    return Response<bool>.Fault(isFruitReadyToUpdateResponse.Message);
                }

                var fruit = await _fruitRepository.FindById(fruitDTO.Id);
                if (fruit is null)
                {
                    return Response<bool>.Fault(Messages.MSF008, notFound: true);
                }

                await _fruitRepository.Update(fruit, fruitDTO);
                return Response<bool>.Success();
            }
            catch (Exception ex)
            {
                return Response<bool>.Fault(ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteFruit(int id)
        {
            try
            {
                var fruit = await _fruitRepository.FindById(id);
                if (fruit is null)
                {
                    return Response<bool>.Fault(Messages.MSF008, notFound: true);
                }

                await _fruitRepository.Delete(fruit);
                _cache.Remove(FruitsKey);

                return Response<bool>.Success();
            }
            catch (Exception ex)
            {
                return Response<bool>.Fault(ex.Message);
            }
        }
    }
}
