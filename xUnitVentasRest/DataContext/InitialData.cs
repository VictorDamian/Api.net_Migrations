using ApiVentas.Repositories;
using ApiVentas.Models;

namespace xUnitVentasRest.DataContext
{
    public class InitialData
    {
        public static void Init(VentasContext context)
        {
            //Datos en memoria
            if(context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                return;
            }
            //Db cargada
            context.Database.EnsureCreated();
            var cliente = new Cliente[]
            {
                new Cliente{Id = 9, Nombre = "Ichika"}               
            };
            foreach (Cliente c in cliente)
                context.Clientes.Add(c);
        }
    }
}