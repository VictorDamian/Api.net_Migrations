using ApiVentas.Models;
using ApiVentas.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace xUnitVentasRest.DataContext
{
    public class VentasContextMemory
    {
        public VentasContext GetContext()
        {
            var opt = new DbContextOptionsBuilder<VentasContext>().ConfigureWarnings
            (x=>x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).
            UseInMemoryDatabase(databaseName : "TestVentas").Options;

            var context = new VentasContext(opt);
            //mandamos a llmar a iniciar la funcion para inicar los datos de prueba
            InitialData.Init(context);
            return context;
        }
    }
}