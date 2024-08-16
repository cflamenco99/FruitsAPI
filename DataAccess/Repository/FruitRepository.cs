using DataAccess.Models;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class FruitRepository : IFruitRepository
    {
        private readonly FruitContext _context;

        public FruitRepository(FruitContext context)
        {
            _context = context;
        }

        public async Task<List<FruitDTO>> FindAll()
        {
            var response = await _context.Fruits.Include(f => f.Type)
                .Select(x =>
                    new FruitDTO
                    {
                        Description = x.Description,
                        Id = x.Id,
                        Name = x.Name,
                        TypeId = x.TypeId
                    }
                 ).ToListAsync();

            return response;
        }

        public async Task<Fruit> FindById(long id)
        {
            var fruit = await _context.Fruits
                .FirstOrDefaultAsync(f => f.Id == id);

            return fruit;
        }

        public async Task Save(Fruit fruit)
        {
            _context.Fruits.Add(fruit);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Fruit fruit, FruitDTO fruitDTO)
        {

            fruit.Name = fruitDTO.Name;
            fruit.TypeId = fruitDTO.TypeId;
            fruit.Description = fruitDTO.Description;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Fruit fruit)
        {
            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();
        }
    }
}
