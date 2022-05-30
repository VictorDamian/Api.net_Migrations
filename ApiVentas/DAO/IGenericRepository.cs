namespace ApiVentas.DAO
{
    public interface IGenericRepository<Entity> where Entity:class
    {
        Task<IEnumerable<Entity>> GetAll();
        Entity GetById(int idPk);
        Task Create(Entity entity);
        Task Update(Entity entity);
        Task Delete(Entity entity);
        Task<bool> Save();
    }
}