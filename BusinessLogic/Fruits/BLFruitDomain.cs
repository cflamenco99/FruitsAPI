using BusinessLogic.Utils;
using DataAccess.Models;
using Entities;

namespace BusinessLogic.Fruits
{
    public class BLFruitDomain
    {
        public static Response<Fruit> CheckFruitCreation(FruitDTO fruitDTO)
        {
            var validationResponse = ValidateCommonFruitProperties(fruitDTO);
            if (!validationResponse.Ok)
            {
                return Response<Fruit>.Fault(validationResponse.Message);
            }

            Fruit fruit = new Fruit()
            {
                Description = fruitDTO.Description,
                Name = fruitDTO.Name,
                TypeId = fruitDTO.TypeId
            };

            return Response<Fruit>.Success(fruit);
        }

        public static Response<bool> IsFruitReadyToUpdate(FruitDTO fruitDTO)
        {
            if (fruitDTO == null || fruitDTO.Id <= 0)
            {
                return Response<bool>.Fault(Messages.MSF007);
            }

            var validationResponse = ValidateCommonFruitProperties(fruitDTO);
            if (!validationResponse.Ok)
            {
                return Response<bool>.Fault(validationResponse.Message);
            }

            return Response<bool>.Success(true);
        }

        private static Response<bool> ValidateCommonFruitProperties(FruitDTO fruitDTO)
        {
            if (fruitDTO is null)
            {
                return Response<bool>.Fault(Messages.MSF001);
            }

            if (string.IsNullOrWhiteSpace(fruitDTO.Name))
            {
                return Response<bool>.Fault(Messages.MSF002);
            }

            if (fruitDTO.TypeId <= 0)
            {
                return Response<bool>.Fault(Messages.MSF003);
            }

            if (fruitDTO.Description == null)
            {
                return Response<bool>.Fault(Messages.MSF004);
            }

            if (string.IsNullOrWhiteSpace(fruitDTO.Description))
            {
                return Response<bool>.Fault(Messages.MSF005);
            }

            if (fruitDTO.Description.Length < 25)
            {
                return Response<bool>.Fault(Messages.MSF006);
            }

            return Response<bool>.Success();
        }
    }
}
